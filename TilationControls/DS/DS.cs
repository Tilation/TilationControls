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
        public Color BackColor;
        public FontStyle Style;
        public SyntaxHighlight(string stringMatch,Color textColor, Color? backColor = null, FontStyle style = FontStyle.Regular)
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
        public Dictionary<string, SyntaxHighlight> SyntaxHighlightsArgs;
        public Font Font;
        public SyntaxHighlightArgs(int cursorPos, string text, Dictionary<string, SyntaxHighlight> highlights, Color defaultColor, Font font)
        {
            CursorPos = cursorPos;
            Text = text;
            SyntaxHighlightsArgs = highlights;
            DefaultColor = defaultColor;
            Font = font;
        }
    }
}
