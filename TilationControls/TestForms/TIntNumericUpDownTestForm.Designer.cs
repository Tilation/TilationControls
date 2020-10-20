namespace TilationControls.TestForms
{
    partial class TIntNumericUpDownTestForm
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
            this.tIntNumericUpDown1 = new TilationControls.Controls.TIntNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.tIntNumericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tIntNumericUpDown1
            // 
            this.tIntNumericUpDown1.Increment = 4;
            this.tIntNumericUpDown1.Location = new System.Drawing.Point(224, 49);
            this.tIntNumericUpDown1.Name = "tIntNumericUpDown1";
            this.tIntNumericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.tIntNumericUpDown1.TabIndex = 0;
            this.tIntNumericUpDown1.Value = 0;
            // 
            // TIntNumericUpDownTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tIntNumericUpDown1);
            this.Name = "TIntNumericUpDownTestForm";
            this.Text = "TIntNumericUpDownTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.tIntNumericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TIntNumericUpDown tIntNumericUpDown1;
    }
}