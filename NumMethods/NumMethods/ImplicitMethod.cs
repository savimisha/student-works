using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class ImplicitMethod: SolvingThermalCondEq
    {
        public ImplicitMethod(ThermalCondEq _task, int _n, int _m):base(_task, _n, _m) { }
        protected override void solve()
        {
            result = new double[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
                result[i, 0] = task.InitialCondition(Xgrid.points[i]);
            for (int j = 0; j <= m; j++)
            {
                result[0, j] = task.BoundaryConditionLeft(Tgrid.points[j]);
                result[n, j] = task.BoundaryConditionRight(Tgrid.points[j]);
            }
            f = new double[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                    f[i, j] = task.f(Xgrid.points[i], Tgrid.points[j]);

            var A = new double[n + 1];
            var B = new double[n + 1];
            var C = new double[n + 1];
            var F = new double[n + 1];
            var tmpRes = new double[n + 1];
            for (int i = 1; i < n; i++)
            {
                A[i] = - 1 / (Xgrid.h * Xgrid.h);
                C[i] = 1 / Tgrid.h + 2 / (Xgrid.h * Xgrid.h);
                B[i] = - 1 / (Xgrid.h * Xgrid.h);
            }
            A[0] = 0;
            C[0] = 1;
            B[0] = 0;
            A[n] = 0;
            C[n] = 1;
            B[n] = 0;
            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i < n; i++)
                    F[i] = result[i, j - 1] / Tgrid.h + f[i, j];
                F[0] = task.BoundaryConditionLeft(Tgrid.points[j]);
                F[n] = task.BoundaryConditionRight(Tgrid.points[j]);
                TridiagonalMatrixAlgorithm.Solve(A, C, B, F, ref tmpRes, n + 1);
                for (int i = 0; i <= n; i++)
                    result[i, j] = tmpRes[i];
            }

        }
    }
}
