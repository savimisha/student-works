using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReactDiff
{
    public partial class PhasePortrait : Form
    {
        private Parameters p;
        private ReactDiffSolving task;
        private int x;
        private int step;
        public PhasePortrait(Parameters p, ReactDiffSolving task)
        {
            InitializeComponent();
            this.p = p;
            this.task = task;
            Plot.Series[0].Points.Clear();
            Plot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.Series[0].IsVisibleInLegend = false;
            Plot.Series[0].Color = Color.Blue;
            Plot.ChartAreas[0].AxisY.Title = "Активатор";
            Plot.ChartAreas[0].AxisX.Title = "Ингибитор";

            PlotV1.Series[0].Points.Clear();
            PlotV1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotV1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotV1.Series[0].IsVisibleInLegend = false;
            PlotV1.Series[0].Color = Color.Green;
            PlotV1.ChartAreas[0].AxisY.Title = "Активатор";
            PlotV1.ChartAreas[0].AxisX.Title = "t";

            PlotV2.Series[0].Points.Clear();
            PlotV2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotV2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotV2.Series[0].IsVisibleInLegend = false;
            PlotV2.Series[0].Color = Color.Red;
            PlotV2.ChartAreas[0].AxisY.Title = "Ингибитор";
            PlotV2.ChartAreas[0].AxisX.Title = "t";

            x = 1;
            step = p.maxStepsT / 100;
            StepLayerTextBox.Text = (p.maxStepsT / 100).ToString();
            XValuetextBox.Text = "1";

        }

        private void XValuetextBox_TextChanged(object sender, EventArgs e)
        {
            int new_x;
            try
            {
                new_x = Int32.Parse(XValuetextBox.Text);
            }
            catch (FormatException)
            {
                return;
            }
            if (new_x < 0 || new_x > p.n)
            {
                MessageBox.Show("Ошибка! x должен быть в пределах от 0 до " + p.n.ToString());
                return;
            }

            x = new_x;
            Plot.Series[0].Points.Clear();
            PlotV1.Series[0].Points.Clear();
            PlotV2.Series[0].Points.Clear();
            for (int i = 0; i <= p.maxStepsT; i += step)
            {
                Plot.Series[0].Points.AddXY(task.V2[x, i], task.V1[x, i]);
                PlotV1.Series[0].Points.AddXY(i * p.k, task.V1[x, i]);
                PlotV2.Series[0].Points.AddXY(i * p.k, task.V2[x, i]);
            }
        }

        private void StepLayerTextBox_TextChanged(object sender, EventArgs e)
        {
            int new_step;
            try
            {
                new_step = Int32.Parse(StepLayerTextBox.Text);
            }
            catch (FormatException)
            {
                return;
            }
            if (new_step < 0 || new_step > p.maxStepsT)
            {
                MessageBox.Show("Ошибка! Шаг должен быть в пределах от 0 до " + p.maxStepsT.ToString());
                return;
            }

            step = new_step;
            Plot.Series[0].Points.Clear();
            PlotV1.Series[0].Points.Clear();
            PlotV2.Series[0].Points.Clear();

            for (int i = 0; i <= p.maxStepsT; i += step)
            {
                Plot.Series[0].Points.AddXY(task.V2[x, i], task.V1[x, i]);
                PlotV1.Series[0].Points.AddXY(i * p.k, task.V1[x, i]);
                PlotV2.Series[0].Points.AddXY(i * p.k, task.V2[x, i]);
            }
        }

    }
}
