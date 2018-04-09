using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Screenulate
{
    public partial class Screenulate : Form
    {
        private string ScreenshotImagePath;
        private Rectangle LastScreenshotRect;

        public Screenulate()
        {
            InitializeComponent();
        }

        private void Screenulate_Load(object sender, EventArgs e)
        {
            ScreenshotImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
            Directory.CreateDirectory(ScreenshotImagePath);
            btnLoadTesseractFile.Text = openFileDialogTesseract.FileName;

            Subscribe();
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            DoProcessing(false);
        }

        private void DoProcessing(bool instant)
        {
            if (!btnScreenshot.Enabled)
                return;
            if (!File.Exists(openFileDialogTesseract.FileName))
            {
                MessageBox.Show(this, "Tesseract-OCR path not configured!", "Screenulate - Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressBar.Style = ProgressBarStyle.Marquee;
            btnScreenshot.Enabled = false;

            var sf = new ScreenshotForm
            {
                Rectangle = LastScreenshotRect
            };
            if (instant)
            {
                sf.CaptureImage(LastScreenshotRect);
            }
            else
            {
                sf.ShowDialog(this);
            }
            if (sf.Image == null)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                btnScreenshot.Enabled = true;
                return;
            }

            LastScreenshotRect = sf.Rectangle;

            long time = DateTime.Now.ToFileTime();
            var id = Path.Combine(ScreenshotImagePath, time + "");
            var imagePath = id + ".png";
            var textPath = id + ".txt";

            sf.Image.Save(imagePath);

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = openFileDialogTesseract.FileName,
                    Arguments = $@"""{imagePath}"" ""{id}"" -l jpn --psm 6",
                    WorkingDirectory = Path.GetDirectoryName(openFileDialogTesseract.FileName),
                    CreateNoWindow = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };
            proc.Start();

            proc.Exited += (o, args) =>
            {
                try
                {
                    var text = File.ReadAllText(textPath);
                    text = text.Substring(0, text.Length - 1);

                    webBrowser.Navigate(
                        $"https://www.bing.com/translator/?to=en&from=ja&text={HttpUtility.UrlEncode(text)}");
                    //webBrowser.Navigate(
                    //$"https://translate.google.com/#ja/en/{HttpUtility.UrlEncode(text)}");
                }
                catch
                {
                    MessageBox.Show(this, "Error during OCR!", "Screenulate - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.BeginInvoke((MethodInvoker)delegate
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    btnScreenshot.Enabled = true;
                });
            };
        }

        private void btnLoadTesseractFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialogTesseract.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                btnLoadTesseractFile.Text = openFileDialogTesseract.FileName;
            }
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Shift && e.KeyCode == Keys.T)
            {
                DoProcessing(true);
            }
            else if (e.Alt && e.KeyCode == Keys.T)
            {
                DoProcessing(false);
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            m_GlobalHook.Dispose();
        }

        private void Screenulate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Unsubscribe();
        }
    }
}
