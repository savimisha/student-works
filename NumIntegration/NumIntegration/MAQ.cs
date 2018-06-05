using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumIntegration
{
    public class MAQ
    {
        private Integration formula;
        private int depth = 0;
        public MAQ(Integration formula) 
        {
            this.formula = formula;
        }
        public double compute(double a, double b, Function f, double eps)
        {
            depth++;
            double g = formula.compute(a, b, f, 1);
            double g1 = formula.compute(a, (a + b) / 2.0, f, 1);
            double g2 = formula.compute((a + b) / 2.0, b, f, 1);
            double temp = Math.Abs(g1 + g2 - g);
            if (Math.Abs(g1 + g2 - g) < eps) return g;
            else return compute(a, (a + b) / 2.0, f, eps / 2.0) + compute((a + b) / 2.0, b, f, eps / 2.0);
        }
        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }
    }
}
