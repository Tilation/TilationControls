﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TilationControls.TestForms;

namespace TilationControls
{
    public partial class MainForms : Form
    {
        public MainForms()
        {
            InitializeComponent();
        }

        private void buttonTProgressBarTest_Click(object sender, EventArgs e)
        {
            TProgressBarTestForm t = new TProgressBarTestForm();
            t.ShowDialog();
        }
    }
}
