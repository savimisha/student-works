using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class TestTask2: ThermalCondEq
    {
        public override double f(double x, double t)
        {
            return 0;
        }
        public override double BoundaryConditionLeft(double t)
        {
            return 0;
        }
        public override double BoundaryConditionRight(double t)
        {
            return 0;
        }
        public override double InitialCondition(double x)
        {
            return Math.Sin(Math.PI*x);
        }
        public TestTask2()
        {
            a = 0;
            b = 1;
            T = 1;
        }
        public override double analiticRes(double x, double t)
        {
            return Math.Exp(-Math.PI * Math.PI * t) * Math.Sin(Math.PI * x);
        } 
    }
}
