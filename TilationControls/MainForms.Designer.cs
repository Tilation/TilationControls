﻿namespace TilationControls
{
    partial class MainForms
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
            this.buttonTProgressBarTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTProgressBarTest
            // 
            this.buttonTProgressBarTest.AutoSize = true;
            this.buttonTProgressBarTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonTProgressBarTest.Location = new System.Drawing.Point(12, 12);
            this.buttonTProgressBarTest.Name = "buttonTProgressBarTest";
            this.buttonTProgressBarTest.Size = new System.Drawing.Size(105, 23);
            this.buttonTProgressBarTest.TabIndex = 0;
            this.buttonTProgressBarTest.Text = "TProgressBar Test";
            this.buttonTProgressBarTest.UseVisualStyleBackColor = true;
            this.buttonTProgressBarTest.Click += new System.EventHandler(this.buttonTProgressBarTest_Click);
            // 
            // MainForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 282);
            this.Controls.Add(this.buttonTProgressBarTest);
            this.Name = "MainForms";
            this.Text = "Testing Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTProgressBarTest;
    }
}