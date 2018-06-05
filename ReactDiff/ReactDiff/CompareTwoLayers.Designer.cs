namespace ReactDiff
{
    partial class CompareTwoLayers
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea13 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend13 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea14 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend14 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea15 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend15 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series23 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea16 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend16 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series24 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Layer1TextBox = new System.Windows.Forms.TextBox();
            this.Layer2TextBox = new System.Windows.Forms.TextBox();
            this.PlotActiv = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotIngib = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotActivDiff = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotIngibDiff = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlotActiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotIngib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotActivDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotIngibDiff)).BeginInit();
            this.SuspendLayout();
            // 
            // Layer1TextBox
            // 
            this.Layer1TextBox.Location = new System.Drawing.Point(9, 25);
            this.Layer1TextBox.Name = "Layer1TextBox";
            this.Layer1TextBox.Size = new System.Drawing.Size(42, 20);
            this.Layer1TextBox.TabIndex = 0;
            this.Layer1TextBox.TextChanged += new System.EventHandler(this.Layer1TextBox_TextChanged);
            // 
            // Layer2TextBox
            // 
            this.Layer2TextBox.Location = new System.Drawing.Point(74, 25);
            this.Layer2TextBox.Name = "Layer2TextBox";
            this.Layer2TextBox.Size = new System.Drawing.Size(41, 20);
            this.Layer2TextBox.TabIndex = 1;
            this.Layer2TextBox.TextChanged += new System.EventHandler(this.Layer2TextBox_TextChanged);
            // 
            // PlotActiv
            // 
            chartArea13.Name = "ChartArea1";
            this.PlotActiv.ChartAreas.Add(chartArea13);
            legend13.Name = "Legend1";
            this.PlotActiv.Legends.Add(legend13);
            this.PlotActiv.Location = new System.Drawing.Point(9, 51);
            this.PlotActiv.Name = "PlotActiv";
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series19.Legend = "Legend1";
            series19.Name = "Series1";
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series20.Legend = "Legend1";
            series20.Name = "Series2";
            this.PlotActiv.Series.Add(series19);
            this.PlotActiv.Series.Add(series20);
            this.PlotActiv.Size = new System.Drawing.Size(318, 191);
            this.PlotActiv.TabIndex = 2;
            this.PlotActiv.Text = "chart1";
            // 
            // PlotIngib
            // 
            chartArea14.Name = "ChartArea1";
            this.PlotIngib.ChartAreas.Add(chartArea14);
            legend14.Name = "Legend1";
            this.PlotIngib.Legends.Add(legend14);
            this.PlotIngib.Location = new System.Drawing.Point(9, 260);
            this.PlotIngib.Name = "PlotIngib";
            series21.ChartArea = "ChartArea1";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series21.Legend = "Legend1";
            series21.Name = "Series1";
            series22.ChartArea = "ChartArea1";
            series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series22.Legend = "Legend1";
            series22.Name = "Series2";
            this.PlotIngib.Series.Add(series21);
            this.PlotIngib.Series.Add(series22);
            this.PlotIngib.Size = new System.Drawing.Size(318, 191);
            this.PlotIngib.TabIndex = 3;
            this.PlotIngib.Text = "chart2";
            // 
            // PlotActivDiff
            // 
            chartArea15.Name = "ChartArea1";
            this.PlotActivDiff.ChartAreas.Add(chartArea15);
            legend15.Name = "Legend1";
            this.PlotActivDiff.Legends.Add(legend15);
            this.PlotActivDiff.Location = new System.Drawing.Point(343, 51);
            this.PlotActivDiff.Name = "PlotActivDiff";
            series23.ChartArea = "ChartArea1";
            series23.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series23.Legend = "Legend1";
            series23.Name = "Series1";
            this.PlotActivDiff.Series.Add(series23);
            this.PlotActivDiff.Size = new System.Drawing.Size(318, 191);
            this.PlotActivDiff.TabIndex = 4;
            this.PlotActivDiff.Text = "chart1";
            // 
            // PlotIngibDiff
            // 
            chartArea16.Name = "ChartArea1";
            this.PlotIngibDiff.ChartAreas.Add(chartArea16);
            legend16.Name = "Legend1";
            this.PlotIngibDiff.Legends.Add(legend16);
            this.PlotIngibDiff.Location = new System.Drawing.Point(343, 260);
            this.PlotIngibDiff.Name = "PlotIngibDiff";
            series24.ChartArea = "ChartArea1";
            series24.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series24.Legend = "Legend1";
            series24.Name = "Series1";
            this.PlotIngibDiff.Series.Add(series24);
            this.PlotIngibDiff.Size = new System.Drawing.Size(318, 191);
            this.PlotIngibDiff.TabIndex = 5;
            this.PlotIngibDiff.Text = "chart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Слой 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Слой 2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Норма разности слоев активатора: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Норма разности слоев ингибитора: ";
            // 
            // CompareTwoLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 486);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlotIngibDiff);
            this.Controls.Add(this.PlotActivDiff);
            this.Controls.Add(this.PlotIngib);
            this.Controls.Add(this.PlotActiv);
            this.Controls.Add(this.Layer2TextBox);
            this.Controls.Add(this.Layer1TextBox);
            this.Name = "CompareTwoLayers";
            this.Text = "Сравнение пары слоев";
            ((System.ComponentModel.ISupportInitialize)(this.PlotActiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotIngib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotActivDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotIngibDiff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Layer1TextBox;
        private System.Windows.Forms.TextBox Layer2TextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotActiv;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotIngib;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotActivDiff;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotIngibDiff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}