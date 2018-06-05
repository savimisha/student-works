using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ReactDiff
{
    public partial class MainForm : Form
    {
        private Parameters p;
        private ReactDiffSolving task;
        private int currLayerOnPlotNum;
        public MainForm()
        {
            InitializeComponent();
            PlotActiv.Series[0].Points.Clear();
            PlotActiv.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActiv.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActiv.ChartAreas[0].AxisX.Minimum = 0;
            PlotActiv.ChartAreas[0].AxisX.Maximum = 1;
            PlotActiv.Series[0].IsVisibleInLegend = false;
            PlotActiv.Series[0].Color = Color.Green;
            PlotActiv.ChartAreas[0].AxisY.Title = "Активатор";
            PlotActiv.ChartAreas[0].AxisX.Title = "x";
            PlotActiv.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            PlotActiv.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";

            PlotIngib.Series[0].Points.Clear();
            PlotIngib.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngib.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngib.ChartAreas[0].AxisX.Minimum = 0;
            PlotIngib.ChartAreas[0].AxisX.Maximum = 1;
            PlotIngib.Series[0].IsVisibleInLegend = false;
            PlotIngib.Series[0].Color = Color.Red;
            PlotIngib.ChartAreas[0].AxisY.Title = "Ингибитор";
            PlotIngib.ChartAreas[0].AxisX.Title = "x";
            PlotIngib.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            PlotIngib.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";

            CompareTwoLayersToolStripMenuItem.Enabled = false;
            PortraitToolStripMenuItem.Enabled = false;
            PhasePortraitMultToolStripMenuItem.Enabled = false;
            PlotActiv.Series[0].Points.AddXY(0, 0);
            PlotIngib.Series[0].Points.AddXY(0, 0);
        }

        private void AllLayersCompute_Button_Click(object sender, EventArgs e)
        {
            try
            {
                LoadParameters();
            }
            catch(FormatException)
            {
                MessageBox.Show("Ошибка! Один или несколько параметров были неверно заданы.");
                return;
            }
            double tmp1, tmp2, h = 1.0 / (double)p.n;
            for (int i = 0; i <= p.n; i++)
            {
                tmp1 = p.Harmobic0[1] + p.Harmobic1[1] * Math.Cos(Math.PI * p.Harmobic1[0] * i * h) + p.Harmobic2[1] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h)
                    + p.Harmobic2[1] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h) + p.Harmobic3[1] * Math.Cos(Math.PI * p.Harmobic3[0] * i * h) +
                    p.Harmobic4[1] * Math.Cos(Math.PI * p.Harmobic4[0] * i * h) + p.Harmobic5[1] * Math.Cos(Math.PI * p.Harmobic5[0] * i * h);
                tmp2 = p.Harmobic0[2] + p.Harmobic1[2] * Math.Cos(Math.PI * p.Harmobic1[0] * i * h) + p.Harmobic2[2] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h)
                    + p.Harmobic2[2] * Math.Cos(Math.PI * p.Harmobic2[0] * i * h) + p.Harmobic3[2] * Math.Cos(Math.PI * p.Harmobic3[0] * i * h) +
                    p.Harmobic4[2] * Math.Cos(Math.PI * p.Harmobic4[0] * i * h) + p.Harmobic5[2] * Math.Cos(Math.PI * p.Harmobic5[0] * i * h);
                if (tmp1 <= 0 || tmp2 <= 0)
                {
                    MessageBox.Show("Неверное задание начальных условий. Они дожны быть положительны");
                    return;
                }
            }

            task = new ExplicitReactDiff(p);
            if (Implicit_radioButton.Checked == true) task = new ImplicitReactDiff(p);

            if (Explicit_radioButton.Checked == true)
            {
                if (p.k >= (1.0 / (double)p.n) * (1.0 / (double)p.n) / 2)
                {
                    MessageBox.Show("На выбранной сетке явная схема неустойчива. Выберите другую сетку.");
                    return;
                }
            }

            task.solve();
            double maxV1 = 0, maxV2 = 0;
            for (int j = 0; j <= p.maxStepsT; j++)
                for (int i = 0; i <= p.n; i++)
                {
                    if (maxV1 < task.V1[i, j]) maxV1 = task.V1[i, j];
                    if (maxV2 < task.V2[i, j]) maxV2 = task.V2[i, j];
                }
            PlotActiv.ChartAreas[0].AxisY.Maximum = maxV1 + 0.1;
            PlotIngib.ChartAreas[0].AxisY.Maximum = maxV2 + 0.1;
            CompareTwoLayersToolStripMenuItem.Enabled = true;
            PortraitToolStripMenuItem.Enabled = true;
            PhasePortraitMultToolStripMenuItem.Enabled = true;

            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i/((double)p.n),task.V1[i,p.maxStepsT]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, p.maxStepsT]);
            currLayerOnPlotNum = p.maxStepsT;
            CurrNumLayerTextBox.Text = currLayerOnPlotNum.ToString();
            InAllComputedLabel.Text = "Всего подсчитано: " + task.InAllComputedLayers.ToString();


        }

        private void LoadParameters()
        {
            p = new Parameters();
            try
            {
                p.lambda_1 = Double.Parse(lambda1_TextBox.Text);
                p.lambda_2 = Double.Parse(lambda2_TextBox.Text);
                p.alpha = Double.Parse(ro_TextBox.Text);
                p.betta = Double.Parse(k_TextBox.Text);
                p.gamma = Double.Parse(gamma_TextBox.Text);
                p.delta = Double.Parse(c_TextBox.Text);
                p.nu = Double.Parse(nu_TextBox.Text);
                p.n = Int32.Parse(n_TextBox.Text);
                p.maxStepsT = Int32.Parse(maxStep_TextBox.Text);
                p.k = Double.Parse(stepT_TextBox.Text);
            }
            catch (FormatException)
            {
                FormatException exc1 = new FormatException();
                throw exc1;
            }
            MaxLayerNumLabel.Text = "из " + p.maxStepsT.ToString();
            p.Harmobic0 = new double[3];
            p.Harmobic1 = new double[3];
            p.Harmobic2 = new double[3];
            p.Harmobic3 = new double[3];
            p.Harmobic4 = new double[3];
            p.Harmobic5 = new double[3];

            for (int i = 0; i < 3; i++)
            {
                p.Harmobic0[i] = 0;
                p.Harmobic1[i] = 0;
                p.Harmobic2[i] = 0;
                p.Harmobic3[i] = 0;
                p.Harmobic4[i] = 0;
                p.Harmobic5[i] = 0;
            }

            p.Harmobic0[0] = 0;
            try
            {
                p.Harmobic0[1] = Double.Parse(coeffActiv0_TextBox.Text);
                p.Harmobic0[2] = Double.Parse(coeffIngib0_TextBox.Text);

                if (Harmonic1_comboBox.Text != "" && coeffActiv1_TextBox.Text != "" && coeffIngib1_TextBox.Text != "")
                {
                    p.Harmobic1[0] = Double.Parse(Harmonic1_comboBox.Text);
                    p.Harmobic1[1] = Double.Parse(coeffActiv1_TextBox.Text);
                    p.Harmobic1[2] = Double.Parse(coeffIngib1_TextBox.Text);
                }

                if (Harmonic2_comboBox.Text != "" && coeffActiv2_TextBox.Text != "" && coeffIngib2_TextBox.Text != "")
                {
                    p.Harmobic2[0] = Double.Parse(Harmonic2_comboBox.Text);
                    p.Harmobic2[1] = Double.Parse(coeffActiv2_TextBox.Text);
                    p.Harmobic2[2] = Double.Parse(coeffIngib2_TextBox.Text);
                }

                if (Harmonic3_comboBox.Text != "" && coeffActiv3_TextBox.Text != "" && coeffIngib3_TextBox.Text != "")
                {
                    p.Harmobic3[0] = Double.Parse(Harmonic3_comboBox.Text);
                    p.Harmobic3[1] = Double.Parse(coeffActiv3_TextBox.Text);
                    p.Harmobic3[2] = Double.Parse(coeffIngib3_TextBox.Text);
                }

                if (Harmonic4_comboBox.Text != "" && coeffActiv4_TextBox.Text != "" && coeffIngib4_TextBox.Text != "")
                {
                    p.Harmobic4[0] = Double.Parse(Harmonic4_comboBox.Text);
                    p.Harmobic4[1] = Double.Parse(coeffActiv4_TextBox.Text);
                    p.Harmobic4[2] = Double.Parse(coeffIngib4_TextBox.Text);
                }

                if (Harmonic5_comboBox.Text != "" && coeffActiv5_TextBox.Text != "" && coeffIngib5_TextBox.Text != "")
                {
                    p.Harmobic5[0] = Double.Parse(Harmonic5_comboBox.Text);
                    p.Harmobic5[1] = Double.Parse(coeffActiv5_TextBox.Text);
                    p.Harmobic5[2] = Double.Parse(coeffIngib5_TextBox.Text);
                }
            }
            catch (FormatException)
            {
                FormatException exc2 = new FormatException();
                throw exc2;
            }

        }

        private void PrevLayerButton_Click(object sender, EventArgs e)
        {
            if (p == null || task == null) return;
            int step;
            try
            {
                step = Int32.Parse(StepLayerTextBox.Text);
            }
            catch (FormatException)
            {
                step = 1;
            }
            if (currLayerOnPlotNum - step < 0) return;
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, currLayerOnPlotNum-step]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, currLayerOnPlotNum-step]);
            currLayerOnPlotNum -= step;
            CurrNumLayerTextBox.Text = currLayerOnPlotNum.ToString();

        }

        private void CurrNumLayerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (p == null || task == null) return;
            int newCurrLayerNum;
            try
            {
                newCurrLayerNum = Int32.Parse(CurrNumLayerTextBox.Text);
            }
            catch (FormatException)
            {
                newCurrLayerNum = currLayerOnPlotNum;
            }
            if (newCurrLayerNum < 0 || newCurrLayerNum > p.maxStepsT) return;
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, newCurrLayerNum]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, newCurrLayerNum]);
            currLayerOnPlotNum = newCurrLayerNum;
        }

        private void NextLayerButton_Click(object sender, EventArgs e)
        {
            if (p == null || task == null) return;
            int step;
            try
            {
                step = Int32.Parse(StepLayerTextBox.Text);
            }

            catch (FormatException)
            {
                step = 1;
            }
            if (currLayerOnPlotNum + step > p.maxStepsT) return;
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, currLayerOnPlotNum + step]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, currLayerOnPlotNum + step]);
            currLayerOnPlotNum += step;
            CurrNumLayerTextBox.Text = currLayerOnPlotNum.ToString();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CompareTwoLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompareTwoLayers form = new CompareTwoLayers(p, task);
            form.Show();

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Решение уравнения реакции диффузии.");
        }

        private void PortraitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PhasePortrait(p, task);
            form.Show();
        }

        private void PhasePortraitMultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PhasePortraitMult(p, task);
            form.Show();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            try
            {
                p.maxStepsT = Int32.Parse(maxStep_TextBox.Text);
            }
            catch (FormatException)
            {
                return;
            }
            task.MaxStepsT = p.maxStepsT;
            task.ContinueSolving();
            double maxV1 = 0, maxV2 = 0;
            for (int j = 0; j <= p.maxStepsT; j++)
                for (int i = 0; i <= p.n; i++)
                {
                    if (maxV1 < task.V1[i, j]) maxV1 = task.V1[i, j];
                    if (maxV2 < task.V2[i, j]) maxV2 = task.V2[i, j];
                }
            PlotActiv.ChartAreas[0].AxisY.Maximum = maxV1 + 0.1;
            PlotIngib.ChartAreas[0].AxisY.Maximum = maxV2 + 0.1;
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, p.maxStepsT]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, p.maxStepsT]);
            currLayerOnPlotNum = p.maxStepsT;
            CurrNumLayerTextBox.Text = currLayerOnPlotNum.ToString();
            InAllComputedLabel.Text = "Всего подсчитано: " + task.InAllComputedLayers.ToString();
            MaxLayerNumLabel.Text = "из " + p.maxStepsT.ToString();
        }
    }
}
