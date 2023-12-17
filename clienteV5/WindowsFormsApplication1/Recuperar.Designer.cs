namespace WindowsFormsApplication1
{
    partial class Recuperar
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
            this.GameBox = new System.Windows.Forms.TextBox();
            this.RecoverGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameBox
            // 
            this.GameBox.Location = new System.Drawing.Point(14, 46);
            this.GameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GameBox.Name = "GameBox";
            this.GameBox.Size = new System.Drawing.Size(147, 26);
            this.GameBox.TabIndex = 4;
            // 
            // RecoverGame
            // 
            this.RecoverGame.Location = new System.Drawing.Point(14, 109);
            this.RecoverGame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RecoverGame.Name = "RecoverGame";
            this.RecoverGame.Size = new System.Drawing.Size(158, 32);
            this.RecoverGame.TabIndex = 5;
            this.RecoverGame.Text = "Recover Game";
            this.RecoverGame.UseVisualStyleBackColor = true;
            this.RecoverGame.Click += new System.EventHandler(this.recoverButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Input the game code you want to recover";
            // 
            // Recuperar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RecoverGame);
            this.Controls.Add(this.GameBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Recuperar";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Recuperar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GameBox;
        private System.Windows.Forms.Button RecoverGame;
        private System.Windows.Forms.Label label1;
    }
}