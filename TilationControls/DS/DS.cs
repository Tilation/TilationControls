using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.DS
{

    public struct SyntaxHighlight
    {
        public string StringMatch;
        public Color TextColor;
        public FontStyle Style;
        public Color BackColor;

        public SyntaxHighlight(string stringMatch, Color textColor) : this()
        {
            StringMatch = stringMatch;
            TextColor = textColor;
            BackColor = Color.Transparent;
            Style = FontStyle.Regular;
        }

        public SyntaxHighlight(string stringMatch, Color textColor, FontStyle style) : this()
        {
            StringMatch = stringMatch;
            TextColor = textColor;
            Style = style;
            BackColor = Color.Transparent;
        }

        public SyntaxHighlight(string stringMatch, Color textColor, Color? backColor = null, FontStyle style = FontStyle.Regular)
        {
            StringMatch = stringMatch;
            TextColor = textColor;
            BackColor = backColor ?? Color.Transparent;
            Style = style;
        }
    }

    public struct SyntaxHighlightArgs
    {
        public Color DefaultColor;
        public int CursorPos;
        public string Text;
        public List<SyntaxHighlight> SyntaxHighlightsArgs;
        public Font Font;
        public SyntaxHighlightArgs(int cursorPos, string text, List<SyntaxHighlight> highlights, Color defaultColor, Font font)
        {
            CursorPos = cursorPos;
            Text = text;
            SyntaxHighlightsArgs = highlights;
            DefaultColor = defaultColor;
            Font = font;
        }
    }
}
