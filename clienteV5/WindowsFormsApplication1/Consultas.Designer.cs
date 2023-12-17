namespace WindowsFormsApplication1
{
    partial class Consultas
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
            this.usuarioLabel = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.Consulta2Button = new System.Windows.Forms.Button();
            this.Consulta1Button = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Results = new System.Windows.Forms.Label();
            this.resultsDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // usuarioLabel
            // 
            this.usuarioLabel.AutoSize = true;
            this.usuarioLabel.Location = new System.Drawing.Point(177, 32);
            this.usuarioLabel.Name = "usuarioLabel";
            this.usuarioLabel.Size = new System.Drawing.Size(0, 13);
            this.usuarioLabel.TabIndex = 11;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(110, 154);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(155, 20);
            this.dateTimePicker2.TabIndex = 20;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(110, 105);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(155, 20);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 105);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 69);
            this.button3.TabIndex = 18;
            this.button3.Text = "List of games";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Consulta2Button
            // 
            this.Consulta2Button.Location = new System.Drawing.Point(18, 75);
            this.Consulta2Button.Name = "Consulta2Button";
            this.Consulta2Button.Size = new System.Drawing.Size(134, 23);
            this.Consulta2Button.TabIndex = 17;
            this.Consulta2Button.Text = "Results with player:";
            this.Consulta2Button.UseVisualStyleBackColor = true;
            this.Consulta2Button.Click += new System.EventHandler(this.Consulta2Button_Click);
            // 
            // Consulta1Button
            // 
            this.Consulta1Button.Location = new System.Drawing.Point(18, 46);
            this.Consulta1Button.Name = "Consulta1Button";
            this.Consulta1Button.Size = new System.Drawing.Size(247, 23);
            this.Consulta1Button.TabIndex = 16;
            this.Consulta1Button.Text = "Players who i played with";
            this.Consulta1Button.UseVisualStyleBackColor = true;
            this.Consulta1Button.Click += new System.EventHandler(this.Consulta1Button_Click);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(714, 22);
            this.Close.Margin = new System.Windows.Forms.Padding(2);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 15;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Location = new System.Drawing.Point(158, 77);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(107, 20);
            this.nombreTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "TO";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Results);
            this.groupBox2.Controls.Add(this.resultsDataGrid);
            this.groupBox2.Controls.Add(this.Consulta1Button);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nombreTextBox);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Controls.Add(this.Consulta2Button);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(105, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 214);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Queries";
            // 
            // Results
            // 
            this.Results.AutoSize = true;
            this.Results.Location = new System.Drawing.Point(296, 30);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(42, 13);
            this.Results.TabIndex = 23;
            this.Results.Text = "Results";
            // 
            // resultsDataGrid
            // 
            this.resultsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsDataGrid.Location = new System.Drawing.Point(299, 46);
            this.resultsDataGrid.Name = "resultsDataGrid";
            this.resultsDataGrid.Size = new System.Drawing.Size(240, 150);
            this.resultsDataGrid.TabIndex = 22;
            this.resultsDataGrid.SelectionChanged += new System.EventHandler(this.resultsDataGrid_SelectionChanged);
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.usuarioLabel);
            this.Controls.Add(this.Close);
            this.Name = "Consultas";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Consultas_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usuarioLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button Consulta2Button;
        private System.Windows.Forms.Button Consulta1Button;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Results;
        private System.Windows.Forms.DataGridView resultsDataGrid;
    }
}