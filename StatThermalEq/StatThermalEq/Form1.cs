using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatThermalEq
{
    public partial class Form1 : Form
    {
        private const double c1 = -0.23104783711550246238017098699224;
        private const double c2 = 0.67094868360394508646969236150596;
        private const double c3 = -0.32356455123943440438682748377914;
        private const double c4 = 0.29940138539752630749366309778080;

        public Form1()
        {
            InitializeComponent();

        }
        private double testTaskVal(double x)
        {
            if (x < Math.PI / 4)
                return c1 * Math.Exp(Math.Sqrt(4 + Math.PI) * x / 2) + c2 * Math.Exp(-Math.Sqrt(4 + Math.PI) * x / 2) + 4 / (4 + Math.PI);
            if (x >= Math.PI / 4)
                return c3 * Math.Exp(Math.PI * x / 4) + c4 * Math.Exp(-Math.PI * x / 4) + 4 * Math.Sqrt(2) / (Math.PI * Math.PI);
            return -10000;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double mu1 = 1, mu2 = 0;
            double ksi = Math.PI / 4;
            int n = Int32.Parse(textBox1.Text);
            Coeffs coeffs = new TestCoeffs();
            var task = new StatThermalEqSolving(coeffs, mu1, mu2, ksi, n);
            task.solve();
            if (checkBox1.Checked == true)
            {
                dataGridView1.ColumnCount = 5;
                dataGridView1.RowCount = n + 2;
                dataGridView1[0, 0].Value = "#";
                dataGridView1[1, 0].Value = "Xi";
                dataGridView1[2, 0].Value = "Ui";
                dataGridView1[3, 0].Value = "Vi";
                dataGridView1[4, 0].Value = "|Ui - Vi|";
                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[0, i].Value = i - 1;
                    dataGridView1[1, i].Value = task.Grid.points[i - 1];
                    dataGridView1[2, i].Value = testTaskVal(task.Grid.points[i - 1]);
                    dataGridView1[3, i].Value = task.V[i - 1];
                    dataGridView1[4, i].Value = Math.Abs(testTaskVal(task.Grid.points[i - 1]) - task.V[i - 1]);
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 1;
                chart1.ChartAreas[0].AxisX.Interval = 0.2;
                chart1.Series[0].Color = Color.Black;
                chart1.Series[1].Color = Color.Black;
                chart1.Series[0].BorderWidth = 5;
                chart1.Series[1].BorderWidth = 5;
                for (int i = 0; i <= n; i++)
                {
                    chart1.Series[0].Points.AddXY(task.Grid.points[i], testTaskVal(task.Grid.points[i]) );
                    chart1.Series[1].Points.AddXY(task.Grid.points[i], task.V[i]);
                }
            }
            double err = 0;
            double err_idx = 0;
            for (int i = 0; i <= n; i++)
            {
                if (err < Math.Abs(testTaskVal(task.Grid.points[i]) - task.V[i]))
                {
                    err = Math.Abs(testTaskVal(task.Grid.points[i]) - task.V[i]);
                    err_idx = i;
                }
            }
            label2.Text = "Ошибка " + err.ToString();
            label3.Text = "Узел № " + err_idx.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double mu1 = 1, mu2 = 0;
            double ksi = Math.PI / 4;
            int n = Int32.Parse(textBox1.Text);
            Coeffs coeffs = new BaseCoeffs();
            var task1 = new StatThermalEqSolving(coeffs, mu1, mu2, ksi, n);
            task1.solve();
            var task2 = new StatThermalEqSolving(coeffs, mu1, mu2, ksi, 2 * n);
            task2.solve();
            if (checkBox1.Checked == true)
            {
                dataGridView1.ColumnCount = 5;
                dataGridView1.RowCount = n + 2;
                dataGridView1[0, 0].Value = "#";
                dataGridView1[1, 0].Value = "Xi";
                dataGridView1[2, 0].Value = "Vi";
                dataGridView1[3, 0].Value = "V1i";
                dataGridView1[4, 0].Value = "|Vi - V1i|";
                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[0, i].Value = i - 1;
                    dataGridView1[1, i].Value = task1.Grid.points[i - 1];
                    dataGridView1[2, i].Value = task1.V[i - 1];
                    dataGridView1[3, i].Value = task2.V[2 * (i - 1)];
                    dataGridView1[4, i].Value = Math.Abs(task1.V[i - 1] - task2.V[2 * (i - 1)]);
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 1;
                chart1.ChartAreas[0].AxisX.Interval = 0.2;
                chart1.Series[0].Color = Color.Black;
                chart1.Series[1].Color = Color.Black;
                chart1.Series[0].BorderWidth = 5;
                chart1.Series[1].BorderWidth = 5;
                for (int i = 0; i <= n; i++)
                {
                    chart1.Series[0].Points.AddXY(task1.Grid.points[i], task1.V[i]);
                    chart1.Series[1].Points.AddXY(task1.Grid.points[i], task2.V[2 * i]);
                }
            }
            double err = 0;
            int err_idx = 0;
            for (int i = 0; i <= n; i++)
            {
                if (err < Math.Abs(task1.V[i] - task2.V[2 * i]))
                {
                    err = Math.Abs(task1.V[i] - task2.V[2 * i]);
                    err_idx = i;
                }
            }
            label2.Text = "Ошибка " + err.ToString();
            label3.Text = "Узел № " + err_idx.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(textBox1.Text);
            var task = new StatThermalEqSolving_simple(n);
            task.solve();
            if (checkBox1.Checked == true)
            {
                dataGridView1.ColumnCount = 5;
                dataGridView1.RowCount = n + 2;
                dataGridView1[0, 0].Value = "#";
                dataGridView1[1, 0].Value = "Xi";
                dataGridView1[2, 0].Value = "Ui";
                dataGridView1[3, 0].Value = "Vi";
                dataGridView1[4, 0].Value = "|Ui - Vi|";
                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[0, i].Value = i - 1;
                    dataGridView1[1, i].Value = task.Grid.points[i - 1];
                    dataGridView1[2, i].Value = 5 * Math.Pow(task.Grid.points[i], 2) - 6 * task.Grid.points[i] + 7;
                    dataGridView1[3, i].Value = task.V[i - 1];
                    dataGridView1[4, i].Value = Math.Abs(5 * Math.Pow(task.Grid.points[i], 2) - 6 * task.Grid.points[i] + 7 - task.V[i - 1]);
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 1;
                chart1.ChartAreas[0].AxisX.Interval = 0.2;
                for (int i = 0; i <= n; i++)
                {
                    chart1.Series[0].Points.AddXY(task.Grid.points[i], 5 * Math.Pow(task.Grid.points[i], 2) - 6 * task.Grid.points[i] + 7);
                    chart1.Series[1].Points.AddXY(task.Grid.points[i], task.V[i]);
                }
            }
            double err = 0;
            double err_idx = 0;
            for (int i = 0; i <= n; i++)
            {
                if (err < Math.Abs(5 * Math.Pow(task.Grid.points[i], 2) - 6 * task.Grid.points[i] + 7 - task.V[i]))
                {
                    err = Math.Abs(5 * Math.Pow(task.Grid.points[i], 2) - 6 * task.Grid.points[i] + 7 - task.V[i]);
                    err_idx = i;
                }
            }
            label2.Text = "Ошибка " + err.ToString();
            label3.Text = "Узел № " + err_idx.ToString();
        }
    }
}

