using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class CubicSplineInterpolation
    {
        public class CubicSline
        {
            public double a, b, c, d, x;
        }
        
        private Function f;
        public Grid1D grid;
        public Grid1D AddGrid;
        private bool NatCond;
        public CubicSline[] splines;
        private double[] funcValues;
        private int n;
        private int AddN;
        public int cond = 0;
        public CubicSplineInterpolation(Function _f, double a, double b, int _n, int _add_n, int _cond)
        {
            cond = _cond;
            f = _f;
            n = _n;
            AddN = _add_n;
            grid = new Grid1D(a, b, n);
            AddGrid = new Grid1D(a, b, AddN*n);
            splines = new CubicSline[n+1];
            for (int i = 0; i <= n; i++)
                splines[i] = new CubicSline();
            NatCond = false;
            funcValues = new double[n + 1];
            for (int i = 0; i <= n; i++)
                funcValues[i] = f.val(grid.points[i]);
            interpolate();
        }

        private void interpolate()
        {
            double h = grid.h;
            for (int i = 0; i <= n; i++)
            {
                splines[i].a = funcValues[i];
                splines[i].x = grid.points[i];
            }
            double[] A = new double[n + 1];
            double[] C = new double[n + 1];
            double[] B = new double[n + 1];
            double[] X = new double[n + 1];
            double[] F = new double[n + 1];
            A[0] = 0;
            C[0] = 1;
            B[0] = 0;
            A[n] = 0;
            C[n] = 1;
            B[n] = 0;
            F[0] = 0;
            F[n] = 0;
            if (cond == 1)
            {
                F[0] = f.secondDerivative(grid.points[0]);
                F[n] = f.secondDerivative(grid.points[n]);
            }
            for (int i = 1; i < n; i++)
            {
                A[i] = h;
                C[i] = 4 * h;
                B[i] = h;
                F[i] = 6 * (funcValues[i + 1] - 2*funcValues[i] + funcValues[i - 1]) / h;
            }
            TridiagonalMatrixAlgorithm.Solve(A, C, B, F, ref X, n + 1);
            for (int i = 1; i <= n; i++)
            {
                splines[i].c = X[i];
                splines[i].d = (X[i] - X[i - 1]) / h;
                splines[i].b = (funcValues[i] - funcValues[i - 1]) / h + h * (2 * X[i] + X[i - 1]) / 6;
            }
            /*splines[0].d = (splines[0].c - X[0]) / h;
            splines[0].b = (funcValues[1] - funcValues[0]) / h + h * (2 * splines[0].c + X[0]) / 6;
            for (int i = 1; i < n; i++)
            {
                splines[i].d = (splines[i].c - splines[i - 1].c) / h;
                splines[i].b = (funcValues[i+1] - funcValues[i]) / h + h * (2 * splines[i].c + splines[i-1].c) / 6;
            }*/
        }
        private int WhatSpline(double x)
        {
            int tmp = Convert.ToInt32( Math.Truncate((x - grid.A) / grid.h));
            if (x == grid.B) return tmp;
            return tmp + 1;
        }
       public double sp(double x)
        {
            int k = WhatSpline(x);
            return splines[k].a + splines[k].b * (x - splines[k].x) +
                        splines[k].c * Math.Pow((x - splines[k].x), 2) / 2 + splines[k].d * Math.Pow((x - splines[k].x), 3) / 6;
        }
       public double spFirstDerivative(double x)
       {
           int k = WhatSpline(x);
           return  splines[k].b  +
                       splines[k].c *(x - splines[k].x) + splines[k].d * Math.Pow((x - splines[k].x), 2) / 2;
       }
        public bool verify()
        {
            double eps = 0.000001;
            double[] tempArr = new double[AddN * n];
            for (int i = 0; i < AddN*n; i++)
            {
                tempArr[i] = f.val(AddGrid.points[i]) - sp(AddGrid.points[i]);
            }
            double max = tempArr[0];
            for (int i = 0; i < AddN * n; i++)
            {
                if (max < tempArr[i]) max = tempArr[i];
            }

            if (max < eps) return true;
            return false;

        }
    }
}
