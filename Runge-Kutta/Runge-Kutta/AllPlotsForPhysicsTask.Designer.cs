namespace Runge_Kutta
{
    partial class AllPlotsForPhysicsTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Plot1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Plot2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PhasePlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Plot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Plot2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhasePlot)).BeginInit();
            this.SuspendLayout();
            // 
            // Plot1
            // 
            chartArea1.Name = "ChartArea1";
            this.Plot1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Plot1.Legends.Add(legend1);
            this.Plot1.Location = new System.Drawing.Point(2, 3);
            this.Plot1.Name = "Plot1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Plot1.Series.Add(series1);
            this.Plot1.Size = new System.Drawing.Size(341, 256);
            this.Plot1.TabIndex = 0;
            this.Plot1.Text = "Plot1";
            // 
            // Plot2
            // 
            chartArea2.Name = "ChartArea1";
            this.Plot2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.Plot2.Legends.Add(legend2);
            this.Plot2.Location = new System.Drawing.Point(349, 3);
            this.Plot2.Name = "Plot2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.Plot2.Series.Add(series2);
            this.Plot2.Size = new System.Drawing.Size(341, 256);
            this.Plot2.TabIndex = 1;
            this.Plot2.Text = "Plot2";
            // 
            // PhasePlot
            // 
            chartArea3.Name = "ChartArea1";
            this.PhasePlot.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.PhasePlot.Legends.Add(legend3);
            this.PhasePlot.Location = new System.Drawing.Point(2, 265);
            this.PhasePlot.Name = "PhasePlot";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.PhasePlot.Series.Add(series3);
            this.PhasePlot.Size = new System.Drawing.Size(341, 256);
            this.PhasePlot.TabIndex = 2;
            this.PhasePlot.Text = "PhasePlot";
            // 
            // AllPlotsForPhysicsTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 529);
            this.Controls.Add(this.PhasePlot);
            this.Controls.Add(this.Plot2);
            this.Controls.Add(this.Plot1);
            this.Name = "AllPlotsForPhysicsTask";
            this.Text = "AllPlotsForPhysicsTask";
            ((System.ComponentModel.ISupportInitialize)(this.Plot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Plot2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhasePlot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Plot1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Plot2;
        private System.Windows.Forms.DataVisualization.Charting.Chart PhasePlot;
    }
}