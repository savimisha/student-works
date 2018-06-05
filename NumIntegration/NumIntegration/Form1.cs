using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumIntegration
{
    public partial class Form1 : Form
    {
        private double eps = 1e-8;
        private int n = 10;
        private double X = 0.5;
        private f2 f2;
        public Form1()
        {
            InitializeComponent();
            Plot.Series[0].Points.Clear();
            Plot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.Series[0].IsVisibleInLegend = false;
            Plot.Series[0].Color = Color.Green;
            radioButton1.Checked = true;
            f2 = new f2();
            Random r = new Random();
            double[] A = new double[14];
            double[] B = new double[14];
            double alpha = r.NextDouble();
            for (int i = 0; i < 14; i++)
            {
                A[i] = 2.0 * r.NextDouble() - 1.0;
                B[i] = 2.0 * r.NextDouble() - 1.0;
            }
            f2.A = A;
            f2.B = B;
            f2.alpha = alpha;
            f2.X = X;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                eps = Double.Parse(textBox1.Text);
                n = Int32.Parse(textBox2.Text);
                X = Double.Parse(textBox3.Text);
            }
            catch(FormatException)
            {
                return;
            }
            Integration task = new IntegrationRectangle();
            if (radioButton2.Checked) task = new IntegrationTrapeze();
            if (radioButton3.Checked) task = new IntegrationSimpson();
            if (!checkBox1.Checked)
            {
                Function f1 = new f1();
                if (radioButton5.Checked) f1 = new f3();
                if (radioButton6.Checked) f1 = new f4();
                int N = 10;
                double a = 1, b = 2;
                double h = (b - a) / (double)N;
                Plot.Series[0].Points.Clear();
                for (int i = 0; i <= N; i++)
                    Plot.Series[0].Points.AddXY(a + i * h, f1.value(a + i * h));
                double value = task.compute(a, b, f1, n);
                ValueLabel.Text = "Численное значение: " + value.ToString();
                ErrorLabel.Text = "Погрешность: " + Math.Abs(value - (0.25 * Math.Log(2, Math.E) + 0.5)).ToString();
                if (radioButton5.Checked) ErrorLabel.Text = "Погрешность: " + Math.Abs(value - (0.25 * Math.Log(2, Math.E) + 0.5 + 0.1*(Math.Sin(20) - Math.Sin(10)))).ToString();
                if (radioButton6.Checked) ErrorLabel.Text = "Погрешность: " + Math.Abs(value - (0.25 * Math.Log(2, Math.E) + 0.5 + 0.01 * (Math.Sin(200) - Math.Sin(100)))).ToString();
            }
            if (checkBox1.Checked)
            {
                Plot.Series[0].Points.Clear();
                int N = 100;
                double a = -Math.PI/2, b = Math.PI/2;
                double h = (b - a) / N;
                f2.X = X;
                for (int i = 0; i <= N; i++)
                    Plot.Series[0].Points.AddXY(a + i * h, f2.value(a + i * h));
                var MAQ = new MAQ(task);
                double res = MAQ.compute(a,b,f2,eps);
                ValueLabel.Text = "Численное значение: " + res.ToString();
                ErrorLabel.Text = "";
                label4.Text = "Количество рекурсий: " + MAQ.Depth.ToString();
                label5.Text = "Alpha = " + f2.alpha.ToString();

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Random r = new Random();
            double[] A = new double[14];
            double[] B = new double[14];
            double alpha = r.NextDouble();
            for (int i = 0; i < 14; i++)
            {
                A[i] = 2.0 * r.NextDouble() - 1.0;
                B[i] = 2.0 * r.NextDouble() - 1.0;
            }
            f2.A = A;
            f2.B = B;
            f2.alpha = alpha;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                eps = Double.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                return;
            }
            double a = -Math.PI / 2, b = Math.PI / 2;
            double h = 0.1;
            Integration task = new IntegrationRectangle();
            if (radioButton2.Checked) task = new IntegrationTrapeze();
            if (radioButton3.Checked) task = new IntegrationSimpson();
            var f3 = new f2();
            f3.A = f2.A;
            f3.B = f2.B;
            f3.alpha = f2.alpha;
            f3.X = 0;
            var MAQ = new MAQ(task);
            int[] depth = new int[11];
            for (int i = 0; i <= 10; i++)
            {
                MAQ.compute(a, b, f3, eps);
                depth[i] = MAQ.Depth;
                MAQ.Depth = 0;
                f3.X += h;
            }
            var form = new PlotForm(depth);
            form.Show();

        }
    }
}
