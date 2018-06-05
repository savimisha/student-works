using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IterativeMethods
{
    public class MinimalDiscrepancyMethod2 : IterativeMethod
    {
        private Rect rect2;
        private int I, J;
        public MinimalDiscrepancyMethod2(fxy _Fxy, fxy _Uxy, Rect _rect, Rect _rect_2, int _n, int _m)
            : base(_Fxy, _Uxy, _rect, _n, _m)
        {
            rect2 = _rect_2;
        }
        public override void run()
        {
            I = Convert.ToInt32((rect2.a - rect.a) * n / (rect.b - rect.a));
            J = Convert.ToInt32((rect2.c - rect.c) * m / (rect.d - rect.c));
            for (int j = J+1; j <= m; j++)
                for (int i = I+1; i <= n; i++)
                    v[i, j] = 0;
            for (int j = J; j <= m; j++)
                v[I, j] = Uxy.val(rect2.a, rect.c + j * k);
            for (int i = I; i <= n; i++)
                v[i, J] = Uxy.val(rect.a + i * h, rect2.c);

            double v_old, v_new, Eps_curr; //переменные для хранения значений текущей компоненты, новой и текущей нормы
            double h2 = 1 / (h * h); //предварительное вычисление необходимых коэффициентов (ненулевые элементы матрицы А кудрявое)
            double k2 = 1 / (k * k);
            double a2 = -2 * (h2 + k2);
            s = 0;
            double tau = 0, Arr = 0, ArAr = 0, tmp = 0;
            var r = new double[n + 1, m + 1];
            while (!isStopped)
            {
                _Eps_max = 0;
                Arr = 0;
                ArAr = 0;
                for (int j = 1; j < J; j++)
                    for (int i = 1; i < n; i++)
                        r[i, j] = k2 * v[i, j - 1] + k2 * v[i, j + 1] + h2 * v[i - 1, j] + h2 * v[i + 1, j] + a2 * v[i, j] + f[i, j];
                for (int j = J; j < m; j++)
                    for (int i = 1; i < I; i++)
                        r[i, j] = k2 * v[i, j - 1] + k2 * v[i, j + 1] + h2 * v[i - 1, j] + h2 * v[i + 1, j] + a2 * v[i, j] + f[i, j];

                for (int j = 1; j < J; j++)
                    for (int i = 1; i < n; i++)
                    {
                        tmp = k2 * r[i, j - 1] + k2 * r[i, j + 1] + h2 * r[i - 1, j] + h2 * r[i + 1, j] + a2 * r[i, j];
                        Arr += tmp * r[i, j];
                        ArAr += tmp * tmp;
                    }
                for (int j = J; j < m; j++)
                    for (int i = 1; i < I; i++)
                    {
                        tmp = k2 * r[i, j - 1] + k2 * r[i, j + 1] + h2 * r[i - 1, j] + h2 * r[i + 1, j] + a2 * r[i, j];
                        Arr += tmp * r[i, j];
                        ArAr += tmp * tmp;
                    }
                tau = Arr / ArAr;
                for (int j = 1; j < J; j++) //в этом цикле происходит покомпонентное вычисление новых значений вектора v
                    for (int i = 1; i < n; i++)
                    {
                        v_old = v[i, j];
                        v_new = v_old - tau * r[i, j];
                        Eps_curr = Math.Abs(v_old - v_new); //вычисление нормы
                        if (Eps_curr > _Eps_max) { _Eps_max = Eps_curr; }
                        v[i, j] = v_new;
                    }
                for (int j = J; j < m; j++) //в этом цикле происходит покомпонентное вычисление новых значений вектора v
                    for (int i = 1; i < I; i++)
                    {
                        v_old = v[i, j];
                        v_new = v_old - tau * r[i, j];
                        Eps_curr = Math.Abs(v_old - v_new); //вычисление нормы
                        if (Eps_curr > _Eps_max) { _Eps_max = Eps_curr; }
                        v[i, j] = v_new;
                    }


                s++;
                if (_Eps_max <= _Eps || s >= _Nmax) isStopped = true; //проверка останова
            }
        }

    }
}
