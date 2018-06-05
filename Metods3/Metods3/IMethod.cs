using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Metods3
{
    interface IMethod
    {
        double CharactR(int i);
        double Run();
        double GetPoint(int i);
        void GetValueCharactM(int i);
        void MethodWithLimitNumIter();
        void MethodWithLimitEpsilon();
        List<double> GetOrderX();
        List<List<double>> GetPointsArrayLists();
    }
}
