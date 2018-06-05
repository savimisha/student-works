using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumMethods;

namespace NumMethodsVisualizationSplines
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plots.Series[0].Points.Clear();
            Plots.Series[1].Points.Clear();
            Plots.ChartAreas[0].AxisX.Minimum = -1;
            Plots.ChartAreas[0].AxisX.Maximum = 1;
            Plots.ChartAreas[0].AxisX.Interval = 0.5;

            int n = Convert.ToInt32(SegmentCount.Text);
            int addN = Convert.ToInt32(AddSegmentCount.Text);
            int idx = comboBox1.SelectedIndex;
            Function f = new FirstFunc();
            if (idx == 0) f = new TestFunc();
            if (idx == 1) f = new FirstFunc();
            if (idx == 2) f = new SecondFunc();
            if (idx == 3) f = new ThirdFunc();
            int cond = 0;
            if (radioButton2.Checked == true) cond = 1;
            var task = new CubicSplineInterpolation(f, f.a, f.b, n, addN, cond);
            bool b = task.verify();
            CubicSplineInterpolation.CubicSline[] cs = task.splines;
            double y;
            int N = 10;
            double h = 0;

            if (checkBox1.Checked == false)
            {
                for (int i = 1; i <= n; i++)
                {

                    h = (task.grid.points[i] - task.grid.points[i - 1]) / N;
                    for (int j = 0; j <= N; j++)
                    {
                        y = task.sp(task.grid.points[i - 1] + j * h);
                        Plots.Series[0].Points.AddXY(task.grid.points[i - 1] + j * h, y);
                    }

                }
                int PointsCount = 50;
                h = (f.b - f.a) / PointsCount;
                for (int i = 0; i <= PointsCount; i++)
                {
                    y = f.val(f.a + i * h);
                    Plots.Series[1].Points.AddXY(f.a + i * h, y);
                }
            }
            if (checkBox1.Checked == true)
            {
                for (int i = 1; i <= n; i++)
                {

                    h = (task.grid.points[i] - task.grid.points[i - 1]) / N;
                    for (int j = 0; j <= N; j++)
                    {
                        y = task.spFirstDerivative(task.grid.points[i - 1] + j * h);
                        Plots.Series[0].Points.AddXY(task.grid.points[i - 1] + j * h, y);
                    }

                }
                int PointsCount = 50;
                h = (f.b - f.a) / PointsCount;
                for (int i = 0; i <= PointsCount; i++)
                {
                    y = f.firstDerivative(f.a + i * h);
                    Plots.Series[1].Points.AddXY(f.a + i * h, y);
                }
            }



            dataTable.RowCount = n+1;
            dataTable.ColumnCount = 7;
            dataTable[0, 0].Value = "N";
            dataTable[1, 0].Value = "Xi";
            dataTable[2, 0].Value = "Xi+1";
            dataTable[3, 0].Value = "a";
            dataTable[4, 0].Value = "b";
            dataTable[5, 0].Value = "c";
            dataTable[6, 0].Value = "d";
            for (int i = 0; i < n; i++)
            {
                dataTable[0, i+1].Value = i + 1;
                dataTable[1, i+1].Value = task.grid.points[i];
                dataTable[2, i+1].Value = task.grid.points[i+1];
                dataTable[3, i+1].Value = cs[i + 1].a;
                dataTable[4, i+1].Value = cs[i + 1].b;
                dataTable[5, i+1].Value = cs[i + 1].c;
                dataTable[6, i+1].Value = cs[i + 1].d;
            }
            var Values = new double[addN * n + 1];
            var ValuesFirstDer = new double[addN * n + 1];
            for (int i = 0; i < addN * n; i++)
            {
                Values[i] = Math.Abs(f.val(task.AddGrid.points[i]) - task.sp(task.AddGrid.points[i]));
                ValuesFirstDer[i] = Math.Abs(f.firstDerivative(task.AddGrid.points[i]) - task.spFirstDerivative(task.AddGrid.points[i]));
            }
            double max1 = Values[0], max2 = ValuesFirstDer[0];
            for (int i = 0; i < addN * n; i++)
            {
                if (max1 < Values[i]) max1 = Values[i];
                if (max2 < ValuesFirstDer[i]) max2 = ValuesFirstDer[i];
            }
            dataTable2.RowCount = addN*n + 2;
            dataTable2.ColumnCount = 8;
            dataTable2[0, 0].Value = "N";
            dataTable2[1, 0].Value = "x";
            dataTable2[2, 0].Value = "f(x)";
            dataTable2[3, 0].Value = "S(x)";
            dataTable2[4, 0].Value = "|f(x)-S(x)|";
            dataTable2[5, 0].Value = "f'(x)";
            dataTable2[6, 0].Value = "S'(x)";
            dataTable2[7, 0].Value = "|f'(x)-S'(x)|"; 
            for (int i = 0; i <= addN*n; i++)
            {     
                dataTable2[0, i + 1].Value = i + 1;
                dataTable2[1, i + 1].Value = task.AddGrid.points[i];
                dataTable2[2, i + 1].Value = f.val(task.AddGrid.points[i]);
                dataTable2[3, i + 1].Value = task.sp(task.AddGrid.points[i]);
                dataTable2[4, i + 1].Value = Math.Abs(f.val(task.AddGrid.points[i]) - task.sp(task.AddGrid.points[i]));
                dataTable2[5, i + 1].Value = f.firstDerivative(task.AddGrid.points[i]);
                dataTable2[6, i + 1].Value = task.spFirstDerivative(task.AddGrid.points[i]);
                dataTable2[7, i + 1].Value = Math.Abs(f.firstDerivative(task.AddGrid.points[i]) - task.spFirstDerivative(task.AddGrid.points[i]));
            }
            label4.Text = "Max|f(x)-S(x)| = " + max1.ToString();
            label5.Text = "Max|f'(x)-S'(x)| = " + max2.ToString();
        }
    }
}
