using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IterativeMethods
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fxy Fxy = new FxyTest_7();
            fxy Uxy = new UxyTest_7();
            var rect = new Rect(0, 2, 0, 1);
            var rect2 = new Rect(1, 2, 0.5, 1); 
            // 2 var rect = new Rect(-1, 1, -1, 1);
            // 4 var rect = new Rect(1, 2, 2, 3);
            double w;
            int n = Convert.ToInt32(textBox1.Text), m = Convert.ToInt32(textBox2.Text);
            IterativeMethod method = new JacobiMethod(Fxy, Uxy, rect, n, m);
            if (radioButton2.Checked == true) method = new SeidelMethod(Fxy, Uxy, rect, n, m);
            if (radioButton4.Checked == true) method = new MinimalDiscrepancyMethod(Fxy, Uxy, rect, n, m);
            if (radioButton5.Checked == true) method = new MinimalDiscrepancyMethod2(Fxy, Uxy, rect, rect2, n, m);
            if (radioButton3.Checked == true)
            {
                w = Double.Parse(textBox5.Text);
                method = new SOR(Fxy, Uxy, rect, n, m, w);
            }
            if (textBox3.Text != "") method.Eps = Double.Parse(textBox3.Text);
            if (textBox4.Text != "") method.Nmax = Int32.Parse(textBox4.Text);
            method.run();
            double[,] v = method.V;
            table.RowCount = m + 1;
            table.ColumnCount = n + 1;
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                {
                    table[i, j].Value = v[i, m-j];
                }
            double temp;
            double max_err = 0;
            if (radioButton5.Checked != true)
            {
                for (int i = 0; i < n + 1; i++)
                    for (int j = 0; j < m + 1; j++)
                    {
                        temp = Uxy.val(rect.a + i * method.H, rect.c + j * method.K);
                        if (max_err < Math.Abs(temp - v[i, j])) max_err = Math.Abs(temp - v[i, j]);
                    }
            }
            if (radioButton5.Checked == true)
            {
                temp = 0;
                max_err = 0;
                int I = Convert.ToInt32((rect2.a - rect.a) * n / (rect.b - rect.a));
                int J = Convert.ToInt32((rect2.c - rect.c) * m / (rect.d - rect.c));

                for (int j = 1; j <= J; j++)
                    for (int i = 1; i <= n; i++)
                    {
                        temp = Uxy.val(rect.a + i * method.H, rect.c + j * method.K);
                        if (max_err < Math.Abs(temp - v[i, j])) max_err = Math.Abs(temp - v[i, j]);
                    }

                for (int j = J; j <= m; j++)
                    for (int i = 1; i <= I; i++)
                    {
                        temp = Uxy.val(rect.a + i * method.H, rect.c + j * method.K);
                        if (max_err < Math.Abs(temp - v[i, j])) max_err = Math.Abs(temp - v[i, j]);
                    }
            }

            label4.Text = "Итерации: " + method.S.ToString();
            label5.Text = "Точность: " + method.Eps_max.ToString();
            label6.Text = "Погрешность: " + max_err.ToString();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fxy Fxy = new FxyBase_7();
            fxy Uxy = new UxyBase_7();
            var rect = new Rect(0, 2, 0, 1);
            var rect2 = new Rect(1, 2, 0.5, 1); 
            // 2 var rect = new Rect(-1, 1, -1, 1);
            // 4 var rect = new Rect(1, 2, 2, 3);
            double w;
            int n = Convert.ToInt32(textBox1.Text), m = Convert.ToInt32(textBox2.Text);
            IterativeMethod method = new JacobiMethod(Fxy, Uxy, rect, n, m);
            if (radioButton2.Checked == true) method = new SeidelMethod(Fxy, Uxy, rect, n, m);
            if (radioButton4.Checked == true) method = new MinimalDiscrepancyMethod(Fxy, Uxy, rect, n, m);
            if (radioButton5.Checked == true)
            {
                Uxy = new UxyBase_7_2();
                method = new MinimalDiscrepancyMethod2(Fxy, Uxy, rect, rect2, n, m);
            }
            if (radioButton3.Checked == true)
            {
                w = Double.Parse(textBox5.Text);
                method = new SOR(Fxy, Uxy, rect, n, m, w);
            }
            if (textBox3.Text != "") method.Eps = Double.Parse(textBox3.Text);
            if (textBox4.Text != "") method.Nmax = Int32.Parse(textBox4.Text);


            method.run();
            var v = new double[n+1,m+1];
            for (int i = 0; i < n + 1; i++)
                for (int j = 0; j < m + 1; j++)
                    v[i, j] = method.V[i, j];
            double s = method.S;
            double Eps_max = method.Eps_max;
            table.RowCount = m + 1;
            table.ColumnCount = n + 1;
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                {
                    table[i, j].Value = v[i, m - j];
                }
            method = new JacobiMethod(Fxy, Uxy, rect, 2*n, 2*m);
            if (radioButton2.Checked == true) method = new SeidelMethod(Fxy, Uxy, rect, 2*n, 2*m);
            if (radioButton4.Checked == true) method = new MinimalDiscrepancyMethod(Fxy, Uxy, rect, 2*n, 2*m);
            if (radioButton5.Checked == true)
            {
                Uxy = new UxyBase_7_2();
                method = new MinimalDiscrepancyMethod2(Fxy, Uxy, rect, rect2, 2 * n, 2 * m);
            }
            if (radioButton3.Checked == true)
            {
                w = Double.Parse(textBox5.Text);
                method = new SOR(Fxy, Uxy, rect, 2*n, 2*m, w);
            }
            method.run();
            double[,] v1 = method.V;
            
            double max_err = 0;
            if (radioButton5.Checked != true)
            {
                for (int i = 0; i < n + 1; i++)
                    for (int j = 0; j < m + 1; j++)
                    {
                        if (max_err < Math.Abs(v[i, j] - v1[2 * i, 2 * j])) max_err = Math.Abs(v[i, j] - v1[2 * i, 2 * j]);
                    }
            }

            if (radioButton5.Checked == true)
            {
                max_err = 0;
                int I = Convert.ToInt32((rect2.a - rect.a) * n / (rect.b - rect.a));
                int J = Convert.ToInt32((rect2.c - rect.c) * m / (rect.d - rect.c));

                for (int j = 1; j <= J; j++)
                    for (int i = 1; i <= n; i++)
                    {
                        if (max_err < Math.Abs(v[i, j] - v1[2 * i, 2 * j])) max_err = Math.Abs(v[i, j] - v1[2 * i, 2 * j]);
                    }

                for (int j = J; j <= m; j++)
                    for (int i = 1; i <= I; i++)
                    {
                        if (max_err < Math.Abs(v[i, j] - v1[2 * i, 2 * j])) max_err = Math.Abs(v[i, j] - v1[2 * i, 2 * j]);
                    }
            }


            label4.Text = "Итерации: " + s.ToString();
            label5.Text = "Точность: " + Eps_max.ToString();
            label6.Text = "Погрешность: " + max_err.ToString();

        }
    }
}
