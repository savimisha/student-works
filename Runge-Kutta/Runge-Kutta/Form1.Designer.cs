namespace Runge_Kutta
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TestButton = new System.Windows.Forms.Button();
            this.StepTextBox = new System.Windows.Forms.TextBox();
            this.NmaxTextBox = new System.Windows.Forms.TextBox();
            this.EpsTextBox = new System.Windows.Forms.TextBox();
            this.XmaxTextBox = new System.Windows.Forms.TextBox();
            this.StepLabel = new System.Windows.Forms.Label();
            this.NmaxLabel = new System.Windows.Forms.Label();
            this.EpsLabel = new System.Windows.Forms.Label();
            this.XmaxLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ControlCondcheckBox = new System.Windows.Forms.CheckBox();
            this.ControlErrorcheckBox = new System.Windows.Forms.CheckBox();
            this.Plot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.table = new System.Windows.Forms.DataGridView();
            this.BaseFirstButton = new System.Windows.Forms.Button();
            this.PhysicsButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ParBLabel = new System.Windows.Forms.Label();
            this.ParALabel = new System.Windows.Forms.Label();
            this.ParBtextBox = new System.Windows.Forms.TextBox();
            this.ParAtextBox = new System.Windows.Forms.TextBox();
            this.Base2Button = new System.Windows.Forms.Button();
            this.AllPlots = new System.Windows.Forms.Button();
            this.InitBounds2Label = new System.Windows.Forms.Label();
            this.InitBounds1Label = new System.Windows.Forms.Label();
            this.InitBounds2textBox = new System.Windows.Forms.TextBox();
            this.InitBounds1textBox = new System.Windows.Forms.TextBox();
            this.InitBoundtextBox = new System.Windows.Forms.TextBox();
            this.InitBoundLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Plot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(7, 33);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(132, 28);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Решить тестовую";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // StepTextBox
            // 
            this.StepTextBox.Location = new System.Drawing.Point(150, 4);
            this.StepTextBox.Name = "StepTextBox";
            this.StepTextBox.Size = new System.Drawing.Size(100, 20);
            this.StepTextBox.TabIndex = 1;
            this.StepTextBox.Text = "0.1";
            // 
            // NmaxTextBox
            // 
            this.NmaxTextBox.Location = new System.Drawing.Point(150, 30);
            this.NmaxTextBox.Name = "NmaxTextBox";
            this.NmaxTextBox.Size = new System.Drawing.Size(100, 20);
            this.NmaxTextBox.TabIndex = 2;
            this.NmaxTextBox.Text = "100";
            // 
            // EpsTextBox
            // 
            this.EpsTextBox.Location = new System.Drawing.Point(150, 56);
            this.EpsTextBox.Name = "EpsTextBox";
            this.EpsTextBox.Size = new System.Drawing.Size(100, 20);
            this.EpsTextBox.TabIndex = 3;
            this.EpsTextBox.Text = "1e-3";
            // 
            // XmaxTextBox
            // 
            this.XmaxTextBox.Location = new System.Drawing.Point(150, 82);
            this.XmaxTextBox.Name = "XmaxTextBox";
            this.XmaxTextBox.Size = new System.Drawing.Size(100, 20);
            this.XmaxTextBox.TabIndex = 4;
            this.XmaxTextBox.Text = "10";
            // 
            // StepLabel
            // 
            this.StepLabel.AutoSize = true;
            this.StepLabel.Location = new System.Drawing.Point(57, 8);
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.Size = new System.Drawing.Size(30, 13);
            this.StepLabel.TabIndex = 5;
            this.StepLabel.Text = "Шаг:";
            // 
            // NmaxLabel
            // 
            this.NmaxLabel.AutoSize = true;
            this.NmaxLabel.Location = new System.Drawing.Point(6, 31);
            this.NmaxLabel.Name = "NmaxLabel";
            this.NmaxLabel.Size = new System.Drawing.Size(135, 13);
            this.NmaxLabel.TabIndex = 6;
            this.NmaxLabel.Text = "Макс. количество шагов:";
            // 
            // EpsLabel
            // 
            this.EpsLabel.AutoSize = true;
            this.EpsLabel.Location = new System.Drawing.Point(60, 58);
            this.EpsLabel.Name = "EpsLabel";
            this.EpsLabel.Size = new System.Drawing.Size(28, 13);
            this.EpsLabel.TabIndex = 7;
            this.EpsLabel.Text = "Eps:";
            // 
            // XmaxLabel
            // 
            this.XmaxLabel.AutoSize = true;
            this.XmaxLabel.Location = new System.Drawing.Point(15, 85);
            this.XmaxLabel.Name = "XmaxLabel";
            this.XmaxLabel.Size = new System.Drawing.Size(115, 13);
            this.XmaxLabel.TabIndex = 8;
            this.XmaxLabel.Text = "Правая граница по x:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ControlCondcheckBox);
            this.panel1.Controls.Add(this.ControlErrorcheckBox);
            this.panel1.Controls.Add(this.StepTextBox);
            this.panel1.Controls.Add(this.XmaxLabel);
            this.panel1.Controls.Add(this.StepLabel);
            this.panel1.Controls.Add(this.XmaxTextBox);
            this.panel1.Controls.Add(this.EpsLabel);
            this.panel1.Controls.Add(this.EpsTextBox);
            this.panel1.Controls.Add(this.NmaxLabel);
            this.panel1.Controls.Add(this.NmaxTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 174);
            this.panel1.TabIndex = 9;
            // 
            // ControlCondcheckBox
            // 
            this.ControlCondcheckBox.AutoSize = true;
            this.ControlCondcheckBox.Checked = true;
            this.ControlCondcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ControlCondcheckBox.Location = new System.Drawing.Point(18, 136);
            this.ControlCondcheckBox.Name = "ControlCondcheckBox";
            this.ControlCondcheckBox.Size = new System.Drawing.Size(229, 17);
            this.ControlCondcheckBox.TabIndex = 10;
            this.ControlCondcheckBox.Text = "С контролем выхода на правую границу";
            this.ControlCondcheckBox.UseVisualStyleBackColor = true;
            // 
            // ControlErrorcheckBox
            // 
            this.ControlErrorcheckBox.AutoSize = true;
            this.ControlErrorcheckBox.Checked = true;
            this.ControlErrorcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ControlErrorcheckBox.Location = new System.Drawing.Point(18, 113);
            this.ControlErrorcheckBox.Name = "ControlErrorcheckBox";
            this.ControlErrorcheckBox.Size = new System.Drawing.Size(160, 17);
            this.ControlErrorcheckBox.TabIndex = 9;
            this.ControlErrorcheckBox.Text = "С контролем погрешности";
            this.ControlErrorcheckBox.UseVisualStyleBackColor = true;
            // 
            // Plot
            // 
            chartArea1.Name = "ChartArea1";
            this.Plot.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Plot.Legends.Add(legend1);
            this.Plot.Location = new System.Drawing.Point(528, 22);
            this.Plot.Name = "Plot";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.Plot.Series.Add(series1);
            this.Plot.Series.Add(series2);
            this.Plot.Size = new System.Drawing.Size(480, 267);
            this.Plot.TabIndex = 10;
            this.Plot.Text = "chart1";
            // 
            // table
            // 
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Location = new System.Drawing.Point(309, 295);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(938, 264);
            this.table.TabIndex = 11;
            // 
            // BaseFirstButton
            // 
            this.BaseFirstButton.Location = new System.Drawing.Point(7, 64);
            this.BaseFirstButton.Name = "BaseFirstButton";
            this.BaseFirstButton.Size = new System.Drawing.Size(132, 30);
            this.BaseFirstButton.TabIndex = 12;
            this.BaseFirstButton.Text = "Решить основную 1";
            this.BaseFirstButton.UseVisualStyleBackColor = true;
            this.BaseFirstButton.Click += new System.EventHandler(this.BaseFirstButton_Click);
            // 
            // PhysicsButton
            // 
            this.PhysicsButton.Location = new System.Drawing.Point(8, 62);
            this.PhysicsButton.Name = "PhysicsButton";
            this.PhysicsButton.Size = new System.Drawing.Size(115, 44);
            this.PhysicsButton.TabIndex = 13;
            this.PhysicsButton.Text = "Решить задачу с грузом";
            this.PhysicsButton.UseVisualStyleBackColor = true;
            this.PhysicsButton.Click += new System.EventHandler(this.PhysicsButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ParBLabel);
            this.panel2.Controls.Add(this.ParALabel);
            this.panel2.Controls.Add(this.ParBtextBox);
            this.panel2.Controls.Add(this.ParAtextBox);
            this.panel2.Controls.Add(this.Base2Button);
            this.panel2.Controls.Add(this.AllPlots);
            this.panel2.Controls.Add(this.InitBounds2Label);
            this.panel2.Controls.Add(this.InitBounds1Label);
            this.panel2.Controls.Add(this.InitBounds2textBox);
            this.panel2.Controls.Add(this.InitBounds1textBox);
            this.panel2.Controls.Add(this.PhysicsButton);
            this.panel2.Location = new System.Drawing.Point(12, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 161);
            this.panel2.TabIndex = 14;
            // 
            // ParBLabel
            // 
            this.ParBLabel.AutoSize = true;
            this.ParBLabel.Location = new System.Drawing.Point(160, 39);
            this.ParBLabel.Name = "ParBLabel";
            this.ParBLabel.Size = new System.Drawing.Size(25, 13);
            this.ParBLabel.TabIndex = 23;
            this.ParBLabel.Text = "b = ";
            // 
            // ParALabel
            // 
            this.ParALabel.AutoSize = true;
            this.ParALabel.Location = new System.Drawing.Point(159, 16);
            this.ParALabel.Name = "ParALabel";
            this.ParALabel.Size = new System.Drawing.Size(22, 13);
            this.ParALabel.TabIndex = 22;
            this.ParALabel.Text = "a =";
            // 
            // ParBtextBox
            // 
            this.ParBtextBox.Location = new System.Drawing.Point(190, 37);
            this.ParBtextBox.Name = "ParBtextBox";
            this.ParBtextBox.Size = new System.Drawing.Size(54, 20);
            this.ParBtextBox.TabIndex = 21;
            this.ParBtextBox.Text = "1";
            // 
            // ParAtextBox
            // 
            this.ParAtextBox.Location = new System.Drawing.Point(190, 13);
            this.ParAtextBox.Name = "ParAtextBox";
            this.ParAtextBox.Size = new System.Drawing.Size(54, 20);
            this.ParAtextBox.TabIndex = 20;
            this.ParAtextBox.Text = "1";
            // 
            // Base2Button
            // 
            this.Base2Button.Location = new System.Drawing.Point(129, 63);
            this.Base2Button.Name = "Base2Button";
            this.Base2Button.Size = new System.Drawing.Size(115, 42);
            this.Base2Button.TabIndex = 19;
            this.Base2Button.Text = "Решить основную 2";
            this.Base2Button.UseVisualStyleBackColor = true;
            this.Base2Button.Click += new System.EventHandler(this.Base2Button_Click);
            // 
            // AllPlots
            // 
            this.AllPlots.Location = new System.Drawing.Point(63, 112);
            this.AllPlots.Name = "AllPlots";
            this.AllPlots.Size = new System.Drawing.Size(119, 38);
            this.AllPlots.TabIndex = 18;
            this.AllPlots.Text = "Все графики";
            this.AllPlots.UseVisualStyleBackColor = true;
            this.AllPlots.Click += new System.EventHandler(this.AllPlots_Click);
            // 
            // InitBounds2Label
            // 
            this.InitBounds2Label.AutoSize = true;
            this.InitBounds2Label.Location = new System.Drawing.Point(10, 37);
            this.InitBounds2Label.Name = "InitBounds2Label";
            this.InitBounds2Label.Size = new System.Drawing.Size(36, 13);
            this.InitBounds2Label.TabIndex = 17;
            this.InitBounds2Label.Text = "u\'(0) =";
            // 
            // InitBounds1Label
            // 
            this.InitBounds1Label.AutoSize = true;
            this.InitBounds1Label.Location = new System.Drawing.Point(11, 12);
            this.InitBounds1Label.Name = "InitBounds1Label";
            this.InitBounds1Label.Size = new System.Drawing.Size(34, 13);
            this.InitBounds1Label.TabIndex = 16;
            this.InitBounds1Label.Text = "u(0) =";
            // 
            // InitBounds2textBox
            // 
            this.InitBounds2textBox.Location = new System.Drawing.Point(47, 36);
            this.InitBounds2textBox.Name = "InitBounds2textBox";
            this.InitBounds2textBox.Size = new System.Drawing.Size(40, 20);
            this.InitBounds2textBox.TabIndex = 15;
            this.InitBounds2textBox.Text = "1";
            // 
            // InitBounds1textBox
            // 
            this.InitBounds1textBox.Location = new System.Drawing.Point(47, 10);
            this.InitBounds1textBox.Name = "InitBounds1textBox";
            this.InitBounds1textBox.Size = new System.Drawing.Size(40, 20);
            this.InitBounds1textBox.TabIndex = 14;
            this.InitBounds1textBox.Text = "1";
            // 
            // InitBoundtextBox
            // 
            this.InitBoundtextBox.Location = new System.Drawing.Point(55, 7);
            this.InitBoundtextBox.Name = "InitBoundtextBox";
            this.InitBoundtextBox.Size = new System.Drawing.Size(51, 20);
            this.InitBoundtextBox.TabIndex = 15;
            this.InitBoundtextBox.Text = "1";
            // 
            // InitBoundLabel
            // 
            this.InitBoundLabel.AutoSize = true;
            this.InitBoundLabel.Location = new System.Drawing.Point(12, 10);
            this.InitBoundLabel.Name = "InitBoundLabel";
            this.InitBoundLabel.Size = new System.Drawing.Size(37, 13);
            this.InitBoundLabel.TabIndex = 16;
            this.InitBoundLabel.Text = "u(0) = ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.InitBoundtextBox);
            this.panel3.Controls.Add(this.InitBoundLabel);
            this.panel3.Controls.Add(this.BaseFirstButton);
            this.panel3.Controls.Add(this.TestButton);
            this.panel3.Location = new System.Drawing.Point(280, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 102);
            this.panel3.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 640);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.table);
            this.Controls.Add(this.Plot);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Метод Рунге-Кутта";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Plot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.TextBox StepTextBox;
        private System.Windows.Forms.TextBox NmaxTextBox;
        private System.Windows.Forms.TextBox EpsTextBox;
        private System.Windows.Forms.TextBox XmaxTextBox;
        private System.Windows.Forms.Label StepLabel;
        private System.Windows.Forms.Label NmaxLabel;
        private System.Windows.Forms.Label EpsLabel;
        private System.Windows.Forms.Label XmaxLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ControlCondcheckBox;
        private System.Windows.Forms.CheckBox ControlErrorcheckBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart Plot;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.Button BaseFirstButton;
        private System.Windows.Forms.Button PhysicsButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AllPlots;
        private System.Windows.Forms.Label InitBounds2Label;
        private System.Windows.Forms.Label InitBounds1Label;
        private System.Windows.Forms.TextBox InitBounds2textBox;
        private System.Windows.Forms.TextBox InitBounds1textBox;
        private System.Windows.Forms.TextBox InitBoundtextBox;
        private System.Windows.Forms.Label InitBoundLabel;
        private System.Windows.Forms.Button Base2Button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label ParBLabel;
        private System.Windows.Forms.Label ParALabel;
        private System.Windows.Forms.TextBox ParBtextBox;
        private System.Windows.Forms.TextBox ParAtextBox;
        private System.Windows.Forms.Label label1;
    }
}

