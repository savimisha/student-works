
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IterativeMethods
{
    public class JacobiMethod : IterativeMethod //класс, реализующий метод Якоби
    {
        public JacobiMethod(fxy _Fxy, fxy _Uxy, Rect _rect, int _n, int _m)
            : base(_Fxy, _Uxy, _rect, _n, _m)
        { }
        public override void run()
        {
            double v_old, v_new, Eps_curr; //переменные для хранения значений текущей компоненты, новой и текущей нормы
            double h2 = -(n / (rect.b - rect.a)) * (n / (rect.b - rect.a)); //предварительное вычисление необходимых коэффициентов (ненулевые элементы матрицы А кудрявое)
            double k2 = -(m / (rect.d - rect.c)) * (m / (rect.d - rect.c));
            double a2 = -2 * (h2 + k2);
            s = 0;
            var v1 = new double[n + 1, m + 1]; //временный массив для хранения старых значений ветора v
            while (!isStopped)
            {
                _Eps_max = 0;
                for (int j = 0; j <= m; j++) //копирование старых значений вектора v
                    for (int i = 0; i <= n; i++)
                        v1[i, j] = v[i, j];

                for (int j = 1; j < m; j++) //в этом цикле происходит покомпонентное вычисление новых значений вектора v
                    for (int i = 1; i < n; i++)
                    {
                        v_old = v1[i, j];
                        v_new = -(h2 * (v1[i + 1, j] + v1[i - 1, j]) + k2 * (v1[i, j + 1] + v1[i, j - 1]));
                        v_new += f[i, j];
                        v_new /= a2;
                        Eps_curr = Math.Abs(v_old - v_new); //вычисление нормы
                        if (Eps_curr > _Eps_max) { _Eps_max = Eps_curr; }
                        v[i, j] = v_new;
                    }
                s++;
                if (_Eps_max < _Eps || s >= _Nmax) isStopped = true; //проверка останова
            }
            
        }
    }
}
