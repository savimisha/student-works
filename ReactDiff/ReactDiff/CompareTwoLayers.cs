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
    public partial class CompareTwoLayers : Form
    {
        private Parameters p;
        private ReactDiffSolving task;
        private int layer_1, layer_2;
        public CompareTwoLayers(Parameters p, ReactDiffSolving task)
        {
            this.p = p;
            this.task = task;
            InitializeComponent();

            PlotActiv.Series[0].Points.Clear();
            PlotActiv.Series[1].Points.Clear();
            PlotActiv.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActiv.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActiv.ChartAreas[0].AxisX.Minimum = 0;
            PlotActiv.ChartAreas[0].AxisX.Maximum = 1;
            PlotActiv.Series[0].IsVisibleInLegend = false;
            PlotActiv.Series[0].Color = Color.Green;
            PlotActiv.Series[1].IsVisibleInLegend = false;
            PlotActiv.Series[1].Color = Color.Blue;
            PlotActiv.ChartAreas[0].AxisY.Title = "Активатор";

            PlotIngib.Series[0].Points.Clear();
            PlotIngib.Series[1].Points.Clear();
            PlotIngib.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngib.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngib.ChartAreas[0].AxisX.Minimum = 0;
            PlotIngib.ChartAreas[0].AxisX.Maximum = 1;
            PlotIngib.Series[0].IsVisibleInLegend = false;
            PlotIngib.Series[0].Color = Color.Red;
            PlotIngib.Series[1].IsVisibleInLegend = false;
            PlotIngib.Series[1].Color = Color.Blue;
            PlotIngib.ChartAreas[0].AxisY.Title = "Ингибитор";

            PlotActivDiff.Series[0].Points.Clear();
            PlotActivDiff.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActivDiff.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotActivDiff.ChartAreas[0].AxisX.Minimum = 0;
            PlotActivDiff.ChartAreas[0].AxisX.Maximum = 1;
            PlotActivDiff.ChartAreas[0].AxisY.Minimum = 0;
            PlotActivDiff.ChartAreas[0].AxisY.Maximum = 1e-3;
            PlotActivDiff.Series[0].IsVisibleInLegend = false;
            PlotActivDiff.Series[0].Color = Color.Black;
            PlotActivDiff.ChartAreas[0].AxisY.Title = "Разность";

            PlotIngibDiff.Series[0].Points.Clear();
            PlotIngibDiff.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngibDiff.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PlotIngibDiff.ChartAreas[0].AxisX.Minimum = 0;
            PlotIngibDiff.ChartAreas[0].AxisX.Maximum = 1;
            PlotIngibDiff.ChartAreas[0].AxisY.Minimum = 0;
            PlotIngibDiff.ChartAreas[0].AxisY.Maximum = 1e-3;
            PlotIngibDiff.Series[0].IsVisibleInLegend = false;
            PlotIngibDiff.Series[0].Color = Color.Black;
            PlotIngibDiff.ChartAreas[0].AxisY.Title = "Разность";

            Layer1TextBox.Text = (p.maxStepsT - 1).ToString();
            Layer2TextBox.Text = p.maxStepsT.ToString();
            layer_1 = p.maxStepsT - 1;
            layer_2 = p.maxStepsT;
        }

        private void Layer1TextBox_TextChanged(object sender, EventArgs e)
        {
            int new_layer_1;
            try
            {
                new_layer_1 = Int32.Parse(Layer1TextBox.Text);
            }
            catch (FormatException)
            {
                return;
            }
            layer_1 = new_layer_1;
            PlotActiv.Series[0].Points.Clear();
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[1].Points.Clear();
            PlotIngib.Series[1].Points.Clear();
            PlotActivDiff.Series[0].Points.Clear();
            PlotIngibDiff.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, layer_1]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, layer_1]);
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[1].Points.AddXY((double)i / ((double)p.n), task.V1[i, layer_2]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[1].Points.AddXY((double)i / ((double)p.n), task.V2[i, layer_2]);
            for (int i = 0; i <= p.n; i++)
                PlotActivDiff.Series[0].Points.AddXY((double)i / ((double)p.n), Math.Abs(task.V1[i, layer_1] - task.V1[i, layer_2]));
            for (int i = 0; i <= p.n; i++)
                PlotIngibDiff.Series[0].Points.AddXY((double)i / ((double)p.n), Math.Abs(task.V2[i, layer_1] - task.V2[i, layer_2]));

            double maxV1 = 0, maxV2 = 0, tmp1, tmp2;
            for (int i = 0; i <= p.n; i++)
            {
                tmp1 = Math.Abs(task.V1[i, layer_1] - task.V1[i, layer_2]);
                tmp2 = Math.Abs(task.V2[i, layer_1] - task.V2[i, layer_2]);
                if (maxV1 < tmp1) maxV1 = tmp1;
                if (maxV2 < tmp2) maxV2 = tmp2;
            }
            label3.Text = "Норма разности активатора: " + maxV1.ToString();
            label4.Text = "Норма разности ингибитора: " + maxV2.ToString();

        }

        private void Layer2TextBox_TextChanged(object sender, EventArgs e)
        {
            int new_layer_2;
            try
            {
                new_layer_2 = Int32.Parse(Layer2TextBox.Text);
            }
            catch (FormatException)
            {
                return;
            }
            layer_2 = new_layer_2;
            PlotActiv.Series[0].Points.Clear();
            PlotIngib.Series[0].Points.Clear();
            PlotActiv.Series[1].Points.Clear();
            PlotIngib.Series[1].Points.Clear();
            PlotActivDiff.Series[0].Points.Clear();
            PlotIngibDiff.Series[0].Points.Clear();
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[0].Points.AddXY((double)i / ((double)p.n), task.V1[i, layer_1]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[0].Points.AddXY((double)i / ((double)p.n), task.V2[i, layer_1]);
            for (int i = 0; i <= p.n; i++)
                PlotActiv.Series[1].Points.AddXY((double)i / ((double)p.n), task.V1[i, layer_2]);
            for (int i = 0; i <= p.n; i++)
                PlotIngib.Series[1].Points.AddXY((double)i / ((double)p.n), task.V2[i, layer_2]);
            for (int i = 0; i <= p.n; i++)
                PlotActivDiff.Series[0].Points.AddXY((double)i / ((double)p.n), Math.Abs(task.V1[i, layer_1] - task.V1[i, layer_2]));
            for (int i = 0; i <= p.n; i++)
                PlotIngibDiff.Series[0].Points.AddXY((double)i / ((double)p.n), Math.Abs(task.V2[i, layer_1] - task.V2[i, layer_2]));
            double maxV1 = 0, maxV2 = 0, tmp1, tmp2;
            for (int i = 0; i <= p.n; i++)
            {
                tmp1 = Math.Abs(task.V1[i, layer_1] - task.V1[i, layer_2]);
                tmp2 = Math.Abs(task.V2[i, layer_1] - task.V2[i, layer_2]);
                if (maxV1 < tmp1) maxV1 = tmp1;
                if (maxV2 < tmp2) maxV2 = tmp2;
            }
            label3.Text = "Норма разности активатора: " + maxV1.ToString();
            label4.Text = "Норма разности ингибитора: " + maxV2.ToString();
        }
    }
}
