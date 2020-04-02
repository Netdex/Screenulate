using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Gma.System.MouseKeyHook;
using Screenulate.Forms;
using Screenulate.NLP;
using Screenulate.Tesseract;

namespace Screenulate
{
    public partial class Screenulate : Form
    {
        private JapaneseParser JpParser { get; set; }

        private Rectangle _lastScreenshotRect;
        private RikaiForm _rikaiForm;


        public Screenulate()
        {
            InitializeComponent();
        }

        private void Screenulate_Load(object sender, EventArgs e)
        {
            btnLoadTesseractFile.Text = openFileDialogTesseract.FileName;

            JpParser = new JapaneseParser();
            var jpParserLoadTask = Task.Run(JpParser.Load);
            jpParserLoadTask.ContinueWith(Screenulate_ParserLoaded);

            var test = Deinflector.Deinflect("されました");

            Subscribe();
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            ProcessFrame(false);
        }

        public void Screenulate_ParserLoaded(Task t)
        {
            //var test = JpParser.Annotate("新しいコロナウイルスに「アビガン」を使う試験が始まる");
            BeginInvoke((Action) (() =>
            {
                parserLoadProgress.Style = ProgressBarStyle.Blocks;
                _rikaiForm = new RikaiForm();
                _rikaiForm.Show(this);
                _rikaiForm.Value = JpParser.Annotate(richTextBox.Text);
            }));
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (JpParser.IsLoaded)
            {
                _rikaiForm.Value = JpParser.Annotate(richTextBox.Text);
            }
        }

        public void Screenulate_TextReceived(string text)
        {
            ITextTransform transform = new VnTextTransform();
            var processText = transform.Transform(text);
            richTextBox.Text = processText;
        }

        private void ProcessFrame(bool instant)
        {
            if (!btnScreenshot.Enabled)
                return;
            if (!File.Exists(openFileDialogTesseract.FileName))
            {
                MessageBox.Show(this, "Tesseract-OCR path not configured!", "Screenulate",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tesseractProgress.Style = ProgressBarStyle.Marquee;
            btnScreenshot.Enabled = false;

            var screenshotForm = new ScreenshotForm {Rectangle = _lastScreenshotRect};

            if (instant)
                screenshotForm.CaptureImage(_lastScreenshotRect);
            else
                screenshotForm.ShowDialog(this);

            if (screenshotForm.Bitmap == null)
            {
                tesseractProgress.Style = ProgressBarStyle.Blocks;
                btnScreenshot.Enabled = true;
                return;
            }

            _lastScreenshotRect = screenshotForm.Rectangle;

            IImage oldImage = previewBox.Image;
            previewBox.Image = new Image<Bgr, byte>(screenshotForm.Bitmap);
            oldImage?.Dispose();
            var processor = new TesseractProcessor(openFileDialogTesseract.FileName, previewBox.DisplayedImage);
            processor.Process();

            processor.Completed += TesseractProcessor_Completed;
            processor.Finished += TesseractProcessor_Finished;
            processor.Error += (sender, args) =>
            {
                MessageBox.Show($@"{args.GetException().Message}", "Screenulate", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };
        }

        private void TesseractProcessor_Completed(object sender, TesseractEventArgs args)
        {
            IImage oldProcessImage = processBox.Image;
            processBox.Image = CvInvoke.Imread(args.ProcessImagePath, LoadImageType.Color);
            oldProcessImage?.Dispose();

            Screenulate_TextReceived(args.Text);
        }

        private void TesseractProcessor_Finished(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker) delegate
            {
                tesseractProgress.Style = ProgressBarStyle.Blocks;
                tesseractProgress.Value = 100;
                btnScreenshot.Enabled = true;
            });
        }

        private void btnLoadTesseractFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialogTesseract.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                btnLoadTesseractFile.Text = openFileDialogTesseract.FileName;
            }
        }

        private IKeyboardMouseEvents _globalHook;

        public void Subscribe()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyUp += GlobalHookKeyUp;
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde)
            {
                ProcessFrame(true);
            }
            else if (e.Alt && e.KeyCode == Keys.Q)
            {
                ProcessFrame(false);
            }
        }

        public void Unsubscribe()
        {
            _globalHook.KeyUp -= GlobalHookKeyUp;
            _globalHook.Dispose();
        }

        private void Screenulate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Unsubscribe();
        }
    }
}