using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runge_Kutta
{
    public class RK4 : ODE
    {
        public RK4(double u0, Function2 f, double x0, double X, double h, int Nmax, double eps)
            : base(u0, f, x0, X, h, Nmax, eps) 
        {
                p = 4;
        }
        public override void solve()
        {
            int i = 0;
            v[0] = u0;
            v2[0] = u0;
            double x_curr = x0;
            x[0] = x0;
            double k1, k2, k3, k4, v_next_half, v_next_average_half, v_next, x_curr_half, epsCond, H = h[0];
            while (i < Nmax - 1)
            {
                epsCond = H;
                if (condControl)
                if (X - epsCond < x_curr && X + epsCond > x_curr)
                    break;


                k1 = f.val(x_curr, v[i]);
                k2 = f.val(x_curr + h[i] / 2.0, v[i] + (h[i] / 2.0) * k1);
                k3 = f.val(x_curr + h[i] / 2.0, v[i] + (h[i] / 2.0) * k2);
                k4 = f.val(x_curr + h[i], v[i] + h[i] * k3);
                v_next = v[i] + (h[i] / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);

                k1 = f.val(x_curr, v[i]);
                k2 = f.val(x_curr + h[i] / 4.0, v[i] + (h[i] / 4.0) * k1);
                k3 = f.val(x_curr + h[i] / 4.0, v[i] + (h[i] / 4.0) * k2);
                k4 = f.val(x_curr + h[i] / 2.0, v[i] + (h[i] / 2.0) * k3);
                v_next_average_half = v[i] + (h[i] / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);

                x_curr_half = x_curr + h[i] / 2.0;
                k1 = f.val(x_curr_half, v_next_average_half);
                k2 = f.val(x_curr_half + h[i] / 4.0, v_next_average_half + (h[i] / 4.0) * k1);
                k3 = f.val(x_curr_half + h[i] / 4.0, v_next_average_half + (h[i] / 4.0) * k2);
                k4 = f.val(x_curr_half + h[i] / 2.0, v_next_average_half + (h[i] / 2.0) * k3);
                v_next_half = v_next_average_half + (h[i] / 12.0) * (k1 + 2 * k2 + 2 * k3 + k4);


                v2[i + 1] = v_next_half;
                s[i + 1] = Math.Abs((v_next_half - v_next) / 15.0);
                v[i + 1] = v_next;
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
            v[0] = u0;
            v2[0] = u0;
            x[0] = x0;
            s[0] = 0;
            halfCount[0] = 0;
            doubleCount[0] = 0;
            double x_curr = x0, H = h[0];
            double k1, k2, k3, k4, v_next_half, v_next_average_half, v_next, x_curr_half, epsCond;
            int half = 0, doub = 0;
            x[0] = x0;
            while (i < Nmax)
            {
                epsCond = H;
                if (condControl)
                if (X - epsCond < x_curr && X + epsCond > x_curr)
                    break;
                
                k1 = f.val(x_curr, v[i - 1]);
                k2 = f.val(x_curr + H / 2.0, v[i - 1] + (H / 2.0) * k1);
                k3 = f.val(x_curr + H / 2.0, v[i - 1] + (H / 2.0) * k2);
                k4 = f.val(x_curr + H, v[i - 1] + H * k3);
                v_next = v[i - 1] + (H / 6.0) * (k1 + 2.0 * k2 + 2.0 * k3 + k4);

                k1 = f.val(x_curr, v[i - 1]);
                k2 = f.val(x_curr + H / 4.0, v[i - 1] + (H / 4.0) * k1);
                k3 = f.val(x_curr + H / 4.0, v[i - 1] + (H / 4.0) * k2);
                k4 = f.val(x_curr + H / 2.0, v[i - 1] + (H / 2.0) * k3);
                v_next_average_half = v[i - 1] + (H / 12.0) * (k1 + 2.0 * k2 + 2.0 * k3 + k4);

                x_curr_half = x_curr + H / 2.0;
                k1 = f.val(x_curr_half, v_next_average_half);
                k2 = f.val(x_curr_half + H / 4.0, v_next_average_half + (H / 4.0) * k1);
                k3 = f.val(x_curr_half + H / 4.0, v_next_average_half + (H / 4.0) * k2);
                k4 = f.val(x_curr_half + H / 2.0, v_next_average_half + (H / 2.0) * k3);
                v_next_half = v_next_average_half + (H / 12.0) * (k1 + 2.0 * k2 + 2.0 * k3 + k4);

                v2[i] = v_next_half;
                s[i] = Math.Abs((v_next_half - v_next) / 15.0);
                h[i] = H;
                if (s[i] > eps)
                {
                    H /= 2.0;
                    half++;
                    continue;
                }
                x_curr += H;
                x[i] = x_curr;
                if (s[i] < eps / 32.0)
                {
                    H *= 2.0;
                    doub++;
                }
                v[i] = v_next;
                halfCount[i] = half;
                doubleCount[i] = doub;
                i++;
                iterationsCount++;
            }
        }
    }

    public abstract class ODE
    {
        protected int p;
        protected double u0;
        protected Function2 f;
        protected double x0, X;
        protected int Nmax;
        protected double[] v;
        protected double[] v2;
        protected double[] s;
        protected double[] h;
        protected double[] x;
        protected int[] doubleCount, halfCount;
        protected int iterationsCount;
        protected double eps;
        protected bool condControl = false;
        public ODE(double u0, Function2 f, double x0, double X, double h, int Nmax, double eps)
        {
            this.u0 = u0;
            this.f = f;
            this.x0 = x0;
            this.X = X;
            this.Nmax = Nmax;
            this.eps = eps;
            v = new double[Nmax];
            v2 = new double[Nmax];
            s = new double[Nmax];
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
        public double[] S
        {
            get
            {
                return s;
            }
        }
        public double[] V
        {
            get
            {
                return v;
            }
        }
        public double[] V2
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
