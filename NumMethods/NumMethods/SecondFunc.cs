using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class SecondFunc: Function
    {
        public SecondFunc()
        {
            a = 0;
            b = 1;
        }
        public override double val(double x)
        {
            return Math.Sqrt(1 + Math.Pow(x, 4)) + Math.Cos(10*x);
        }
        public override double firstDerivative(double x)
        {
            return 2 * Math.Pow(x, 3) / Math.Sqrt(1 + Math.Pow(x, 4)) - 10 * Math.Sin(10 * x);
        }
        public override double secondDerivative(double x)
        {
            return 2 * x * x * (Math.Pow(x, 4) + 3) / (Math.Pow(Math.Sqrt(1 + Math.Pow(x, 4)), 3)) - 100 * Math.Cos(10 * x);
        }
    }
}
