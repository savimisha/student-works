using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metods3
{
    class MethodPiyavskii : IMethod
    {
        private List<double> X;
        private List<List<double>> _testPointsArrayLists;
        private List<double> _orderX;
        private Function _func;
        private double _m;
        private double _pointTest;
        private List<double> Area; //{a,b,c,d}
        private List<int> _countIter;
        private List<bool> _IsEpsilonCondition;
        private List<double> _r;
        private List<double> _epsilon;
        private List<double> _minPoint;
        private bool _IsDimensionalTask;
        private int _NumRun;
        private double minValue;


        public MethodPiyavskii(Function func, List<double> area, List<int> countIter, List<bool> isEpsilonCondition, List<double> r,
            List<double> epsilon, bool isDimensionalTask, int numRun, double pointTest = 0)
        {
            _func = func;
            Area = area;
            _countIter = countIter;
            _IsEpsilonCondition = isEpsilonCondition;
            _r = r;
            _epsilon = epsilon;
            _IsDimensionalTask = isDimensionalTask;
            _NumRun = numRun;
            _pointTest = pointTest;

            _testPointsArrayLists = new List<List<double>>();
            _orderX = new List<double>();
            X = new List<double>();
            _minPoint = new List<double>();
            X.Add(Area[2 * _NumRun]);
            X.Add(Area[2 * _NumRun + 1]);
        }

        public double CharactR(int i)
        {
            if (_IsDimensionalTask)
            {
                return ((0.5) * _m * (X[i] - X[i - 1]) - (0.5) * (_func.GetValue(_pointTest, X[i]) + _func.GetValue(_pointTest, X[i - 1])));
            }
            else
            {
                return ((0.5) * _m * (X[i] - X[i - 1]) - (0.5) * (_func.GetValue(X[i], _minPoint[i]) + _func.GetValue(X[i - 1], _minPoint[i - 1])));
            }
        }

        public double Run()
        {
            if (_IsEpsilonCondition[_NumRun])
            {
                MethodWithLimitEpsilon();
            }
            else
            {
                MethodWithLimitNumIter();
            }
            return minValue;
        }

        public List<double> GetOrderX()
        {
            return _orderX;
        }

        public List<List<double>> GetPointsArrayLists()
        {
            return _testPointsArrayLists;
        }

        public double GetPoint(int i)
        {
            if (_IsDimensionalTask)
            {
                return ((0.5) * (X[i] + X[i - 1]) -
                        ((double)1 / (double)(2 * _m)) * (_func.GetValue(_pointTest, X[i]) - _func.GetValue(_pointTest, X[i - 1])));
            }
            else
            {
                return ((0.5) * (X[i] + X[i - 1]) -
                        ((double)1 / (double)(2 * _m)) * (_func.GetValue(X[i], _minPoint[i]) - _func.GetValue(X[i - 1], _minPoint[i - 1])));
            }
        }

        public void GetValueCharactM(int top)
        {
            if (_IsDimensionalTask)
            {
                double max = (Math.Abs(_func.GetValue(_pointTest, X[1]) - _func.GetValue(_pointTest, X[0]))) / (X[1] - X[0]);
                double M = max;
                for (int i = 2; i < top; i++)
                {
                    M = (Math.Abs(_func.GetValue(_pointTest, X[i]) - _func.GetValue(_pointTest, X[i - 1]))) / (X[i] - X[i - 1]);
                    if (M > max)
                        max = M;
                }
                M = max;
                if (M == 0)
                {
                    _m = 1;
                }
                else
                {
                    _m = _r[_NumRun] * M;
                }
            }
            else
            {
                double max = (Math.Abs(_func.GetValue(X[1], _minPoint[1]) - _func.GetValue(X[0], _minPoint[0]))) / (X[1] - X[0]);
                double M = max;
                for (int i = 2; i < top; i++)
                {
                    M = (Math.Abs(_func.GetValue(X[i], _minPoint[i]) - _func.GetValue(X[i - 1], _minPoint[i - 1]))) / (X[i] - X[i - 1]);
                    if (M > max)
                        max = M;
                }
                M = max;
                if (M == 0)
                {
                    _m = 1;
                }
                else
                {
                    _m = _r[_NumRun] * M;
                }
            }
        }

        public void MethodWithLimitNumIter()
        {
            double R;
            double x;
            _orderX = new List<double>();
            int point = 1;
            double max = 0;
            int top = 2;
            minValue = _func.GetValue(Area[0], Area[2]);
            if (_IsDimensionalTask == false)
            {
                SetMinPoint(X[0], false);
                SetMinPoint(X[1], false);
            }
            else
            {
                var value = _func.GetValue(_pointTest, Area[2]);
                var value2 = _func.GetValue(_pointTest, Area[2]);
                if (value < minValue)
                    minValue = value;
                if (value < minValue)
                    minValue = value2;
            }
            while (top < _countIter[_NumRun])
            {
                point = 1;
                GetValueCharactM(top);
                max = CharactR(point);
                for (int i = 1; i < top; i++)
                {
                    R = CharactR(i);
                    if (R > max)
                    {
                        max = R;
                        point = i;
                    }
                }
                x = GetPoint(point);
                {
                    var value = _func.GetValue(_pointTest, x);
                    if (value < minValue)
                        minValue = value;
                }
                
                _orderX.Add(x);
                X.Add(x);
                if (_IsDimensionalTask == false)
                {
                    SetMinPoint(X[top], true);
                    var pointTest = _minPoint[top];
                    for (int i = 0; i < top; i++)
                    {
                        if (x < X[i])
                        {
                            for (int j = top; j > i; j--)
                            {
                                X[j] = X[j - 1];
                                _minPoint[j] = _minPoint[j - 1];
                            }
                            X[i] = x;
                            _minPoint[i] = pointTest;
                            break;
                        }
                    }
                }
                else
                {
                    X.Sort();
                }
                top++;
            }
        }

        private void SetMinPoint(double x, bool AddArrayList)
        {
            var method = new MethodPiyavskii( _func, Area, _countIter, _IsEpsilonCondition, _r,
                _epsilon, true, _NumRun + 1, x);
            var value = method.Run();
            if (value < minValue)
                minValue = value;
            var pointArray = method.GetOrderX();
            if(AddArrayList)
            _testPointsArrayLists.Add(pointArray);
            double pointMin = (double)pointArray[pointArray.Count - 1];
            _minPoint.Add(pointMin);
        }
        public void MethodWithLimitEpsilon()
        {
            double R;
            double x;
            _orderX = new List<double>();
            int point = 1;
            double max = 0;
            int top = 2;
            double delta = X[1] - X[0];
            double min = X[1] - X[0];
            minValue = _func.GetValue(Area[0], Area[2]);
            if (_IsDimensionalTask == false)
            {
                SetMinPoint(X[0], false);
                SetMinPoint(X[1], false);
            }
            else
            {
                var value = _func.GetValue(_pointTest, Area[2]);
                var value2 = _func.GetValue(_pointTest, Area[2]);
                if (value < minValue)
                    minValue = value;
                if (value < minValue)
                    minValue = value2;
            }
            while (delta > _epsilon[_NumRun])
            {
                point = 1;
                GetValueCharactM(top);
                max = CharactR(point);
                for (int i = 1; i < top; i++)
                {
                    R = CharactR(i);
                    if (R > max)
                    {
                        max = R;
                        point = i;
                    }
                }
                x = GetPoint(point);
                {
                    var value = _func.GetValue(_pointTest, x);
                    if (value < minValue)
                        minValue = value;
                }
                _orderX.Add(x);
                X.Add(x);
                if (_IsDimensionalTask == false)
                {
                    SetMinPoint(X[top], true);
                    var pointTest = _minPoint[top];
                    for (int i = 0; i < top; i++)
                    {
                        if (x < X[i])
                        {
                            for (int j = top; j > i; j--)
                            {
                                X[j] = X[j - 1];
                                _minPoint[j] = _minPoint[j - 1];
                            }
                            X[i] = x;
                            _minPoint[i] = pointTest;
                            break;
                        }
                    }
                }
                else
                {
                    X.Sort();
                }
                top++;

                for (int i = 1; i < top; i++)
                {
                    delta = (X[i] - X[i - 1]);
                    if (delta < min)
                    {
                        min = delta;
                    }
                }
                delta = min;
            }
        }
    }
}
