using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IterativeMethods
{
    public class SeidelMethod : IterativeMethod //класс, реализующий метод Зейделя
    {
        public SeidelMethod(fxy _Fxy, fxy _Uxy, Rect _rect, int _n, int _m)
            : base(_Fxy, _Uxy, _rect, _n, _m)
        { }
        public override void run()
        {
            double v_old, v_new, Eps_curr;
            double h2 = -(n / (rect.b - rect.a)) * (n / (rect.b - rect.a));
            double k2 = -(m / (rect.d - rect.c)) * (m / (rect.d - rect.c));
            double a2 = -2 * (h2 + k2);
            s = 0;
            while (!isStopped)
            {
                _Eps_max = 0;
                for (int j = 1; j < m; j++)
                for (int i = 1; i < n; i++)
                {
                    v_old = v[i,j];
                    v_new = -(h2*(v[i+1,j]+v[i-1,j])+k2*(v[i,j+1]+v[i,j-1]));
                    v_new += f[i,j];
                    v_new /= a2;
                    Eps_curr = Math.Abs(v_old - v_new);
                    if (Eps_curr > _Eps_max) { _Eps_max = Eps_curr; }
                    v[i,j] = v_new;
                }
                s++;
                if (_Eps_max < _Eps || s >= _Nmax) isStopped = true;
            }
            
        }
    }
}

