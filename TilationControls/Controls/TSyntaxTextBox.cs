using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TilationControls.DS;

namespace TilationControls.Controls
{
    public class TSyntaxTextBox : RichTextBox
    {
        bool format_text = true;

        bool format_again = false;

        List<SyntaxHighlight> highlights = null;

        BackgroundWorker bwFormatter;

        List<SyntaxHighlight> Highlights
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
        public void ClearHighlights()
        {
            Highlights.Clear();
        }

        public void AddHighlight(SyntaxHighlight NewHighlight)
        {
            if (!Highlights.Contains(NewHighlight))
            {
                Highlights.Add(NewHighlight);
            }
        }

        public void SetHighlights(List<SyntaxHighlight> NewHighlights)
        {
            Highlights = NewHighlights;
        }

        
        public TSyntaxTextBox()
        {
            bwFormatter = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
            };

            bwFormatter.DoWork += BwFormatter_DoWork;

            bwFormatter.RunWorkerCompleted += BwFormatter_RunWorkerCompleted;

            Highlights = new List<SyntaxHighlight>()
            {
                new SyntaxHighlight("int"           , Color.Blue    , FontStyle.Bold),
                new SyntaxHighlight("string"        , Color.Blue    , FontStyle.Bold),
                new SyntaxHighlight("String"        , Color.Green   , FontStyle.Bold),
                new SyntaxHighlight("new"           , Color.Blue    , FontStyle.Bold),
                new SyntaxHighlight("GraphicsPath"  , Color.Green   , FontStyle.Bold)
            };
        }

        private void BwFormatter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var Selection = SelectionStart;
            if (format_again)
            {
                format_again = false;
                FormatText();
            }
            else
            {
                format_text = false;
                SetRTF(((string)e.Result));
                format_text = true;
            }
            SelectionStart = Selection;
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
            RichTextBox _buffer = new RichTextBox
            {
                Font = _args.Font
            };
            e.Result = null;

            if (_args.SyntaxHighlightsArgs != null)
            {
                _buffer.Text = _args.Text;
                foreach (SyntaxHighlight format in _args.SyntaxHighlightsArgs)
                {
                    if (e.Cancel)
                    {
                        break;
                    }

                    string pattern = $"\\b{format.StringMatch}\\b";

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
            FormatText();
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
