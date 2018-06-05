using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class ThirdFunc : Function
    {
        public ThirdFunc()
        {
            a = 0;
            b = 1;
        }
        public override double val(double x)
        {
            return Math.Sqrt(1 + Math.Pow(x, 4)) + Math.Cos(100 * x);
        }
        public override double firstDerivative(double x)
        {
            return 2 * Math.Pow(x, 3) / Math.Sqrt(1 + Math.Pow(x, 4)) - 100 * Math.Sin(100 * x);
        }
        public override double secondDerivative(double x)
        {
            return 2 * x * x * (Math.Pow(x, 4) + 3) / (Math.Pow(Math.Sqrt(1 + Math.Pow(x, 4)), 3)) - 10000 * Math.Cos(100 * x);
        }
    }
}
