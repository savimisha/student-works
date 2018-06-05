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
    public partial class PlotForm : Form
    {
        private int[] depth;
        public PlotForm(int[] depth)
        {
            InitializeComponent();
            this.depth = depth;
            Plot.Series[0].Points.Clear();
            Plot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.Series[0].IsVisibleInLegend = false;
            Plot.Series[0].Color = Color.Red;

            for (int i = 0; i <= 10; i++)
            {
                Plot.Series[0].Points.AddXY(i * 0.1, depth[i]);
            }

        }
    }
}
