using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public abstract class SolvingThermalCondEq
    {
        protected ThermalCondEq task;
        public Grid1D Tgrid, Xgrid;
        public double[,] result;
        protected double[,] f;
        protected int n, m;
        private double norma;
        public SolvingThermalCondEq(ThermalCondEq _task, int _n, int _m)
        {
            task = _task;
            m = _m;
            n = _n;
            Tgrid = new Grid1D(0, task.getT, m);
            Xgrid = new Grid1D(task.getA, task.getB, n);
            solve();
        }
        protected abstract void solve();
        public bool convergence()
        {
            solve();
            double left, right;
            double[,] delta = new double[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                    delta[i, j] = Math.Abs(task.analiticRes(Xgrid.points[i], Tgrid.points[j]) - result[i, j]);
            double[] temp = new double[m+1];
            for (int i = 0; i <= m; i++)
            {
                temp[i] = 0;
                for (int j = 0; j <= n; j++)
                {
                    double a = delta[j, i] * delta[j, i] / (n - 1);
                    temp[i] += delta[j, i] * delta[j, i] / (n - 1);
                    
                }
                temp[i] = Math.Sqrt(temp[i]);
                double b = temp[i];
            }
            double max = temp[0];
            for (int j = 1; j <= m; j++)
            {
                if (max < temp[j]) max = temp[j];
            }
            left = max;
            norma = left;
            right = Xgrid.h * Xgrid.h + Tgrid.h;
            if (left < right) return true;
            return false;
        }
        public double Norma()
        {
            return norma;
        }
    }
}
