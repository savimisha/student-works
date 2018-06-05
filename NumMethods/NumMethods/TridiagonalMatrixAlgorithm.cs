using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public static class TridiagonalMatrixAlgorithm
    {
        public static void Solve(double[] A, double[] C, double[] B, double[] F, ref double[] X, int n)
        {
            var alpha = new double[n];
            var betta = new double[n];
            alpha[0] = 0;
            betta[0] = 0;
            for (int i = 1; i < n; i++)
            {
                alpha[i] = -B[i - 1] / (A[i - 1] * alpha[i - 1] + C[i - 1]);
                betta[i] = (F[i - 1] - A[i - 1] * betta[i - 1]) / (A[i - 1] * alpha[i - 1] + C[i - 1]);
            }
            X[n - 1] = (F[n - 1] - A[n - 1] * B[n - 1]) / (C[n - 1] + A[n - 1] * alpha[n - 1]);
            for (int i = n - 2; i >= 0; i--)
                X[i] = alpha[i + 1] * X[i + 1] + betta[i + 1];
        }
    }
}
