using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.Controls
{
    public enum ProgressChange
    {
        Instant,
        Lerp,
        Constant
    }

    public enum ProgressStyle
    {
        Flat,
        Segmented
    }

    public partial class TProgressBar : UserControl
    {
        private int value;
        private int shown_value;
        private int borderThickness;
        private ProgressChange progressType;
        private bool valueIsDrawn = false;
        private Timer animationTimer;

        private Color borderColor;

        

        public TProgressBar()
        {
            DoubleBuffered = true;
            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimationTimerTick;
            InitializeComponent();
        }

        private void AnimationTimerTick(object sender, EventArgs e)
        {
            switch (progressType)
            {
                default:
                case ProgressChange.Instant:
                    {
                        shown_value = value;
                        Invalidate();
                        animationTimer.Stop();
                        break;
                    }
            }
        }

        int GetBarWidthFromPercent()
        {
            return ClientSize.Width * shown_value / 100;
        }

        public int Value 
        {
            get => value;
            set 
            { 
                var sanitized_input = (value <= 100) ? (value >= 0) ? value : 0 : 100;
                if (this.value != sanitized_input)
                {
                    this.value = sanitized_input;
                    valueIsDrawn = false;
                    animationTimer.Start();
                    Invalidate();
                }
            }
        }

        public int BorderThickness 
        {
            get => borderThickness;
            set
            {
                borderThickness = value;
                Invalidate();
            }
        }

        public ProgressChange ProgressType 
        { 
            get => progressType;
            set
            {
                progressType = value;
                Invalidate();
            }
        }

        public Color BorderColor 
        { 
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private void TProgressBar_Paint(object sender, PaintEventArgs e)
        {
            //Manual Double Buffer
            Bitmap b = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(b);

            //  Draw Background to buffer
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, b.Width, b.Height);

            //  Draw ProgressBar to buffer
            if (shown_value > 0)
            {
                g.FillRectangle(new SolidBrush(ForeColor),
                    0 + BorderThickness,
                    0 + BorderThickness,
                    GetBarWidthFromPercent() - (BorderThickness * 2),
                    b.Height - (BorderThickness * 2)
                    );
            }
            //  Draw Borders, if enabled, to buffer
            //  BUG: BORDER SHOWN IN EDITORR BUT NOT IN EXECUTION
            if (BorderThickness > 0)
            {
                g.DrawRectangle(new Pen(BorderColor, BorderThickness),
                    0 + (BorderThickness / 2),
                    0 + (BorderThickness / 2),
                    b.Width - (BorderThickness),
                    b.Height - (BorderThickness)
                    );
            }

            //  Draw Buffer
            e.Graphics.DrawImage(b, 0, 0);
        }

        private void TProgressBar_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void TProgressBar_Move(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
