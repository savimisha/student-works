using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IterativeMethods
{
    
    public abstract class fxy //абстрактный класс для представления функции двух переменных
    {
        public fxy() { }
        public abstract double val(double x, double y); //возвращает значение функции от двух переменных
        public abstract double lap(double x, double y); //оператор Лапласа для этой функции
    }

    public class UxyTest_7 : fxy 
    {
        public override double val(double x, double y)
        {
            return Math.Exp(Math.Sin(Math.PI * x * y) * Math.Sin(Math.PI * x * y));
        }
        public override double lap(double x, double y)
        {
            return (-0.5) * Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2)) * Math.PI * Math.PI * (x * x + y * y) *
                (-1.0 - 4.0 * Math.Cos(2.0 * Math.PI * x * y) + Math.Cos(4.0 * Math.PI * x * y));
        }
    }
    public class FxyTest_7 : fxy
    {
        public override double val(double x, double y)
        {
            return 0.5 * Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2)) * Math.PI * Math.PI * (x * x + y * y) *
                (-1.0 - 4.0 * Math.Cos(2.0 * Math.PI * x * y) + Math.Cos(4.0 * Math.PI * x * y));
        }
        public override double lap(double x, double y)
        {
            return 0;
        }
    }

    public class UxyTest_4 : fxy 
    {
        public override double val(double x, double y)
        {
            return Math.Sin(Math.PI*x*y);
        }
        public override double lap(double x, double y)
        {
            return -Math.PI * Math.PI * Math.Sin(Math.PI * x * y) * (x * x + y * y);
        }
    }
    public class FxyTest_4 : fxy
    {
        public override double val(double x, double y)
        {
            return Math.PI * Math.PI * Math.Sin(Math.PI * x * y) * (x * x + y * y);
        }
        public override double lap(double x, double y)
        {
            return Math.PI * Math.PI * Math.Sin(Math.PI * x * y) * (x * x + y * y);
        }
    }

    public class FxyTest_2 : fxy 
    {
        public override double val(double x, double y)
        {
            return 4;
        }
        public override double lap(double x, double y)
        {
            return 0;
        }
    }
    public class UxyTest_2 : fxy 
    {
        public override double val(double x, double y)
        {
            return 1-x*x-y*y;
        }
        public override double lap(double x, double y)
        {
            return -4;
        }
    }
    public class UxyBase_7 : fxy
    {
        public override double val(double x, double y)
        {
            if (x == 0) return Math.Sin(Math.PI * y) * Math.Sin(Math.PI * y);
            if (x == 2.0) return Math.Sin(Math.PI * y * 2.0) * Math.Sin(Math.PI * y * 2.0);
            if (y == 0) return Math.Sin(Math.PI * x) * Math.Sin(Math.PI * x);
            if (y == 1.0) return Math.Sin(Math.PI * x * 2.0) * Math.Sin(Math.PI * x * 2.0);
            return 0;
        }
        public override double lap(double x, double y)
        {
            return 0;
        }
    }
    public class FxyBase_7 : fxy
    {
        public override double val(double x, double y)
        {
            return Math.Abs(x * x - 2 * y);
        }
        public override double lap(double x, double y)
        {
            return 0;
        }
    }
    public class UxyBase_7_2 : fxy
    {
        public override double val(double x, double y)
        {
            if (x == 0) return Math.Sin(Math.PI * y) * Math.Sin(Math.PI * y);
            if (x == 2 && y <= 0.5) return Math.Sin(Math.PI * y * 2) * Math.Sin(Math.PI * y * 2);
            if (y == 0) return Math.Sin(Math.PI * x) * Math.Sin(Math.PI * x);
            if (y == 1 && x < 1) return Math.Sin(Math.PI * x * 2) * Math.Sin(Math.PI * x * 2);
            if (x == 1 && y >= 0.5) return 2.0;//Math.Sin(Math.PI * y) * Math.Sin(Math.PI * y);
            if (y == 0.5 && x >= 1) return 2.0;// Math.Sin(Math.PI * y) * Math.Sin(Math.PI * y);
            return 0;
        }
        public override double lap(double x, double y)
        {
            return 0;
        }
    }
}
