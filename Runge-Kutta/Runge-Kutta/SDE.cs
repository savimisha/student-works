using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runge_Kutta
{
    public class RK4System : SDE
    {
        public RK4System(double u00, double u01, Function3 f1, Function3 f2, double x0, double X, double h, int Nmax, double eps)
            : base(u00, u01, f1, f2, x0, X, h, Nmax, eps) 
        {
                p = 4;
        }
        public override void solve()
        {
            int i = 0;
            v[0, 0] = u00;
            v[0, 1] = u01;
            v2[0, 0] = u00;
            v2[0, 1] = u01;
            double x_curr = x0;
            x[0] = x0;
            double k1, k2, k3, k4, m1, m2, m3, m4, v_next_half1, v_next_average_half1, v_next1, x_curr_half,
                v_next_half2, v_next_average_half2, v_next2;
            double epsCond;
            while (i < Nmax - 1)
            {
                epsCond = h[i];
                if (condControl)
                    if (X - epsCond < x_curr && X + epsCond > x_curr)
                        break;
                k1 = f1.val(x_curr, v[i, 0], v[i, 1]);
                m1 = f2.val(x_curr, v[i, 0], v[i, 1]);
                k2 = f1.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k1, v[i, 1] + (h[i] / 2.0) * m1);
                m2 = f2.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k1, v[i, 1] + (h[i] / 2.0) * m1);
                k3 = f1.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k2, v[i, 1] + (h[i] / 2.0) * m2);
                m3 = f2.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k2, v[i, 1] + (h[i] / 2.0) * m2);
                k4 = f1.val(x_curr + h[i], v[i, 0] + h[i] * k3, v[i, 1] + h[i] * m3);
                m4 = f2.val(x_curr + h[i], v[i, 0] + h[i] * k3, v[i, 1] + h[i] * m3);
                v_next1 = v[i, 0] + (h[i] / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next2 = v[i, 1] + (h[i] / 6.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                k1 = f1.val(x_curr, v[i, 0], v[i, 1]);
                m1 = f2.val(x_curr, v[i, 0], v[i, 1]);
                k2 = f1.val(x_curr + h[i] / 4.0, v[i, 0] + (h[i] / 4.0) * k1, v[i, 1] + (h[i] / 4.0) * m1);
                m2 = f2.val(x_curr + h[i] / 4.0, v[i, 0] + (h[i] / 4.0) * k1, v[i, 1] + (h[i] / 4.0) * m1);
                k3 = f1.val(x_curr + h[i] / 4.0, v[i, 0] + (h[i] / 4.0) * k2, v[i, 1] + (h[i] / 4.0) * m2);
                m3 = f2.val(x_curr + h[i] / 4.0, v[i, 0] + (h[i] / 4.0) * k2, v[i, 1] + (h[i] / 4.0) * m2);
                k4 = f1.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k3, v[i, 1] + (h[i] / 2.0) * m3);
                m4 = f2.val(x_curr + h[i] / 2.0, v[i, 0] + (h[i] / 2.0) * k3, v[i, 1] + (h[i] / 2.0) * m3);

                v_next_average_half1 = v[i, 0] + (h[i] / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next_average_half2 = v[i, 1] + (h[i] / 12.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                x_curr_half = x_curr + h[i] / 2.0;

                k1 = f1.val(x_curr_half, v_next_average_half1, v_next_average_half2);
                m1 = f2.val(x_curr_half, v_next_average_half1, v_next_average_half2);
                k2 = f1.val(x_curr_half + h[i] / 4.0, v_next_average_half1 + (h[i] / 4.0) * k1, v_next_average_half2 + (h[i] / 4.0) * m1);
                m2 = f2.val(x_curr_half + h[i] / 4.0, v_next_average_half1 + (h[i] / 4.0) * k1, v_next_average_half2 + (h[i] / 4.0) * m1);
                k3 = f1.val(x_curr_half + h[i] / 4.0, v_next_average_half1 + (h[i] / 4.0) * k2, v_next_average_half2 + (h[i] / 4.0) * m2);
                m3 = f2.val(x_curr_half + h[i] / 4.0, v_next_average_half1 + (h[i] / 4.0) * k2, v_next_average_half2 + (h[i] / 4.0) * m2);
                k4 = f1.val(x_curr_half + h[i] / 2.0, v_next_average_half1 + (h[i] / 2.0) * k3, v_next_average_half2 + (h[i] / 2.0) * m3);
                m4 = f2.val(x_curr_half + h[i] / 2.0, v_next_average_half1 + (h[i] / 2.0) * k3, v_next_average_half2 + (h[i] / 2.0) * m3);

                v_next_half1 = v_next_average_half1 + (h[i] / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next_half2 = v_next_average_half2 + (h[i] / 12.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                v2[i + 1, 0] = v_next_half1;
                v2[i + 1, 1] = v_next_half2;
                s[i + 1, 0] = Math.Abs((v_next_half1 - v_next1) / 15.0);
                s[i + 1, 1] = Math.Abs((v_next_half2 - v_next2) / 15.0);
                v[i + 1, 0] = v_next1;
                v[i + 1, 1] = v_next2;
                x_curr += h[i];
                x[i + 1] = x_curr;
                h[i + 1] = h[i];
                i++;
                iterationsCount++;
            }
        }

        public override void solveWithControlError()
        {
            int i = 1;
            v[0, 0] = u00;
            v[0, 1] = u01;
            v2[0, 0] = u00;
            v2[0, 1] = u01;
            double x_curr = x0;
            x[0] = x0;
            double k1, k2, k3, k4, m1, m2, m3, m4, v_next_half1, v_next_average_half1, v_next1, x_curr_half,
                v_next_half2, v_next_average_half2, v_next2, H;
            double epsCond;
            H = h[0];
            int half = 0, doub = 0;
            while (i < Nmax)
            {
                epsCond = H;
                if (condControl)
                    if (X - epsCond < x_curr && X + epsCond > x_curr)
                        break;
                k1 = f1.val(x_curr, v[i - 1, 0], v[i - 1, 1]);
                m1 = f2.val(x_curr, v[i - 1, 0], v[i - 1, 1]);
                k2 = f1.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k1, v[i - 1, 1] + (H / 2.0) * m1);
                m2 = f2.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k1, v[i - 1, 1] + (H / 2.0) * m1);
                k3 = f1.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k2, v[i - 1, 1] + (H / 2.0) * m2);
                m3 = f2.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k2, v[i - 1, 1] + (H / 2.0) * m2);
                k4 = f1.val(x_curr + H, v[i - 1, 0] + H * k3, v[i - 1, 1] + H * m3);
                m4 = f2.val(x_curr + H, v[i - 1, 0] + H * k3, v[i - 1, 1] + H * m3);
                v_next1 = v[i - 1, 0] + (H / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next2 = v[i - 1, 1] + (H / 6.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                k1 = f1.val(x_curr, v[i - 1, 0], v[i - 1, 1]);
                m1 = f2.val(x_curr, v[i - 1, 0], v[i - 1, 1]);
                k2 = f1.val(x_curr + H / 4.0, v[i - 1, 0] + (H / 4.0) * k1, v[i - 1, 1] + (H / 4.0) * m1);
                m2 = f2.val(x_curr + H / 4.0, v[i - 1, 0] + (H / 4.0) * k1, v[i - 1, 1] + (H / 4.0) * m1);
                k3 = f1.val(x_curr + H / 4.0, v[i - 1, 0] + (H / 4.0) * k2, v[i - 1, 1] + (H / 4.0) * m2);
                m3 = f2.val(x_curr + H / 4.0, v[i - 1, 0] + (H / 4.0) * k2, v[i - 1, 1] + (H / 4.0) * m2);
                k4 = f1.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k3, v[i - 1, 1] + (H / 2.0) * m3);
                m4 = f2.val(x_curr + H / 2.0, v[i - 1, 0] + (H / 2.0) * k3, v[i - 1, 1] + (H / 2.0) * m3);

                v_next_average_half1 = v[i - 1, 0] + (H / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next_average_half2 = v[i - 1, 1] + (H / 12.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                x_curr_half = x_curr + h[i] / 2.0;

                k1 = f1.val(x_curr_half, v_next_average_half1, v_next_average_half2);
                m1 = f2.val(x_curr_half, v_next_average_half1, v_next_average_half2);
                k2 = f1.val(x_curr_half + H / 4.0, v_next_average_half1 + (H / 4.0) * k1, v_next_average_half2 + (H / 4.0) * m1);
                m2 = f2.val(x_curr_half + H / 4.0, v_next_average_half1 + (H / 4.0) * k1, v_next_average_half2 + (H / 4.0) * m1);
                k3 = f1.val(x_curr_half + H / 4.0, v_next_average_half1 + (H / 4.0) * k2, v_next_average_half2 + (H / 4.0) * m2);
                m3 = f2.val(x_curr_half + H / 4.0, v_next_average_half1 + (H / 4.0) * k2, v_next_average_half2 + (H / 4.0) * m2);
                k4 = f1.val(x_curr_half + H / 2.0, v_next_average_half1 + (H / 2.0) * k3, v_next_average_half2 + (H / 2.0) * m3);
                m4 = f2.val(x_curr_half + H / 2.0, v_next_average_half1 + (H / 2.0) * k3, v_next_average_half2 + (H / 2.0) * m3);

                v_next_half1 = v_next_average_half1 + (H / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                v_next_half2 = v_next_average_half2 + (H / 12.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                v2[i, 0] = v_next_half1;
                v2[i, 1] = v_next_half2;
                s[i, 0] = Math.Abs((v_next_half1 - v_next1) / 15.0);
                s[i, 1] = Math.Abs((v_next_half2 - v_next2) / 15.0);
                h[i] = H;
                double s1 = Math.Max(s[i, 0], s[i, 1]);
                if (s1 > eps)
                {
                    H /= 2.0;
                    half++;
                    continue;
                }
                if (s1 <= eps / 32.0)
                {
                    H *= 2.0;
                    doub++;
                }

                v[i, 0] = v_next1;
                v[i, 1] = v_next2;

                halfCount[i] = half;
                doubleCount[i] = doub;

                
                x_curr += H;
                x[i] = x_curr;
                i++;
                iterationsCount++;
            }
        }
    }

    public abstract class SDE
    {
        protected int p;
        protected double u00, u01;
        protected Function3 f1, f2;
        protected double x0, X;
        protected int Nmax;
        protected double[,] v;
        protected double[,] v2;
        protected double[,] s;
        protected double[] h;
        protected double[] x;
        protected int[] doubleCount, halfCount;
        protected int iterationsCount;
        protected double eps;
        protected bool condControl = false;
        public SDE(double u00, double u01, Function3 f1, Function3 f2, double x0, double X, double h, int Nmax, double eps)
        {
            this.u00 = u00;
            this.u01 = u01;
            this.f1 = f1;
            this.f2 = f2;
            this.x0 = x0;
            this.X = X;
            this.Nmax = Nmax;
            this.eps = eps;
            v = new double[Nmax,2];
            v2 = new double[Nmax,2];
            s = new double[Nmax,2];
            x = new double[Nmax];
            doubleCount = new int[Nmax];
            halfCount = new int[Nmax];
            halfCount[0] = 0;
            doubleCount[0] = 0;
            this.h = new double[Nmax];
            this.h[0] = h;
            iterationsCount = 0; 
        }
        public abstract void solve();
        public abstract void solveWithControlError();
        public bool CondControl
        {
            set
            {
                condControl = value;
            }
        }
        public int IterationsCount
        {
            get
            {
                return iterationsCount;
            }
        }
        public double[] H
        {
            get
            {
                return h;
            }
        }
        public double[,] S
        {
            get
            {
                return s;
            }
        }
        public double[,] V
        {
            get
            {
                return v;
            }
        }
        public double[,] V2
        {
            get
            {
                return v2;
            }
        }
        public int[] DoubleCount
        {
            get
            {
                return doubleCount;
            }
        }
        public int[] HalfCount
        {
            get
            {
                return halfCount;
            }
        }
        public double[] xi
        {
            get
            {
                return x;
            }
        }
        public int P
        {
            get
            {
                return p;
            }
        }
    }
}
