using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactDiff
{
    public class Parameters
    {
        public Parameters(Parameters p)
        {
            lambda_1 = p.lambda_1;
            lambda_2 = p.lambda_2;
            alpha = p.alpha;
            betta = p.betta;
            nu = p.nu;
            gamma = p.gamma;
            delta = p.delta;
            n = p.n;
            k = p.k;
            maxStepsT = p.maxStepsT;
            if (p.Harmobic0 != null)
            {
                Harmobic0 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic0[i] = p.Harmobic0[i];
            }
            if (p.Harmobic1 != null)
            {
                Harmobic1 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic1[i] = p.Harmobic1[i];
            }
            if (p.Harmobic2 != null)
            {
                Harmobic2 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic2[i] = p.Harmobic2[i];
            }
            if (p.Harmobic3 != null)
            {
                Harmobic3 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic3[i] = p.Harmobic3[i];
            }
            if (p.Harmobic4 != null)
            {
                Harmobic4 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic4[i] = p.Harmobic4[i];
            }
            if (p.Harmobic5 != null)
            {
                Harmobic5 = new double[3];
                for (int i = 0; i < 3; i++)
                    Harmobic5[i] = p.Harmobic5[i];
            }
        }
        public Parameters() { }
        public double lambda_1, lambda_2;
        public double alpha, betta, gamma, delta, nu;
        public int n;
        public double k;
        public int maxStepsT;
        public double[] Harmobic0;
        public double[] Harmobic1;
        public double[] Harmobic2;
        public double[] Harmobic3;
        public double[] Harmobic4;
        public double[] Harmobic5;
    }
    public abstract class ReactDiffSolving
    {
        protected double lambda_1, lambda_2;
        protected double alpha, betta, gamma, delta, nu;
        protected int n;
        protected int maxStepsT;
        protected int maxStepsTCopy;
        protected double h, k;
        protected Grid1D gridX;
        protected double[,] v1, v2;
        protected int computedLayers = 0;
        protected int inAllComputedLayers = 0;
        public ReactDiffSolving(Parameters p)
        {
            lambda_1 = p.lambda_1;
            lambda_2 = p.lambda_2;
            alpha = p.alpha;
            betta = p.betta;
            nu = p.nu;
            gamma = p.gamma;
            delta = p.delta;
            n = p.n;
            k = p.k;
            maxStepsT = p.maxStepsT;
            h = 1.0 / n;
            gridX = new Grid1D(0, 1, n);
            v1 = new double[n + 1, maxStepsT + 1];
            v2 = new double[n + 1, maxStepsT + 1];
            for (int i = 0; i <= n; i++)
            {
                v1[i, 0] = p.Harmobic0[1] + p.Harmobic1[1] * Math.Cos(Math.PI * p.Harmobic1[0] * i * h) + p.Harmobic2[1] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h)
                    + p.Harmobic2[1] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h) + p.Harmobic3[1] * Math.Cos(Math.PI * p.Harmobic3[0] * i * h) +
                    p.Harmobic4[1] * Math.Cos(Math.PI * p.Harmobic4[0] * i * h) + p.Harmobic5[1] * Math.Cos(Math.PI * p.Harmobic5[0] * i * h);
                v2[i, 0] = p.Harmobic0[2] + p.Harmobic1[2] * Math.Cos(Math.PI * p.Harmobic1[0] * i * h) + p.Harmobic2[2] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h)
                    + p.Harmobic2[2] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h) + p.Harmobic3[2] * Math.Cos(Math.PI * p.Harmobic3[0] * i * h) +
                    p.Harmobic4[2] * Math.Cos(Math.PI * p.Harmobic4[0] * i * h) + p.Harmobic5[2] * Math.Cos(Math.PI * p.Harmobic5[0] * i * h);
            }
        }
        public abstract void solve();
        public abstract void ComputeNextLayer();
        public void ContinueSolving()
        {
            double[] v1_tmp = new double[n + 1];
            double[] v2_tmp = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                v1_tmp[i] = v1[i, maxStepsTCopy];
                v2_tmp[i] = v2[i, maxStepsTCopy];
            }
            v1 = new double[n + 1, maxStepsT + 1];
            v2 = new double[n + 1, maxStepsT + 1];
            for (int i = 0; i <= n; i++)
            {
                v1[i, 0] = v1_tmp[i];
                v2[i, 0] = v2_tmp[i];
            }
            solve();
        }
        public double[,] V1
        {
            get
            { return v1; }
        }
        public double[,] V2
        {
            get
            { return v2; }
        }
        public int ComputedLayers
        {
            get
            {
                return computedLayers;
            }
        }
        public int InAllComputedLayers
        {
            get
            {
                return inAllComputedLayers;
            }
        }
        public int MaxStepsT
        {
            set
            {
                maxStepsT = value;
            }
        }
    }
}
