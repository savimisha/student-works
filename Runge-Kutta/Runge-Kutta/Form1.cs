using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Runge_Kutta
{
    public partial class Form1 : Form
    {
        bool TaskSolved = false;
        SDE task;
        public Form1()
        {
            InitializeComponent();
            Plot.Series[0].Points.Clear();
            Plot.Series[1].Points.Clear();
            Plot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.Series[0].IsVisibleInLegend = false;
            Plot.Series[0].Color = Color.Green;
            Plot.Series[1].IsVisibleInLegend = false;
            Plot.Series[1].Color = Color.Red;
            Plot.ChartAreas[0].AxisY.Title = "y";
            Plot.ChartAreas[0].AxisX.Title = "x";
            Plot.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            Plot.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            double h, eps, X, u0;
            int Nmax;
            try
            {
                h = Double.Parse(StepTextBox.Text);
                eps = Double.Parse(EpsTextBox.Text);
                X = Double.Parse(XmaxTextBox.Text);
                Nmax = Int32.Parse(NmaxTextBox.Text);
                u0 = Double.Parse(InitBoundtextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Один из параметров задан неверно!");
                return;
            }
            Test7 test7 = new Test7();
            double x0 = 0;
            ODE task = new RK4(u0, test7, x0, X, h, Nmax, eps);
            if (ControlCondcheckBox.Checked) task.CondControl = true;
            if (ControlErrorcheckBox.Checked) task.solveWithControlError();
            if (!ControlErrorcheckBox.Checked) task.solve();

            table.ColumnCount = 11;
            table.RowCount = task.IterationsCount + 2;

            table[0, 0].Value = "i";
            table[1, 0].Value = "Xi";
            table[2, 0].Value = "Vi";
            table[3, 0].Value = "V2i";
            table[4, 0].Value = "|Vi - V2i|";
            table[5, 0].Value = "ОЛП";
            table[6, 0].Value = "hi";
            table[7, 0].Value = "C1";
            table[8, 0].Value = "C2";
            table[9, 0].Value = "Ui";
            table[10, 0].Value = "|Ui - Vi|";
            double[] pogr = new double[task.IterationsCount + 1];
            pogr[0] = 0;
            double maxPogr = 0;
            for (int i = 0; i <= task.IterationsCount; i++)
            {
                if (maxPogr < Math.Abs(Math.Exp(-3.5 * task.xi[i]) - task.V[i])) maxPogr = Math.Abs(Math.Exp(-3.5 * task.xi[i]) - task.V[i]);
            }


            for (int i = 1; i < table.RowCount; i++)
            {
                table[0, i].Value = i - 1;
                table[1, i].Value = task.xi[i - 1];
                table[2, i].Value = task.V[i - 1];
                table[3, i].Value = task.V2[i - 1];
                table[4, i].Value = Math.Abs(task.V[i - 1] - task.V2[i - 1]).ToString("e3") ;
                table[5, i].Value = (Math.Pow(2, (double)task.P) * task.S[i - 1]).ToString("e3");
                table[6, i].Value = task.H[i - 1];
                table[7, i].Value = task.DoubleCount[i - 1];
                table[8, i].Value = task.HalfCount[i - 1];
                table[9, i].Value = Math.Exp(-3.5 * task.xi[i - 1]);
                table[10, i].Value = Math.Abs(Math.Exp(-3.5 * task.xi[i - 1]) - task.V[i - 1]).ToString("e3");
            }
            Plot.Series[0].Points.Clear();
            Plot.Series[1].Points.Clear();
            Plot.ChartAreas[0].AxisX.Minimum = 0;
            Plot.ChartAreas[0].AxisX.Maximum = task.xi[task.IterationsCount];
            for (int i = 0; i <= task.IterationsCount; i++)
            {
                Plot.Series[0].Points.AddXY(task.xi[i], task.V[i]);
                Plot.Series[1].Points.AddXY(task.xi[i], Math.Exp(-3.5 * task.xi[i]));
            }
            label1.Text = "Error = " + maxPogr.ToString("e3");
            if (ControlErrorcheckBox.Checked) label1.Text = ""; 
        }

        private void BaseFirstButton_Click(object sender, EventArgs e)
        {
            double h, eps, X, u0;
            int Nmax;
            try
            {
                h = Double.Parse(StepTextBox.Text);
                eps = Double.Parse(EpsTextBox.Text);
                X = Double.Parse(XmaxTextBox.Text);
                Nmax = Int32.Parse(NmaxTextBox.Text);
                u0 = Double.Parse(InitBoundtextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Один из параметров задан неверно!");
                return;
            }
            BaseFirst7 baseFirst7 = new BaseFirst7();
            double x0 = 0;
            ODE task = new RK4(u0, baseFirst7, x0, X, h, Nmax, eps);
            if (ControlCondcheckBox.Checked) task.CondControl = true;
            if (ControlErrorcheckBox.Checked) task.solveWithControlError();
            if (!ControlErrorcheckBox.Checked) task.solve();

            table.ColumnCount = 9;
            table.RowCount = task.IterationsCount + 2;

            table[0, 0].Value = "i";
            table[1, 0].Value = "Xi";
            table[2, 0].Value = "Vi";
            table[3, 0].Value = "V2i";
            table[4, 0].Value = "|Vi - V2i|";
            table[5, 0].Value = "ОЛП";
            table[6, 0].Value = "hi";
            table[7, 0].Value = "C1";
            table[8, 0].Value = "C2";

            for (int i = 1; i < table.RowCount; i++)
            {
                table[0, i].Value = i - 1;
                table[1, i].Value = task.xi[i - 1];
                table[2, i].Value = task.V[i - 1];
                table[3, i].Value = task.V2[i - 1];
                table[4, i].Value = Math.Abs(task.V[i - 1] - task.V2[i - 1]).ToString("e3");
                table[5, i].Value = (Math.Pow(2, (double)task.P) * task.S[i - 1]).ToString("e3");
                table[6, i].Value = task.H[i - 1];
                table[7, i].Value = task.DoubleCount[i - 1];
                table[8, i].Value = task.HalfCount[i - 1];
            }
            Plot.Series[0].Points.Clear();
            Plot.Series[1].Points.Clear();
            Plot.ChartAreas[0].AxisX.Minimum = 0;
            Plot.ChartAreas[0].AxisX.Maximum = task.xi[task.IterationsCount];
            for (int i = 0; i <= task.IterationsCount; i++)
            {
                Plot.Series[0].Points.AddXY(task.xi[i], task.V[i]);
            }
        }

        private void PhysicsButton_Click(object sender, EventArgs e)
        {
            double h, eps, X, u00, u01;
            int Nmax;
            try
            {
                h = Double.Parse(StepTextBox.Text);
                eps = Double.Parse(EpsTextBox.Text);
                X = Double.Parse(XmaxTextBox.Text);
                Nmax = Int32.Parse(NmaxTextBox.Text);
                u00 = Double.Parse(InitBounds2textBox.Text);
                u01 = Double.Parse(InitBounds1textBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Один из параметров задан неверно!");
                return;
            }
            PhysicsFunction1 physicsFunction1 = new PhysicsFunction1();
            PhysicsFunction2 physicsFunction2 = new PhysicsFunction2();
            double x0 = 0;
            task = new RK4System(u00, u01, physicsFunction1, physicsFunction2, x0, X, h, Nmax, eps);
            if (ControlCondcheckBox.Checked) task.CondControl = true;
            if (ControlErrorcheckBox.Checked) task.solveWithControlError();
            if (!ControlErrorcheckBox.Checked) task.solve();

            table.ColumnCount = 9;
            table.RowCount = task.IterationsCount + 2;

            table[0, 0].Value = "i";
            table[1, 0].Value = "Xi";
            table[2, 0].Value = "Vi";
            table[3, 0].Value = "V2i";
            table[4, 0].Value = "|Vi - V2i|";
            table[5, 0].Value = "ОЛП";
            table[6, 0].Value = "hi";
            table[7, 0].Value = "C1";
            table[8, 0].Value = "C2";

            for (int i = 1; i < table.RowCount; i++)
            {
                    table[0, i].Value = i - 1;
                    table[1, i].Value = task.xi[i - 1];
                    table[2, i].Value = task.V[i - 1, 1];
                    table[3, i].Value = task.V2[i - 1, 1];
                    table[4, i].Value = Math.Abs(task.V[i - 1, 1] - task.V2[i - 1, 1]).ToString("e3");
                    table[5, i].Value = (Math.Pow(2, (double)task.P) * Math.Max(task.S[i - 1, 0], task.S[i - 1, 1])).ToString("e3");
                    table[6, i].Value = task.H[i - 1];
                    table[7, i].Value = task.DoubleCount[i - 1];
                    table[8, i].Value = task.HalfCount[i - 1];
            }
            Plot.Series[0].Points.Clear();
            Plot.Series[1].Points.Clear();
            Plot.ChartAreas[0].AxisX.Minimum = 0;
            Plot.ChartAreas[0].AxisX.Maximum = task.xi[task.IterationsCount];
            for (int i = 0; i <= task.IterationsCount; i++)
            {
                Plot.Series[0].Points.AddXY(task.xi[i], task.V[i, 1]);
            }
            TaskSolved = true;

        }

        private void AllPlots_Click(object sender, EventArgs e)
        {
            if (TaskSolved)
            {
                new AllPlotsForPhysicsTask(task).Show();
            }
        }

        private void Base2Button_Click(object sender, EventArgs e)
        {
            double h, eps, X, u00, u01, a, b;
            int Nmax;
            try
            {
                h = Double.Parse(StepTextBox.Text);
                eps = Double.Parse(EpsTextBox.Text);
                X = Double.Parse(XmaxTextBox.Text);
                Nmax = Int32.Parse(NmaxTextBox.Text);
                u00 = Double.Parse(InitBounds2textBox.Text);
                u01 = Double.Parse(InitBounds1textBox.Text);
                a = Double.Parse(ParAtextBox.Text);
                b = Double.Parse(ParBtextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Один из параметров задан неверно!");
                return;
            }
            BaseSecond7F1 Function1 = new BaseSecond7F1(a, b);
            BaseSecond7F2 Function2 = new BaseSecond7F2();
            double x0 = 0;
            task = new RK4System(u00, u01, Function1, Function2, x0, X, h, Nmax, eps);
            if (ControlCondcheckBox.Checked) task.CondControl = true;
            if (ControlErrorcheckBox.Checked) task.solveWithControlError();
            if (!ControlErrorcheckBox.Checked) task.solve();

            table.ColumnCount = 9;
            table.RowCount = task.IterationsCount + 2;

            table[0, 0].Value = "i";
            table[1, 0].Value = "Xi";
            table[2, 0].Value = "Vi";
            table[3, 0].Value = "V2i";
            table[4, 0].Value = "|Vi - V2i|";
            table[5, 0].Value = "ОЛП";
            table[6, 0].Value = "hi";
            table[7, 0].Value = "C1";
            table[8, 0].Value = "C2";

            for (int i = 1; i < table.RowCount; i++)
            {
                table[0, i].Value = i - 1;
                table[1, i].Value = task.xi[i - 1];
                table[2, i].Value = task.V[i - 1, 1];
                table[3, i].Value = task.V2[i - 1, 1];
                table[4, i].Value = Math.Abs(task.V[i - 1, 1] - task.V2[i - 1, 1]).ToString("e3");
                table[5, i].Value = (Math.Pow(2, (double)task.P) * Math.Max(task.S[i - 1, 0], task.S[i - 1, 1])).ToString("e3");
                table[6, i].Value = task.H[i - 1];
                table[7, i].Value = task.DoubleCount[i - 1];
                table[8, i].Value = task.HalfCount[i - 1];
            }
            Plot.Series[0].Points.Clear();
            Plot.Series[1].Points.Clear();
            Plot.ChartAreas[0].AxisX.Minimum = 0;
            Plot.ChartAreas[0].AxisX.Maximum = task.xi[task.IterationsCount];
            for (int i = 0; i <= task.IterationsCount; i++)
            {
                Plot.Series[0].Points.AddXY(task.xi[i], task.V[i, 1]);
            }
            TaskSolved = true;

        }
    }
}
