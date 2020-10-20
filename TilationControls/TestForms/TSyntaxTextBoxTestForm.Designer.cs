namespace TilationControls.TestForms
{
    partial class TSyntaxTextBoxTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSyntaxTextBoxTestForm));
            this.tSyntaxTextBox1 = new TilationControls.Controls.TSyntaxTextBox();
            this.SuspendLayout();
            // 
            // tSyntaxTextBox1
            // 
            this.tSyntaxTextBox1.Highlights = ((System.Collections.Generic.Dictionary<string, TilationControls.Controls.TextStyle>)(resources.GetObject("tSyntaxTextBox1.Highlights")));
            this.tSyntaxTextBox1.Location = new System.Drawing.Point(49, 78);
            this.tSyntaxTextBox1.Name = "tSyntaxTextBox1";
            this.tSyntaxTextBox1.Size = new System.Drawing.Size(258, 228);
            this.tSyntaxTextBox1.TabIndex = 0;
            this.tSyntaxTextBox1.Text = "tSyntaxTextBox1";
            // 
            // TSyntaxTextBoxTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 404);
            this.Controls.Add(this.tSyntaxTextBox1);
            this.Name = "TSyntaxTextBoxTestForm";
            this.Text = "TSyntaxTextBoxTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TSyntaxTextBox tSyntaxTextBox1;
    }
}