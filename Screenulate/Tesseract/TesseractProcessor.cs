using System;
using System.Diagnostics;
using System.IO;
using Emgu.CV;

namespace Screenulate.Tesseract
{
    class TesseractEventArgs
    {
        public string Text { get; set; }
        public string ProcessImagePath { get; set; } = "tessinput.tif";
    }

    class TesseractProcessor
    {
        private string TesseractPath { get; }
        private IImage Image { get; }

        public TesseractProcessor(string tesseractPath, IImage image)
        {
            TesseractPath = tesseractPath;
            Image = image;
        }

        public void Process()
        {
            string tempFilePath = Path.GetTempFileName();
            string baseFilePath = Path.ChangeExtension(tempFilePath, null);
            string imageFilePath = Path.ChangeExtension(tempFilePath, "png");
            string outputFilePath = Path.ChangeExtension(tempFilePath, "txt");
            string tempDirectory = Path.GetTempPath();

            Image.Save(imageFilePath);

            var tesseractProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = TesseractPath,
                    Arguments = $@"""{imageFilePath}"" ""{baseFilePath}"" -l jpn --psm 6 -c tessedit_write_images=1",
                    WorkingDirectory = tempDirectory,
                    CreateNoWindow = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };
            tesseractProcess.Start();

            tesseractProcess.Exited += (o, args) =>
            {
                try
                {
                    string text = File.ReadAllText(outputFilePath);
                    string processImagePath = Path.Combine(tempDirectory, "tessinput.tif");
                    Completed?.Invoke(this, new TesseractEventArgs()
                    {
                        Text = text, ProcessImagePath = processImagePath
                    });
                }
                catch (Exception e)
                {
                    Error?.Invoke(this, new ErrorEventArgs(e));
                }
                finally
                {
                    Finished?.Invoke(this, null);
                }
            };
        }

        public delegate void TesseractEventHandler(object sender, TesseractEventArgs e);

        public event TesseractEventHandler Completed;
        public event ErrorEventHandler Error;
        public event EventHandler Finished;
    }
}