using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumMethods;
namespace NumMethodsVisualization
{
    public partial class Form1 : Form
    {
        public bool otherSeries = false;
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int m = Convert.ToInt32(textBox2.Text);
            int s = Convert.ToInt32(textBox3.Text);

            if (otherSeries == true)
            {
                chart1.Series["1"].Points.Clear();
                chart1.Series["2"].Points.Clear();
                chart1.Series["3"].Points.Clear();
                chart1.Series["4"].Points.Clear();
                chart1.Series["5"].Points.Clear();
                chart1.Series["6"].Points.Clear();
                chart1.Series["7"].Points.Clear();
                chart1.Series["8"].Points.Clear();
                chart1.Series["9"].Points.Clear();
                chart1.Series["10"].Points.Clear();
            }
            ThermalCondEq eq = new TestTask();
            if (radioButton4.Checked == true) eq = new TestTask2();
            SolvingThermalCondEq task = new ExplicitMethod(eq, n, m); ;
            if (radioButton2.Checked == true)
                task = new ImplicitMethod(eq, n, m);
            if (radioButton1.Checked == true)
            {
                if (task.Tgrid.h >= task.Xgrid.h * task.Xgrid.h / 2.0)
                {
                    MessageBox.Show("Error!");
                    return;
                }
            }
            label5.Text = "k = " + task.Tgrid.h;
            label6.Text = "h = " + task.Xgrid.h;
            bool b = task.convergence();
            label4.Text = "Error = " + task.Norma();
            label8.Text = "h^2+k = " + (task.Xgrid.h * task.Xgrid.h + task.Tgrid.h).ToString();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 1;
            chart1.ChartAreas[0].AxisX.Interval = 0.2;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i <= n; i++)
            {
                chart1.Series[1].Points.AddXY(task.Xgrid.points[i], eq.analiticRes(task.Xgrid.points[i], task.Tgrid.points[s]));
                chart1.Series[0].Points.AddXY(task.Xgrid.points[i], task.result[i, s]);
            }



            int tableI = Convert.ToInt32(textBox4.Text);
            int tableJ = Convert.ToInt32(textBox5.Text);
            dataGridView1.ColumnCount = n + 1;
            dataGridView1.RowCount = tableJ - tableI + 1;
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1[j, i].Value = task.result[j, tableI + i];
                }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (label3.Visible == false && textBox3.Visible == false)
            {
                label3.Visible = true;
                textBox3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                label3.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThermalCondEq eq = new TestTask();
            if (radioButton4.Checked == true) eq = new TestTask2();
            int loc_n = 10;
            int loc_m = 2010;
            var task = new ImplicitMethod(eq, loc_n, loc_m);
            if (otherSeries == false)
            {
                chart1.Series.Add("1");
                chart1.Series.Add("2");
                chart1.Series.Add("3");
                chart1.Series.Add("4");
                chart1.Series.Add("5");
                chart1.Series.Add("6");
                chart1.Series.Add("7");
                chart1.Series.Add("8");
                chart1.Series.Add("9");
                chart1.Series.Add("10");
                chart1.Series["1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["4"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["5"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["6"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["7"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["8"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["9"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["10"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                otherSeries = true;
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series["1"].Points.Clear();
            chart1.Series["2"].Points.Clear();
            chart1.Series["3"].Points.Clear();
            chart1.Series["4"].Points.Clear();
            chart1.Series["5"].Points.Clear();
            chart1.Series["6"].Points.Clear();
            chart1.Series["7"].Points.Clear();
            chart1.Series["8"].Points.Clear();
            chart1.Series["9"].Points.Clear();
            chart1.Series["10"].Points.Clear();
            int l = (loc_m / 10) / 10;
            for (int i = 0; i <= loc_n; i++)
            {
                chart1.Series["1"].Points.AddXY(task.Xgrid.points[i], task.result[i, l]);
                chart1.Series["2"].Points.AddXY(task.Xgrid.points[i], task.result[i, 2 * l]);
                chart1.Series["3"].Points.AddXY(task.Xgrid.points[i], task.result[i, 3 * l]);
                chart1.Series["4"].Points.AddXY(task.Xgrid.points[i], task.result[i, 4 * l]);
                chart1.Series["5"].Points.AddXY(task.Xgrid.points[i], task.result[i, 5 * l]);
                chart1.Series["6"].Points.AddXY(task.Xgrid.points[i], task.result[i, 6 * l]);
                chart1.Series["7"].Points.AddXY(task.Xgrid.points[i], task.result[i, 7 * l]);
                chart1.Series["8"].Points.AddXY(task.Xgrid.points[i], task.result[i, 8 * l]);
                chart1.Series["9"].Points.AddXY(task.Xgrid.points[i], task.result[i, 9 * l]);
                chart1.Series["10"].Points.AddXY(task.Xgrid.points[i], task.result[i, 10 * l]);
            }

        }

    }
}

