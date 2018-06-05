using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatThermalEq
{
    public abstract class Coeffs
    {
        public Coeffs() { }
        public abstract double k1(double x);
        public abstract double k2(double x);
        public abstract double q1(double x);
        public abstract double q2(double x);
        public abstract double f1(double x);
        public abstract double f2(double x);
    }
    public class BaseCoeffs : Coeffs
    {
        public override double k1(double x)
        {
            return Math.Sqrt(2) * Math.Cos(x);
        }
        public override double k2(double x)
        {
            return 2;
        }
        public override double q1(double x)
        {
            return x + 1;
        }
        public override double q2(double x)
        {
            return 2 * x * x;
        }
        public override double f1(double x)
        {
            return Math.Sin(2 * x);
        }
        public override double f2(double x)
        {
            return Math.Sin(x);
        }
    }
    public class TestCoeffs : Coeffs
    {
        public override double k1(double x)
        {
            return 1;
        }
        public override double k2(double x)
        {
            return 2;
        }
        public override double q1(double x)
        {
            return 1 + Math.PI / 4;
        }
        public override double q2(double x)
        {
            return Math.PI * Math.PI / 8;
        }
        public override double f1(double x)
        {
            return 1;
        }
        public override double f2(double x)
        {
            return Math.Sqrt(2) / 2;
        }
    }
}
