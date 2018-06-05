using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class TestTask: ThermalCondEq
    {
        public override double f(double x, double t)
        {
            return 0;
        }
        public override double BoundaryConditionLeft(double t)
        {
            return 1;
        }
        public override double BoundaryConditionRight(double t)
        {
            return 100;
        }
        public override double InitialCondition(double x)
        {
            return 99 * x + 1;
        }
        public TestTask()
        {
            a = 0;
            b = 1;
            T = 1;
        }
        public override double analiticRes(double x, double t)
        {
            return 99 * x + 1;
        }
    }
}
