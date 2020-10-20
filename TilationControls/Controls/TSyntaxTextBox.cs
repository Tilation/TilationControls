using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TilationControls.DS;

namespace TilationControls.Controls
{
    public class TSyntaxTextBox : RichTextBox
    {
        bool format_text = true;

        bool format_again = false;

        Dictionary<string, SyntaxHighlight> highlights = null;

        
        Dictionary<string, SyntaxHighlight> Highlights
        {
            get => highlights;
            set
            {
                if (highlights != value && value != null)
                {
                    highlights = value;
                    HighlightsChanged();
                }
            }
        }

        public void SetHighlights(Dictionary<string, SyntaxHighlight> NewHighlights)
        {
            Highlights = NewHighlights;
        }

        BackgroundWorker bwFormatter;

        public TSyntaxTextBox()
        {
            bwFormatter = new BackgroundWorker();
            bwFormatter.WorkerSupportsCancellation = true;

            bwFormatter.DoWork += BwFormatter_DoWork;

            bwFormatter.RunWorkerCompleted += BwFormatter_RunWorkerCompleted;

            Highlights = new Dictionary<string, SyntaxHighlight>()
            {
                { "\\bint\\b",          new SyntaxHighlight( Color.Blue ,   null, FontStyle.Bold)},
                { "\\bstring\\b",       new SyntaxHighlight( Color.Blue ,   null, FontStyle.Bold)},
                { "\\bString\\b",       new SyntaxHighlight( Color.Green ,   null, FontStyle.Bold)},
                { "\\bnew\\b",       new SyntaxHighlight( Color.Blue ,   null, FontStyle.Bold)},
                { "\\bGraphicsPath\\b", new SyntaxHighlight( Color.Green ,  null, FontStyle.Bold)}

            };
        }

        private void BwFormatter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            format_text = false;
            var Selection = SelectionStart;
            SetRTF(((string)e.Result));
            SelectionStart = Selection;
            format_text = true;

            if (format_again)
            {
                format_again = false;
                FormatText();
            }
        }

        private void SetRTF(string result)
        {
            SuspendLayout();
            Rtf = result;
            ResumeLayout();
        }

        private void BwFormatter_DoWork(object sender, DoWorkEventArgs e)
        {
            SyntaxHighlightArgs _args = (SyntaxHighlightArgs)e.Argument;
            RichTextBox _buffer = new RichTextBox();
            _buffer.Font = _args.Font;
            e.Result = null;

            if (_args.SyntaxHighlightsArgs != null)
            {
                _buffer.Text = _args.Text;
                foreach (KeyValuePair<string, SyntaxHighlight> kv in _args.SyntaxHighlightsArgs)
                {
                    if (e.Cancel)
                    {
                        break;
                    }

                    string pattern = kv.Key;
                    SyntaxHighlight format = kv.Value;

                    foreach (Match match in Regex.Matches(_args.Text, pattern))
                    {
                        if (e.Cancel)
                        {
                            break;
                        }

                        _buffer.SelectionStart = match.Index;
                        _buffer.SelectionLength = match.Length;

                        _buffer.SelectionColor = format.TextColor;
                        _buffer.SelectionBackColor = format.BackColor;

                        if (format.Style != FontStyle.Regular)
                        {
                            _buffer.SelectionFont = new Font(_buffer.Font, format.Style);
                        }
                    };
                }
                if (e.Cancel)
                {
                    e.Result = null;
                }
                else
                {
                    e.Result = _buffer.Rtf;
                }
            }
        }

        private void HighlightsChanged()
        {
            //FormatText();
        }

        public void FormatText()
        {
            if (Text.Length > 0 && Highlights != null && Highlights.Count > 0 && format_text)
            {
                SyntaxHighlightArgs args = new SyntaxHighlightArgs(SelectionStart, Text, Highlights, SelectionColor, Font);
                if (bwFormatter.IsBusy)
                {
                    format_again = true;
                }
                else
                {
                    bwFormatter.RunWorkerAsync(args);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            FormatText();
        }

        public void AppendLine(string line)
        {
            AppendText($"{line}{Environment.NewLine}");
        }
    }
}
