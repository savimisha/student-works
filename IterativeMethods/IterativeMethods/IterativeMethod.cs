using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IterativeMethods
{
    public struct Rect //структура для представления ГУ (для компактности)
    {
        public double a, b, c, d;
        public Rect(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
    }
    public abstract class IterativeMethod //абстрактный класс для численного решения задачи Дирихле итерационными методами
    {
        protected double _Eps = 1e-009; //точность
        protected int _Nmax = 10000; //максимальное количество итераций
        protected int s = 0; //счетчик итераций
        protected int n, m; //размеры сетки
        protected double[,] v; //массив для хранения численного решения на каждой итерации
        protected double[,] f; //массив для хранения значений функции f(x,y) из уранения Пуассона
        protected Rect rect; //ГУ
        protected double _Eps_max = 0; //переменная для хранения нормы вектора v
        protected bool isStopped = false; //флаг остатовки рассчета
        protected fxy Fxy; //функция f
        protected fxy Uxy; //функция u
        protected double h, k; //переменные для хванения шагов по сетке
        public IterativeMethod(fxy _Fxy, fxy _Uxy, Rect _rect, int _n, int _m) //конструктор, в котором задаются необходимые исходные данные, 
                                                                               //выделается память под массивы, вычисляются значения функции f в узлах сетки и ГУ в узлах
        {
            n = _n;
            m = _m;
            Fxy = _Fxy;
            Uxy = _Uxy;
            rect = _rect;
            h = (rect.b - rect.a) / n;
            k = (rect.d - rect.c) / m;
            f = new double[n + 1, m + 1];
            v = new double[n + 1, m + 1];
            for (int i = 0; i < n + 1; i++) //вычисление значений функции f
                for (int j = 0; j < m + 1; j++)
                {
                    f[i, j] = Fxy.val(rect.a + i * h, rect.c + j * k);
                    v[i, j] = 0;
                }
            for (int j = 0; j <= m; j++) //вычисление ГУ
            {
                v[0, j] = Uxy.val(rect.a, rect.c + j * k);
                v[n, j] = Uxy.val(rect.b, rect.c + j * k);
            }
            for (int i = 0; i <= n; i++) //вычисление ГУ
            {
                v[i, 0] = Uxy.val(rect.a + i*h, rect.c);
                v[i, m] = Uxy.val(rect.a + i * h, rect.d);
            }
        }

        public double Eps
        {
            set { _Eps = value; }
        }
        public int Nmax
        {
            set { _Nmax = value; }
        }
        public double Eps_max
        {
            get { return _Eps_max; }
        }
        public int S
        {
            get { return s; }
        }
        public double[,] V
        {
            get { return v; }
        }
        public double H
        {
            get { return h; }
        }
        public double K
        {
            get { return k; }
        }
        public int N
        {
            set { n = value; }
        }
        public int M
        {
            set { m = value; }
        }

        public abstract void run(); //метод, который реализует сам численный метод
    }
}
