using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Screenulate.NLP;

namespace Screenulate.Forms
{
    public partial class RikaiForm : Form
    {
        private AnnotatedString _value;

        private int _lastIndex = 0;

        public AnnotatedString Value
        {
            get => _value;
            set
            {
                _value = value;
                RikaiForm_ValueChanged(value);
            }
        }

        public RikaiForm()
        {
            InitializeComponent();
        }

        private void RikaiForm_Load(object sender, EventArgs e)
        {
        }

        private void RikaiForm_ValueChanged(AnnotatedString value)
        {
            richTextBox.Text = value.Text;
            richTextBox_MouseMove(null, null);
        }

        private void RikaiForm_SelectedIndexChanged(int index)
        {

        }

        private void richTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e == null || Value == null)
                return;

            // Get the character that is currently under the cursor. It turns out 
            // that doing this is actually much more difficult than you might think.

            var index = richTextBox.GetCharIndexFromPosition(e.Location);
            var actualPoint = richTextBox.GetPositionFromCharIndex(index);
            if (e.Location.X < actualPoint.X)
            {
                if (index > 0)
                {
                    // Apply an offset such that the closest character is measured from 
                    // the characters' centers rather than their left edge.
                    var size = TextRenderer.MeasureText(Value.Text[index - 1].ToString(), richTextBox.Font);
                    if (richTextBox.GetCharIndexFromPosition(
                        new Point(e.Location.X - size.Width / 2, actualPoint.Y)) != index)
                    {
                        index--;
                    }
                }
            }

            if (index != _lastIndex)
            {
                _lastIndex = index;

                var tokens = Value.JapaneseTokens[index];
                int bestLength = tokens.FirstOrDefault()?.Length ?? 0;
                if (bestLength > 0)
                {
                    richTextBox.Select(index, bestLength);
                }
                else
                {
                    richTextBox.DeselectAll();
                }
                RikaiForm_SelectedIndexChanged(index);
            }
        }
    }
}