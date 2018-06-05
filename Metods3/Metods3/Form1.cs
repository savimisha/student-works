using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metods3
{
    public partial class Form1 : Form
    {
        double[] deltaX;
        double[] deltaY;
        private List<double> Area;
        private List<int> _countIter;
        private List<bool> _IsEpsilonCondition;
        private List<double> _r;
        private List<double> _epsilon;
        public GraphicsCreator graphics;
        public bool IsStopDraw = false;
        public bool IsDrawingStep;

        private bool _IsStronginMethod;
        public Form1()
        {
            InitializeComponent();
            _countIter = new List<int>();
            _IsEpsilonCondition = new List<bool>();
            _r = new List<double>();
            _epsilon = new List<double>();
            Area = new List<double>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeArea();
            graphics = new GraphicsCreator(InitializeFunction(), pictureBox1.Width, pictureBox1.Height, deltaX, deltaY, pictureBox1, this); 
            graphics.DrawIsolineGraphics();
            Area.Clear();
        }

        private Function InitializeFunction()
        {
            int i = 0;
            double A, B, C;
            if (function1.Checked == true)
                i = 0;
            if (function2.Checked == true)
                i = 1;
            if (function3.Checked == true)
                i = 2;
            if (function4.Checked == true)
                i = 3;
            A = Convert.ToDouble(textBoxA.Text);
            B = Convert.ToDouble(textBoxB.Text);
            C = Convert.ToDouble(textBoxC.Text);
            return new Function(A, B, C, 0, i);
        }

        private void InitializeArea()
        {
            deltaX = new double[2];
            deltaY = new double[2];
            deltaX[0] = Convert.ToDouble(textBoxX0.Text);
            deltaX[1] = Convert.ToDouble(textBoxX1.Text);
            deltaY[0] = Convert.ToDouble(textBoxY0.Text);
            deltaY[1] = Convert.ToDouble(textBoxY1.Text);
            lab1.Text = textBoxX0.Text;
            lab2.Text = textBoxX1.Text;
            lab3.Text = textBoxY0.Text;
            lab4.Text = textBoxY1.Text;
            Area.Add(deltaX[0]);
            Area.Add(deltaX[1]);
            Area.Add(deltaY[0]);
            Area.Add(deltaY[1]);
        }

        private void InitializeParametrs()
        {
            if (radioButton3.Checked)
            {
                _IsStronginMethod = false;
            }
            if (radioButton4.Checked)
            {
                _IsStronginMethod = true;
            }
            if (radioButton5.Checked)
            {
                _IsEpsilonCondition.Add(false);
                _IsEpsilonCondition.Add(false);
                _countIter.Add(Convert.ToInt32(textBox6.Text));
                _countIter.Add(Convert.ToInt32(textBox1.Text));
            }
            else
            {
                _IsEpsilonCondition.Add(true);
                _IsEpsilonCondition.Add(true);
                _epsilon.Add(Convert.ToDouble(textBox6.Text));
                _epsilon.Add(Convert.ToDouble(textBox1.Text));
            }
            _r.Add(Convert.ToDouble(textBox8.Text));
            _r.Add(Convert.ToDouble(textBox2.Text));
            if (radioButton1.Checked == true)
                IsDrawingStep = true;
            if (radioButton2.Checked == true)
                IsDrawingStep = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsStopDraw = false;
            InitializeParametrs();
            InitializeArea();
            Solver solv = new Solver(InitializeFunction(), Area, _countIter, _IsEpsilonCondition, _r, _epsilon, false, 0, graphics, _IsStronginMethod);
            solv.Solve();
            _IsEpsilonCondition.Clear();
            _countIter.Clear();
            _r.Clear();
            _epsilon.Clear();
            Area.Clear();
        }

        public delegate void InvokeDelegate();
        public void RefreshGraph()
        {
            pictureBox1.BeginInvoke(new InvokeDelegate(IvokeMethod));
        }

        public void IvokeMethod()
        {
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsStopDraw = true;
        }

    }
}
