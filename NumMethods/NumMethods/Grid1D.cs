using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NumMethods
{
    public class Grid1D
    {
        protected double a, b;
        protected int n;
        protected double _h;
        public double [] points;

        public Grid1D(double a, double b, int n)
        {
            this.a = a;
            this.b = b;
            this.n = n;
            _h = (b - a) / n;
            points = new double[n+1];
            for (int i = 0; i <= n; i++)
                points[i] = a + i * h;
        }
        public double h
        {
            get
            {
                return _h;
            }
        }
        public double A
        {
            get
            {
                return a;
            }
        }
        public double B
        {
            get
            {
                return b;
            }
        }
    }
}
