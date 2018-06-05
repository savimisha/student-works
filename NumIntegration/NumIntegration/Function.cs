using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumIntegration
{
    public abstract class Function
    {
        public abstract double value(double x);
    }
    public class f1 : Function
    {
        public override double value(double x)
        {
            return (1.0 + 2.0 * x) / (4.0 * x);
        }
    }
    public class f3 : Function
    {
        public override double value(double x)
        {
            return (1.0 + 2.0 * x) / (4.0 * x) + Math.Cos(10*x);
        }
    }
    public class f4 : Function
    {
        public override double value(double x)
        {
            return (1.0 + 2.0 * x) / (4.0 * x) + Math.Cos(100*x);
        }
    }
    public class f2 : Function
    {
        public double[] A;
        public double[] B;
        public double alpha;
        public double X;
        public override double value(double x)
        {
            double s = 0;
            for (int i = 0; i < 14; i++)
                s += A[i] * Math.Sin(2.0 * Math.PI * i * (alpha - X) * x) + B[i] * Math.Cos(2.0 * Math.PI * i * (alpha - X) * x);
            return s;

        }
    }
}
