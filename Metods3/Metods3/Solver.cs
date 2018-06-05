using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metods3
{
    class Solver
    {
        private IMethod _method;
        private GraphicsCreator _graphics;
        private bool _IsStronginMethod;

        private Function _func;
        private List<double> Area; //{a,b,c,d}
        private List<int> _countIter;
        private List<bool> _IsEpsilonCondition;
        private List<double> _r;
        private List<double> _epsilon;
        private bool _IsDimensionalTask;
        private int _NumRun;


        public Solver(Function func, List<double> area, List<int> countIter, List<bool> isEpsilonCondition,
            List<double> r, List<double> epsilon, bool isDimensionalTask, int numRun, GraphicsCreator graphics, bool IsStronginMethod)
        {
            _func = func;
            Area = area;
            _countIter = countIter;
            _IsEpsilonCondition = isEpsilonCondition;
            _r = r;
            _epsilon = epsilon;
            _IsDimensionalTask = isDimensionalTask;
            _NumRun = numRun;
            _graphics = graphics;
            _IsStronginMethod = IsStronginMethod;
        }

        public void Solve()
        {
            if (_IsStronginMethod)
            {
                _method = new MethodStrongin(_func, Area, _countIter, _IsEpsilonCondition, _r, _epsilon, _IsDimensionalTask, _NumRun);
            }
            else
            {
                _method = new MethodPiyavskii(_func, Area, _countIter, _IsEpsilonCondition, _r, _epsilon, _IsDimensionalTask, _NumRun);
            }
            var minValue = _method.Run();
            _graphics.DrawMethodsPoint(_method.GetPointsArrayLists(), _method.GetOrderX(), minValue);
        }
    }
}
