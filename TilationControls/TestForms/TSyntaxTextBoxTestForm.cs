using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.TestForms
{
    public partial class TSyntaxTextBoxTestForm : Form
    {
        public TSyntaxTextBoxTestForm()
        {
            InitializeComponent();
            tSyntaxTextBox1.Highlights = new Dictionary<string, Controls.TextStyle>()
            {
                { "int",new Controls.TextStyle( Color.Red) },
                { "string",new Controls.TextStyle( Color.Blue) },
                { "ringo",new Controls.TextStyle(Color.White, Color.Black) }
            };
        }
    }
}
