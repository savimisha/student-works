using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public abstract class Function
    {
        public abstract double val(double x);
        public abstract double firstDerivative(double x);
        public abstract double secondDerivative(double x);
        public double a, b;
    }
}
