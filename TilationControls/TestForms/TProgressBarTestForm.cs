using System;
using System.Windows.Forms;

namespace TilationControls.TestForms
{
    public partial class TProgressBarTestForm : Form
    {
        float count = 0;
        public TProgressBarTestForm()
        {
            InitializeComponent();
        }


        private void buttonSub_Click(object sender, EventArgs e)
        {
            count--;
            tProgressBar3.Value = count;
            tProgressBar2.Value = count;
            tProgressBar1.Value = count;
            count = tProgressBar1.Value;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            count++;

            tProgressBar1.Value = count;
            tProgressBar2.Value = count;
            tProgressBar3.Value = count;
            count = tProgressBar1.Value;
        }

        private void buttonSet0_Click(object sender, EventArgs e)
        {
            tProgressBar1.Value = 0;
            tProgressBar2.Value = 0;
            tProgressBar3.Value = 0;
            count = tProgressBar1.Value;
        }

        private void buttonSet50_Click(object sender, EventArgs e)
        {

            tProgressBar1.Value = 50;
            tProgressBar2.Value = 50;
            tProgressBar3.Value = 50;
            count = tProgressBar1.Value;

        }


        private void buttonSet100_Click(object sender, EventArgs e)
        {

            tProgressBar1.Value = 100;
            tProgressBar2.Value = 100;
            tProgressBar3.Value = 100;
            count = tProgressBar1.Value;

        }
    }
}
