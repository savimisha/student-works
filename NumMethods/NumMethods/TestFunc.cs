using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumMethods
{
    public class TestFunc: Function
    {
        public TestFunc()
        {
            a = -1;
            b = 1;
        }
        public override double val(double x)
        {
            if (x >= -1 && x <= 0)
                return x * x * x + 3 * x * x;
            if (x > 0 && x <= 1)
                return -x * x * x + 3 * x * x;
            return 0;
        }
        public override double firstDerivative(double x)
        {
            if (x >= -1 && x <= 0)
                return 3 * x * x + 6 * x ;
            if (x > 0 && x <= 1)
                return -3 * x * x + 6 * x;
            return 0;
        }
        public override double secondDerivative(double x)
        {
            if (x >= -1 && x <= 0)
                return 6 * x + 6;
            if (x > 0 && x <= 1)
                return -6 * x + 6;
            return 0;
        }
    }
}
