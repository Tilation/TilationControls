using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TilationControls.Controls
{
    public enum LightIndicatorShape
    {
        Rectangle,
        Circle,
        Diamond
    }
    public partial class TLightIndicator : UserControl
    {
        // Private
        private LightIndicatorShape shape;
        private int borderWidth;
        private int totalStates;
        private int currentState;
        private bool wrapAround;
        List<Color> stateColors;

        // Events
        public event Action<LightIndicatorShape> ShapeChanged;
        public event Action<int> BorderWidthChanged;
        public event Action<int> StateChanged;
        
        // Properties
        public LightIndicatorShape Shape
        {
            get => shape;
            set
            {
                if (shape != value)
                {
                    shape = value;
                    RaiseShapeChangedEvent(shape);
                }
            }
        }
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                if (borderWidth != value)
                {
                    borderWidth = value;
                    RaiseBorderWidthChangedEvent(borderWidth);
                }
            }
        }
        public int TotalStates
        {
            get => totalStates;
            set 
            {
              if (totalStates != value)
                {
                    totalStates = value;
                    CorePropertyChanged();
                }
            }
        }
        public bool WrapAround
        {
            get => wrapAround;
            set
            {
                if (wrapAround != value)
                {
                    wrapAround = value;
                    CorePropertyChanged();
                }
            }
        }
        public int CurrentState
        {
            get => currentState;
            set
            {
                if (currentState != value)
                {
                    if (value < TotalStates)
                    {
                        currentState = value;
                        RaiseCurrentStateChangedEvent(currentState);
                    }
                    else
                    {
                        throw new Exception($"Current state: {value} is not in range 0 to {TotalStates-1}");
                    }
                }
            }
        }

       /* public List<Color> StateColors
        {
            get => stateColors;
            set
            {
                if (stateColors != value)
                {
                    stateColors = value;
                }
            }
        }*/

        // Event Raisers
        private void RaiseShapeChangedEvent(LightIndicatorShape shape)
        {
            RecreateRegion();
            ShapeChanged?.Invoke(shape);
        }
        private void RaiseBorderWidthChangedEvent(int borderWidth)
        {
            BorderWidthChanged?.Invoke(borderWidth);
        }
        private void RaiseCurrentStateChangedEvent(int currentState) 
        {
            StateChanged?.Invoke(currentState);
        }
        private void TLightIndicator_Resize(object sender, EventArgs e)
        {
            RecreateRegion();
        }
        private void CorePropertyChanged()
        {

        }

        // Functions
        private void RecreateRegion()
        {
            switch(Shape)
            {
                default:
                case LightIndicatorShape.Rectangle:
                    {
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddLine(0,0,Width,0);
                        gp.AddLine(Width, Height, 0, Height);
                        Region = new Region(gp);
                        break;
                    }
                case LightIndicatorShape.Circle:
                    {
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddEllipse(0, 0, Width, Height);
                        Region = new Region(gp);
                        break;
                    }
                case LightIndicatorShape.Diamond:
                    {
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddPolygon(new Point[] {
                            new Point(Width/2, 0),
                            new Point(Width, Height/2),
                            new Point(Width/2 , Height),
                            new Point(0,Height/2)
                        });
                        Region = new Region(gp);
                        break;
                    }
            }
        }
        public TLightIndicator()
        {
            InitializeComponent();
        }
        public void NextState()
        {
            if (CurrentState + 1 >= TotalStates)
            {
                CurrentState = 0;
            }
            else
            {
                CurrentState++;
            }
        }
        public void PreviousState()
        {
            if (CurrentState-1 < 0)
            {
                CurrentState = TotalStates-1;
            }
            else
            {
                CurrentState--;
            }
        }
        public void SetState(int newState)
        {
            CurrentState = newState;
        }
    }
}
