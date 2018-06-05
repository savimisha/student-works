using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class ExplicitMethod: SolvingThermalCondEq
    {
        public ExplicitMethod(ThermalCondEq _task, int _n, int _m):base(_task, _n, _m) { }
        protected override void solve()
        {
            result = new double[n+1, m+1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                    result[i, j] = 0;
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
            double left, center, upper, right;
            double a, b;
            for (int j = 1; j <= m; j++)
                for (int i = 1; i < n; i++)
                {
                    left = result[i - 1, j - 1];
                    center = result[i, j - 1];
                    right = result[i + 1, j - 1];
                    a = result[i - 1, j - 1] - 2 * result[i, j - 1] + result[i + 1, j - 1];
                    b = Tgrid.h / (Xgrid.h * Xgrid.h);
                    upper = a*b + result[i, j - 1] + Tgrid.h * f[i, j - 1];
                    result[i,j] = a * b + result[i, j - 1] + Tgrid.h * f[i, j - 1];
                    //result[i, j] = Tgrid.h * (result[i - 1, j - 1] - 2 * result[i, j - 1] + result[i + 1, j - 1]) / (Xgrid.h * Xgrid.h) + result[i, j - 1] + Tgrid.h * f[i, j - 1];
                }


        }
    }
}
