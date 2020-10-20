using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.Controls
{
    class TIntNumericUpDown : NumericUpDown
    {
        public int IntValue => (int)this.Value;
    }
}
