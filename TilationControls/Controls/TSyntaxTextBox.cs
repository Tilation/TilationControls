using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilationControls.Controls
{
    public struct TextStyle
    {
        public TextStyle(Color textColor, Color? backColor = null, params FontStyle[] styles)
        {
            Styles = styles;
            BackColor = backColor?? Color.Transparent;
            TextColor = textColor;
        }

        public FontStyle[] Styles { get; set; }
        public Color BackColor { get; set; } 
        public Color TextColor { get; set; }
    }

    class TSyntaxTextBox : RichTextBox
    {
        struct WorkerArgs
        {
            public WorkerArgs(Dictionary<string, TextStyle> highlights, string text)
            {
                Highlights = highlights;
                Text = text;
            }
            public Dictionary<string, TextStyle> Highlights { get; set; }
            public string Text { get; set; }
        }
        int cursorPosition;
        private Dictionary<string, TextStyle> highlights;
        public Dictionary<string, TextStyle> Highlights
        {
            get => highlights;
            set
            {
                if (value != highlights)
                {
                    highlights = value;
                    CallEvent();
                }
            }
        }
        bool internalTextChange = false;
        private void CallEvent()
        {
            ApplyRegexMachine();
            OnHighlightsChange?.Invoke(highlights.Count);
        }

        private void ApplyRegexMachine()
        {
            if (regexWorker.IsBusy)
            {
                regexWorker.CancelAsync();
            }
            else
            {
                regexWorker.RunWorkerAsync(new WorkerArgs(highlights, Text));
            }
        }

        public event Action<int> OnHighlightsChange;

        //private Regex regMachine;
        private BackgroundWorker regexWorker;
        public TSyntaxTextBox()
        {
            
            regexWorker = new BackgroundWorker();
            regexWorker.WorkerReportsProgress = true;
            regexWorker.WorkerSupportsCancellation = true;
            regexWorker.DoWork += ApplyFormatting;
            regexWorker.ProgressChanged += ReportProgress;
            regexWorker.RunWorkerCompleted += FormattingCompleted;

            highlights = new Dictionary<string, TextStyle>();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            cursorPosition = SelectionStart;
            if (!internalTextChange)
            {
                ApplyRegexMachine();
            }
            SelectionStart = cursorPosition;
        }

        private void FormattingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var output = (RichTextBox)e.Result;
            internalTextChange = true;
            Text = output.Text;
            internalTextChange = false;

        }

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ApplyFormatting(object sender, DoWorkEventArgs e)
        {
            var arg = (WorkerArgs)e.Argument;
            var highlights = arg.Highlights;
            var text = arg.Text;

            var buffer = new RichTextBox();
            buffer.Text = text;

            var regMachine = new Regex("\\w");
            var words = regMachine.Matches(text);

            foreach (Match m in words)
            {
                if (m.Success)
                {
                    if (highlights.ContainsKey(m.Value))
                    {
                        var style = highlights[m.Value];

                        buffer.SelectionStart = m.Index;
                        buffer.SelectionLength = m.Length;

                        buffer.SelectionColor = style.TextColor;
                        buffer.SelectionBackColor = style.BackColor;
                        
                    }
                }
            }
            e.Result = buffer;
        }
    }
}
