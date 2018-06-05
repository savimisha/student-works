using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumIntegration
{
    public class IntegrationRectangle:Integration
    {
        public override double compute(double a, double b, Function f, int n)
        {
            double s = 0, h = (b - a) / n;
            for (int i = 0; i < n; i++)
                s += f.value(a + i * h + h / 2) * h;
            return s;
        }
    }
    public class IntegrationTrapeze : Integration
    {
        public override double compute(double a, double b, Function f, int n)
        {
            double s = 0, h = (b - a) / n;
            for (int i = 0; i < n; i++)
                s += 0.5 * (f.value(a + i * h) + f.value(a + (i + 1) * h)) * h;
            return s;
        }
    }
    public class IntegrationSimpson : Integration
    {
        public override double compute(double a, double b, Function f, int n)
        {
            double s = 0, h = (b - a) / n;
            for (int i = 0; i < n; i++)
                s += 1.0 / 6.0 * (f.value(a + i * h) + f.value(a + (i + 1) * h) + 4.0*f.value(a + i*h + h/2.0)) * h;
            return s;
        }
    }
    public abstract class Integration
    {
        public abstract double compute(double a, double b, Function f, int n);
    }

}
