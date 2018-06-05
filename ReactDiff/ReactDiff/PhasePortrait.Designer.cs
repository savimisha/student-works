namespace ReactDiff
{
    partial class PhasePortrait
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.XValuetextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StepLayerTextBox = new System.Windows.Forms.TextBox();
            this.Plot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotV1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotV2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Plot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotV2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите номер узла по x:";
            // 
            // XValuetextBox
            // 
            this.XValuetextBox.Location = new System.Drawing.Point(154, 6);
            this.XValuetextBox.Name = "XValuetextBox";
            this.XValuetextBox.Size = new System.Drawing.Size(100, 20);
            this.XValuetextBox.TabIndex = 1;
            this.XValuetextBox.TextChanged += new System.EventHandler(this.XValuetextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Шаг по слоям:";
            // 
            // StepLayerTextBox
            // 
            this.StepLayerTextBox.Location = new System.Drawing.Point(154, 28);
            this.StepLayerTextBox.Name = "StepLayerTextBox";
            this.StepLayerTextBox.Size = new System.Drawing.Size(100, 20);
            this.StepLayerTextBox.TabIndex = 3;
            this.StepLayerTextBox.TextChanged += new System.EventHandler(this.StepLayerTextBox_TextChanged);
            // 
            // Plot
            // 
            chartArea4.Name = "ChartArea1";
            this.Plot.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.Plot.Legends.Add(legend4);
            this.Plot.Location = new System.Drawing.Point(12, 54);
            this.Plot.Name = "Plot";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.Plot.Series.Add(series4);
            this.Plot.Size = new System.Drawing.Size(361, 246);
            this.Plot.TabIndex = 4;
            this.Plot.Text = "chart1";
            // 
            // PlotV1
            // 
            chartArea5.Name = "ChartArea1";
            this.PlotV1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.PlotV1.Legends.Add(legend5);
            this.PlotV1.Location = new System.Drawing.Point(379, 54);
            this.PlotV1.Name = "PlotV1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.PlotV1.Series.Add(series5);
            this.PlotV1.Size = new System.Drawing.Size(361, 246);
            this.PlotV1.TabIndex = 5;
            this.PlotV1.Text = "chart1";
            // 
            // PlotV2
            // 
            chartArea6.Name = "ChartArea1";
            this.PlotV2.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.PlotV2.Legends.Add(legend6);
            this.PlotV2.Location = new System.Drawing.Point(12, 306);
            this.PlotV2.Name = "PlotV2";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.PlotV2.Series.Add(series6);
            this.PlotV2.Size = new System.Drawing.Size(361, 246);
            this.PlotV2.TabIndex = 6;
            this.PlotV2.Text = "chart2";
            // 
            // PhasePortrait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 565);
            this.Controls.Add(this.PlotV2);
            this.Controls.Add(this.PlotV1);
            this.Controls.Add(this.Plot);
            this.Controls.Add(this.StepLayerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.XValuetextBox);
            this.Controls.Add(this.label1);
            this.Name = "PhasePortrait";
            this.Text = "Фазовый портрет";
            ((System.ComponentModel.ISupportInitialize)(this.Plot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotV2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox XValuetextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StepLayerTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart Plot;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotV1;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotV2;
    }
}