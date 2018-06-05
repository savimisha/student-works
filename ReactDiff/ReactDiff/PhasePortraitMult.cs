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
      public partial class PhasePortraitMult : Form
    {
        private Parameters p;
        private ReactDiffSolving task;
        private int x = 1;
        private double maxV1 = 0, maxV2 = 0;
        public PhasePortraitMult(Parameters p, ReactDiffSolving task)
        {
            this.p = p;
            this.task = task;
            InitializeComponent();
            Plot.Series[0].Points.Clear();
            Plot.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Plot.Series[0].IsVisibleInLegend = false;
            Plot.Series[0].Color = Color.Blue;
            Plot.ChartAreas[0].AxisY.Title = "Активатор";
            Plot.ChartAreas[0].AxisX.Title = "Ингибитор";
            for (int j = 0; j <= p.maxStepsT; j++)
                for (int i = 0; i <= p.n; i++)
                {
                    if (maxV1 < task.V1[i, j]) maxV1 = task.V1[i, j];
                    if (maxV2 < task.V2[i, j]) maxV2 = task.V2[i, j];
                }
            Plot.ChartAreas[0].AxisX.Minimum = 0;
            Plot.ChartAreas[0].AxisY.Minimum = 0;
            Plot.ChartAreas[0].AxisX.Maximum = maxV2 + 0.1;
            Plot.ChartAreas[0].AxisY.Maximum = maxV1 + 0.1;
            Plot.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            Plot.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";
            progressBar1.Minimum = 0;
            progressBar1.Maximum = p.maxStepsT;

   
        }
        private void addXY(double x, double y)
        {
            Plot.Series[0].Points.AddXY(x, y);
        }
        private void addInProgressBar(int a)
        {
            progressBar1.Value = a;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= p.maxStepsT; i += p.maxStepsT/100)
            {
                if (x <= p.n && x >= 0)
                {
                    try
                    {
                        if (Plot.InvokeRequired) Plot.Invoke(new Action<double, double>(addXY), task.V2[1, i], task.V1[1, i]);
                        if (progressBar1.InvokeRequired) progressBar1.Invoke(new Action<int>(addInProgressBar), i);
                    }
                    catch(ObjectDisposedException)
                    {
                        return;
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                try
                {
                    x = Int32.Parse(textBox1.Text);
                }
                catch (FormatException)
                {
                    return;
                }
                Plot.Series[0].Points.Clear();
                backgroundWorker1.RunWorkerAsync();
            }
        }

    }
}
