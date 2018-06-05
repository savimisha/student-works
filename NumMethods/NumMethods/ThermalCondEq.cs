using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public abstract class ThermalCondEq
    {
        protected double a, b;
        protected double T;
        public abstract double f(double x, double t);
        public abstract double BoundaryConditionLeft(double t);
        public abstract double BoundaryConditionRight(double t);
        public abstract double InitialCondition(double x);
        public abstract double analiticRes(double x, double t);
        public double getT
        {
            get
            {
                return T;
            }
        }
        public double getA
        {
            get
            {
                return a;
            }
        }
        public double getB
        {
            get
            {
                return b;
            }
        }
    }
}
