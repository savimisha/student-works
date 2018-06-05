using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runge_Kutta
{
    public abstract class Function3
    {
        public abstract double val(double x, double u1, double u2);
    }
    public class PhysicsFunction1 : Function3
    {
        public override double val(double x, double u1, double u2)
        {
            return (1.0 / 0.01) * (-0.15 * u1 - 2.0 * u2 - 2.0 * u2 * u2 * u2);
        }
    }
    public class PhysicsFunction2 : Function3
    {
        public override double val(double x, double u1, double u2)
        {
            return u1;
        }
    }

    public class BaseSecond7F1 : Function3
    {
        private double a, b;
        public BaseSecond7F1(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override double val(double x, double u1, double u2)
        {
            return -a * Math.Sqrt(u1 * u1 + 1) - b;
        }
    }
    public class BaseSecond7F2 : Function3
    {
        public override double val(double x, double u1, double u2)
        {
            return u1;
        }
    }
}
