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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MoneyButton = new System.Windows.Forms.Button();
            this.partidasJugadasButton = new System.Windows.Forms.Button();
            this.masPuntosButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.usuarioLabel = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Close);
            this.groupBox1.Controls.Add(this.MoneyButton);
            this.groupBox1.Controls.Add(this.partidasJugadasButton);
            this.groupBox1.Controls.Add(this.masPuntosButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nombreTextBox);
            this.groupBox1.Location = new System.Drawing.Point(303, 77);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(544, 374);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // MoneyButton
            // 
            this.MoneyButton.Location = new System.Drawing.Point(36, 240);
            this.MoneyButton.Name = "MoneyButton";
            this.MoneyButton.Size = new System.Drawing.Size(112, 35);
            this.MoneyButton.TabIndex = 14;
            this.MoneyButton.Text = "Money";
            this.MoneyButton.UseVisualStyleBackColor = true;
            this.MoneyButton.Click += new System.EventHandler(this.MoneyButton_Click);
            // 
            // partidasJugadasButton
            // 
            this.partidasJugadasButton.Location = new System.Drawing.Point(276, 240);
            this.partidasJugadasButton.Name = "partidasJugadasButton";
            this.partidasJugadasButton.Size = new System.Drawing.Size(238, 35);
            this.partidasJugadasButton.TabIndex = 13;
            this.partidasJugadasButton.Text = "Partidas Jugadas";
            this.partidasJugadasButton.UseVisualStyleBackColor = true;
            this.partidasJugadasButton.Click += new System.EventHandler(this.partidasJugadasButton_Click);
            // 
            // masPuntosButton
            // 
            this.masPuntosButton.Location = new System.Drawing.Point(156, 240);
            this.masPuntosButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.masPuntosButton.Name = "masPuntosButton";
            this.masPuntosButton.Size = new System.Drawing.Size(112, 35);
            this.masPuntosButton.TabIndex = 11;
            this.masPuntosButton.Text = "Mas Puntos";
            this.masPuntosButton.UseVisualStyleBackColor = true;
            this.masPuntosButton.Click += new System.EventHandler(this.masPuntosButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Location = new System.Drawing.Point(248, 135);
            this.nombreTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(244, 26);
            this.nombreTextBox.TabIndex = 3;
            // 
            // usuarioLabel
            // 
            this.usuarioLabel.AutoSize = true;
            this.usuarioLabel.Location = new System.Drawing.Point(298, 34);
            this.usuarioLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usuarioLabel.Name = "usuarioLabel";
            this.usuarioLabel.Size = new System.Drawing.Size(0, 20);
            this.usuarioLabel.TabIndex = 9;
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(211, 304);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(112, 35);
            this.Close.TabIndex = 15;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.usuarioLabel);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Consultas";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Consultas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button MoneyButton;
        private System.Windows.Forms.Button partidasJugadasButton;
        private System.Windows.Forms.Button masPuntosButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.Label usuarioLabel;
        private System.Windows.Forms.Button Close;
    }
}