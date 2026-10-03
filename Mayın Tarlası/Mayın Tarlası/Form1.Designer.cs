namespace Mayın_Tarlası
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSmile = new Button();
            btnReset = new Button();
            SuspendLayout();
            // 
            // btnSmile
            // 
            btnSmile.Location = new Point(738, 12);
            btnSmile.Name = "btnSmile";
            btnSmile.Size = new Size(50, 40);
            btnSmile.TabIndex = 0;
            btnSmile.Text = "😄";
            btnSmile.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(575, 12);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(108, 40);
            btnReset.TabIndex = 1;
            btnReset.Text = "Oyunu Sıfırla";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 529);
            Controls.Add(btnReset);
            Controls.Add(btnSmile);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSmile;
        private Button btnReset;
    }
}
