namespace WindowsFormsApplication1
{
    partial class RecoverPassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.recoverButton = new System.Windows.Forms.Button();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.recoverLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Escribe tu ID aquí";
            // 
            // recoverButton
            // 
            this.recoverButton.Location = new System.Drawing.Point(221, 126);
            this.recoverButton.Margin = new System.Windows.Forms.Padding(2);
            this.recoverButton.Name = "recoverButton";
            this.recoverButton.Size = new System.Drawing.Size(212, 36);
            this.recoverButton.TabIndex = 4;
            this.recoverButton.Text = "Recover Password";
            this.recoverButton.UseVisualStyleBackColor = true;
            this.recoverButton.Click += new System.EventHandler(this.recoverButton_Click);
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(221, 92);
            this.IDTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(212, 20);
            this.IDTextBox.TabIndex = 3;
            // 
            // recoverLabel
            // 
            this.recoverLabel.AutoSize = true;
            this.recoverLabel.Location = new System.Drawing.Point(282, 47);
            this.recoverLabel.Name = "recoverLabel";
            this.recoverLabel.Size = new System.Drawing.Size(102, 13);
            this.recoverLabel.TabIndex = 6;
            this.recoverLabel.Text = "Password Recovery";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 278);
            this.Controls.Add(this.recoverLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recoverButton);
            this.Controls.Add(this.IDTextBox);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button recoverButton;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label recoverLabel;
    }
}