using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatThermalEq
{
    public class StatThermalEqSolving
    {
        private Coeffs coeffs;
        private double mu1, mu2, ksi;
        private double a = 0, b = 1;
        private Grid1D grid;
        private double h;
        private int n;
        private double[] v;
        public StatThermalEqSolving(Coeffs _coeffs, double _mu1, double _mu2, double _ksi, int _n, double _a = 0, double _b = 1)
        {
            a = _a;
            b = _b;
            n = _n;
            mu1 = _mu1;
            mu2 = _mu2;
            ksi = _ksi;
            coeffs = _coeffs;
            grid = new Grid1D(a, b, n);
            h = (b - a) / n;
            v = new double[n + 1];
        }
        public void solve()
        {
            var a = new double[n+1];
            var d = new double[n+1];
            var fi = new double[n+1];

            for (int i = 1; i <= n; i++)
            {
                if (grid.points[i] <= ksi) a[i] = coeffs.k1(grid.points[i]-h/2);
                if (grid.points[i - 1] >= ksi) a[i] = coeffs.k2(grid.points[i]-h/2);
                if (ksi > grid.points[i - 1] && ksi < grid.points[i]) a[i] = h / ((ksi - grid.points[i - 1]) / coeffs.k1((ksi + grid.points[i - 1]) / 2) 
                    + ((grid.points[i] - ksi) / coeffs.k2((ksi + grid.points[i]) / 2)));
            }
            for (int i = 1; i < n; i++)
            {
                if (grid.points[i] + h / 2 <= ksi)
                {
                    fi[i] = coeffs.f1(grid.points[i]);
                    d[i] = coeffs.q1(grid.points[i]);
                }
                if (grid.points[i] - h / 2 >= ksi)
                {
                    fi[i] = coeffs.f2(grid.points[i]);
                    d[i] = coeffs.q2(grid.points[i]);
                }
                if (ksi > grid.points[i] - h / 2 && ksi < grid.points[i] + h / 2)
                {
                    fi[i] = (ksi - (grid.points[i]-h/2)) * coeffs.f1((ksi + (grid.points[i] - h/2)) / 2) / h
                    + ((grid.points[i]+ h/2) - ksi) * coeffs.f2((ksi + (grid.points[i] + h/2)) / 2) / h;
                    d[i] = (ksi - (grid.points[i] - h / 2)) * coeffs.q1((ksi + (grid.points[i] - h / 2)) / 2) / h
                    + ((grid.points[i] + h / 2) - ksi) * coeffs.q2(((ksi + grid.points[i]) + h / 2) / 2) / h;
                }
            }


            var A = new double[n+1];
            var C = new double[n+1];
            var B = new double[n+1];
            var F = new double[n+1];
            for(int i = 1; i < n; i++)
            {
                A[i] = a[i]/(h*h);
                C[i] = -a[i]/(h*h) - a[i+1]/(h*h) - d[i];
                B[i] = a[i+1]/(h*h);
                F[i] = -fi[i];
            }
            A[0] = 0;
            C[0] = 1;
            B[0] = 0;
            A[n] = 0;
            C[n] = 1;
            B[n] = 0;
            F[0] = mu1;
            F[n] = mu2;
            var alpha = new double[n+1];
            var betta = new double[n+1];
            alpha[0] = 0;
            betta[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                alpha[i] = -B[i - 1] / (A[i - 1] * alpha[i - 1] + C[i - 1]);
                betta[i] = (F[i - 1] - A[i - 1] * betta[i - 1]) / (A[i - 1] * alpha[i - 1] + C[i - 1]);
            }
            v[n] = (F[n] - A[n] * B[n]) / (C[n] + A[n] * alpha[n]);
            for (int i = n - 1; i >= 0; i--)
                v[i] = alpha[i + 1] * v[i + 1] + betta[i + 1];


        }
        public double[] V
        {
            get
            {
                return v;
            }
        }
        public Grid1D Grid
        {
            get
            {
                return grid;
            }
        }
    }

    public class StatThermalEqSolving_simple
    {
        private double a = 0, b = 1;
        private Grid1D grid;
        private double h;
        private int n;
        private double[] v;
        public StatThermalEqSolving_simple(int _n, double _a = 0, double _b = 1)
        {
            a = _a;
            b = _b;
            n = _n;
            grid = new Grid1D(a, b, n);
            h = (b - a) / n;
            v = new double[n + 1];
        }
        public void solve()
        {
            
            var A = new double[n + 1];
            var C = new double[n + 1];
            var B = new double[n + 1];
            var F = new double[n + 1];
            for (int i = 1; i < n; i++)
            {
                A[i] = 1 / (h * h);
                C[i] = -2 / (h * h) - 3;
                B[i] = 1 / (h * h);
                F[i] = -15 * Math.Pow(grid.points[i], 2) + 18 * grid.points[i] - 11;
            }
            A[0] = 0;
            C[0] = 1;
            B[0] = 0;
            A[n] = 0;
            C[n] = 1;
            B[n] = 0;
            F[0] = 7;
            F[n] = 6;
            var alpha = new double[n + 1];
            var betta = new double[n + 1];
            alpha[0] = 0;
            betta[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                alpha[i] = -B[i - 1] / (A[i - 1] * alpha[i - 1] + C[i - 1]);
                betta[i] = (F[i - 1] - A[i - 1] * betta[i - 1]) / (A[i - 1] * alpha[i - 1] + C[i - 1]);
            }
            v[n] = (F[n] - A[n] * B[n]) / (C[n] + A[n] * alpha[n]);
            for (int i = n - 1; i >= 0; i--)
                v[i] = alpha[i + 1] * v[i + 1] + betta[i + 1];


        }
        public double[] V
        {
            get
            {
                return v;
            }
        }
        public Grid1D Grid
        {
            get
            {
                return grid;
            }
        }
    }

}
