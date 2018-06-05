using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runge_Kutta
{
    public abstract class Function2
    {
        public abstract double val(double x, double u);
    }
    public class Test7 : Function2
    {
        public override double val(double x, double u)
        {
            return -3.5 * u;
        }
    }
    public class BaseFirst7 : Function2
    {
        public override double val(double x, double u)
        {
            return u * u / (1.0 + 3 * x + x * x) + u - u * u * u * Math.Sin(10 * x);
        }
    }
}
