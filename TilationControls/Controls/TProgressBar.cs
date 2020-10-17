using System;
using System.Drawing;
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
        //Segmented,
        //IdkHowLongItsGonnaTakeJustShowThingsAreWorking
    }

    public partial class TProgressBar : UserControl
    {
        // ProgressBar Values
        private float value;
        private float shownValue;
        private float maxValue = 100;

        //  Design
        private int borderThickness;
        private ProgressChange progressType;
        private Color borderColor;

        // Animation Controller
        private Timer animationTimer;

        //  Constant
        private float constantSpeed = 10f;

        // Lerp
        private float lerpSpeed = 20f;

        public TProgressBar()
        {
            DoubleBuffered = true;
            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimationTimerTick;
            InitializeComponent();
        }

        //  This handles the animations
        private void AnimationTimerTick(object sender, EventArgs e)
        {
            var dif = value - shownValue;
            switch (progressType)
            {
                default:
                case ProgressChange.Instant:
                    {
                        shownValue = value;
                        Invalidate();
                        animationTimer.Stop();
                        break;
                    }
                case ProgressChange.Lerp:
                    {
                        float lerpValue = (dif) * LerpSpeed / 100f;

                        shownValue += (lerpValue > 0f && lerpValue < 1f) ? 1 : (lerpValue < 0f && lerpValue > -1f) ? -1 : (int)lerpValue;

                        if (Math.Abs(dif) <= 1)
                        {
                            shownValue = value;
                            animationTimer.Stop();
                        }
                        Invalidate();
                        break;
                    }
                case ProgressChange.Constant:
                    {
                        shownValue += ConstantSpeed * Math.Sign(dif);
                        if (Math.Abs(dif) <= 1)
                        {
                            shownValue = value;
                            animationTimer.Stop();
                        }
                        Invalidate();
                        break;
                    }
            }
        }

        float GetBarWidthFromPercent()
        {
            return (ClientSize.Width - (BorderThickness * 2)) * shownValue / MaxValue;
        }

        public float Value
        {
            get => value;
            set
            {
                var sanitized_input = (value <= MaxValue) ? (value >= 0) ? value : 0 : MaxValue;
                if (this.value != sanitized_input)
                {
                    this.value = sanitized_input;
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

                borderThickness = (value >= 0) ? value : 0;
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

        public float LerpSpeed
        {
            get => lerpSpeed;
            set
            {
                lerpSpeed = (value <= 100) ? (value > 0) ? value : 1 : 100; ;
            }
        }

        public float MaxValue 
        {
            get => maxValue;
            set
            {
                maxValue = (value > 0) ? value : 1;
                Value = (Value > maxValue) ? maxValue : Value;
                Invalidate();
            }
        }

        public float ConstantSpeed
        { 
            get => constantSpeed;
            set
            {
                constantSpeed = (value != 0) ? value : 1;
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
            if (shownValue > 0)
            {
                g.FillRectangle(new SolidBrush(ForeColor),
                    0 + BorderThickness,
                    0 + BorderThickness,
                    GetBarWidthFromPercent(),
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
