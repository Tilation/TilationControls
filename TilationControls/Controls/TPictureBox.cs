using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.Controls
{
    class TPictureBox : PictureBox
    {
        private bool nnInterpolation;

        public bool PixelPerfectInterpolation { get => nnInterpolation; set => nnInterpolation = value; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            base.OnPaint(pe);
        }
    }
}
