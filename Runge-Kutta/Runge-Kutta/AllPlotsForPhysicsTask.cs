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
    public partial class AllPlotsForPhysicsTask : Form
    {
        public AllPlotsForPhysicsTask(SDE task)
        {
            InitializeComponent();
            Plot1.Series[0].Points.Clear();
            Plot1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot1.Series[0].IsVisibleInLegend = false;
            Plot1.Series[0].Color = Color.Green;
            Plot1.ChartAreas[0].AxisY.Title = "v";
            Plot1.ChartAreas[0].AxisX.Title = "x";
            Plot1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            Plot1.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";

            Plot2.Series[0].Points.Clear();
            Plot2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot2.Series[0].IsVisibleInLegend = false;
            Plot2.Series[0].Color = Color.Green;
            Plot2.ChartAreas[0].AxisY.Title = "v'";
            Plot2.ChartAreas[0].AxisX.Title = "x";
            Plot2.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            Plot2.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";

            PhasePlot.Series[0].Points.Clear();
            PhasePlot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PhasePlot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            PhasePlot.Series[0].IsVisibleInLegend = false;
            PhasePlot.Series[0].Color = Color.Green;
            PhasePlot.ChartAreas[0].AxisY.Title = "v";
            PhasePlot.ChartAreas[0].AxisX.Title = "v'";
            PhasePlot.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            PhasePlot.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";


            for (int i = 0; i <= task.IterationsCount; i++)
            {
                Plot1.Series[0].Points.AddXY(task.xi[i], task.V[i, 1]);
                Plot2.Series[0].Points.AddXY(task.xi[i], task.V[i, 0]);
                PhasePlot.Series[0].Points.AddXY(task.V[i, 0], task.V[i, 1]);
            }
        }
    }
}
