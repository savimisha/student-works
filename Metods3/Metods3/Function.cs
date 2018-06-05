using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metods3
{
    public class Function
    {
        private double a;
        private double b;
        private double c;
        private double d;
        private int i;
        public Function(double _a, double _b, double _c, double _d, int _i)
        {
            a = _a;
            b = _b;
            c = _c;
            i = _i;
            d = _d;
        }
        public double GetValue(double x, double y)
        {
            switch (i)
            {
                case 0:
                    return a * x * x + b * y * y + c;
                case 1:
                    return Math.Pow((1 - x), 2) + 100 * Math.Pow((y - Math.Pow(x, 2)), 2);
                case 2:
                    return a * (b * Math.Pow(x, 2) - c * Math.Pow(y, 2));//a * x * x * x + b * x * x + c * x + d;// a * Math.Sin(x -  Math.Sin(2 * y)) ;
                case 3:
                    return 20 + x * x + y * y - 10 * (Math.Cos(2 * Math.PI * x) + Math.Cos(2 * Math.PI * y));
                default:
                    return 0;
            }
        }
    }
}
