using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metods3
{
    public class GraphicsCreator
    {
        private Function _function;
        private int _width;
        private int _height;
        private double[] _deltaX;
        private double[] _deltaY;
        private double _stepX;
        private double _stepY;
        private int _countLevel = 9;
        private Bitmap bmp;
        private Bitmap bmpIsoline;
        private PictureBox _pictureBox;
        private List<List<double>> _pointArray;
        private List<double> _orderListX;
        private Form1 _form;
        public GraphicsCreator(Function function, int width, int height, double[] deltaX, double[] deltaY, PictureBox pictureBox, Form1 form)
        {
            _function = function;
            _width = width;
            _height = height;
            _deltaX = deltaX;
            _deltaY = deltaY;
            _stepX = (deltaX[1] - deltaX[0]) / _width;
            _stepY = (deltaY[1] - deltaY[0]) / _height;
            _pictureBox = pictureBox;
            _form = form;
        }


        public void DrawMethodsPoint(List<List<double>> pointArray, List<double> orderListX, double minValue)
        {
            _form.label12.Text = Convert.ToString(minValue);
            _pointArray = pointArray;
            _orderListX = orderListX;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    bmpIsoline.SetPixel(i, j, bmp.GetPixel(i,j));
                }
            }
            if (_form.IsDrawingStep)
            {
                Thread treadDraw = new Thread(DrawingStep);
                treadDraw.Start();
                Thread Stopering = new Thread(new ParameterizedThreadStart(Stoper));
                Stopering.Start(treadDraw);
            }
            else
            {
                Drawing();
            }
            
        }

        private void Stoper(object treadDraw)
        {
            Thread _treadDraw = (Thread) treadDraw;
            bool flag = true;
            while (flag)
            {
                if (_form.IsStopDraw == true)
                {
                    flag = false;
                    _treadDraw.Abort();
                }  
            }
        }
        private void DrawingStep()
        {
            int j = 0;
            foreach (var item in _orderListX)
            {
                for (int i = 0; i < _width - 1; i++)
                {
                    if (item > _deltaX[0] + i*_stepX && item <= _deltaX[0] + (i + 1)*_stepX)
                    {
                        foreach (double elem in _pointArray[j])
                        {
                            for (int k = 0; k < _height - 1; k++)
                            {
                                if (elem > _deltaY[0] + k*_stepY && elem <= _deltaY[0] + (k + 1)*_stepY)
                                {
                                    bmpIsoline.SetPixel(i, k, Color.Black);
                                    bmpIsoline.SetPixel(i + 1, k + 1, Color.Black);
                                    bmpIsoline.SetPixel(i + 1, k, Color.Black);
                                    bmpIsoline.SetPixel(i, k + 1, Color.Black);
                                    _pictureBox.Image = bmpIsoline;
                                    _form.RefreshGraph();
                                    Thread.Sleep(100);
                                }
                            }
                        }
                        j++;
                    }
                }
            }
        }
        private void Drawing()
        {
            int j = 0;

            foreach (var item in _orderListX)
            {
                for (int i = 0; i < _width - 1; i++)
                {
                    if (item >= _deltaX[0] + i * _stepX && item < _deltaX[0] + (i + 1) * _stepX)
                    {
                        foreach (double elem in _pointArray[j])
                        {
                            for (int k = 0; k < _height - 1; k++)
                            {
                                if (elem >= _deltaY[0] + k * _stepY && elem < _deltaY[0] + (k + 1) * _stepY)
                                {
                                    bmpIsoline.SetPixel(i, k, Color.Black);
                                    bmpIsoline.SetPixel(i + 1, k + 1, Color.Black);
                                    bmpIsoline.SetPixel(i + 1, k, Color.Black);
                                    bmpIsoline.SetPixel(i, k + 1, Color.Black);
                                }
                            }
                        }
                        j++;
                    }
                }
            }
            _pictureBox.Image = bmpIsoline;
            _form.RefreshGraph();
        }
        public void DrawIsolineGraphics()
        {
            bmp = new Bitmap(_width, _height);
            bmpIsoline = new Bitmap(_width, _height);
            Pixel[,] PixelColor = GetGridColor();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    bmp.SetPixel(i , j, PixelColor[i, j].color);
                }
            }
            _pictureBox.Image = bmp;
        }

        private Pixel[,] GetGridColor()
        {
            double min = 0, max = 0;
            Pixel[,] PixelOnGrid = new Pixel[_width, _height];
            var ValueFunction = GetMinMax(ref min, ref max);
            var levelValue = LevelValue(min, max);

            double value = 0;
            int level = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    value = ValueFunction[i, j];
                    level = GetLevel(value, levelValue);
                    PixelOnGrid[i, j].color = MappingColor(level);
                }
            }
            return PixelOnGrid;
        }

        private double[,] GetMinMax(ref double min, ref double max)
        {
            double [,] ValueFunction = new double[_width,_height];
            double value = 0;
            min = _function.GetValue(_deltaX[0] + _stepX, _deltaY[0] +  _stepY);
            max = _function.GetValue(_deltaX[0] + _stepX, _deltaY[0] + _stepY);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    value = _function.GetValue(_deltaX[0] + i*_stepX, _deltaY[0] + j*_stepY);
                    ValueFunction[i, j] = value;
                    if (value < min)
                    {
                        min = value;
                    }
                    else
                    {
                        if (value > max)
                            max = value;
                    }
                }
            }
            return ValueFunction;
        }

        private double[] LevelValue(double min, double max)
        {
            double[] levelValue = new double[_countLevel + 1];
            double stepLevel = (max - min)/_countLevel;
            levelValue[0] = min;

            for (int i = 1; i < _countLevel + 1; i++)
            {
                levelValue[i] = levelValue[i - 1] + stepLevel;
            }

            return levelValue;
        }

        private int GetLevel(double value, double[] levelValue)
        {
            int level = 0;

            for (int i = 0; i < _countLevel; i++)
            {
                if (value > levelValue[i] && value < levelValue[i + 1])
                    level = i;
            }

            return level;
        }
        private Color MappingColor(int level)
        {
            switch (level)
            {
                case 0:
                    return Color.AliceBlue;
                case 1:
                    return Color.Khaki;
                case 2:
                    return Color.Yellow;
                case 3:
                    return Color.Gold;
                case 4:
                    return Color.Goldenrod;
                case 5:
                    return Color.DarkOrange;
                case 6:
                    return Color.OrangeRed;
                case 7:
                    return Color.Red;
                case 8:
                    return Color.Brown;
                default:
                    return Color.Black;
            }
        }
    }

    struct Pixel
    {
         public Color color;
    }
}
