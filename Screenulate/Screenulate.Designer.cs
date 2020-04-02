namespace Screenulate
{
    partial class Screenulate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnScreenshot = new System.Windows.Forms.Button();
            this.btnLoadTesseractFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialogTesseract = new System.Windows.Forms.OpenFileDialog();
            this.tesseractProgress = new System.Windows.Forms.ProgressBar();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.previewBox = new Emgu.CV.UI.ImageBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.processBox = new Emgu.CV.UI.ImageBox();
            this.parserLoadProgress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScreenshot
            // 
            this.btnScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScreenshot.Location = new System.Drawing.Point(1087, 555);
            this.btnScreenshot.Margin = new System.Windows.Forms.Padding(6);
            this.btnScreenshot.Name = "btnScreenshot";
            this.btnScreenshot.Size = new System.Drawing.Size(205, 81);
            this.btnScreenshot.TabIndex = 5;
            this.btnScreenshot.Text = "Screenshot";
            this.btnScreenshot.UseVisualStyleBackColor = true;
            this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // btnLoadTesseractFile
            // 
            this.btnLoadTesseractFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTesseractFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTesseractFile.Location = new System.Drawing.Point(279, 19);
            this.btnLoadTesseractFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnLoadTesseractFile.Name = "btnLoadTesseractFile";
            this.btnLoadTesseractFile.Size = new System.Drawing.Size(1013, 33);
            this.btnLoadTesseractFile.TabIndex = 0;
            this.btnLoadTesseractFile.Text = "...";
            this.btnLoadTesseractFile.UseVisualStyleBackColor = true;
            this.btnLoadTesseractFile.Click += new System.EventHandler(this.btnLoadTesseractFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tesseract-OCR Executable";
            // 
            // openFileDialogTesseract
            // 
            this.openFileDialogTesseract.FileName = "C:\\Program Files\\Tesseract-OCR\\tesseract.exe";
            this.openFileDialogTesseract.Filter = "Tesseract-OCR|tesseract.exe";
            // 
            // tesseractProgress
            // 
            this.tesseractProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tesseractProgress.Location = new System.Drawing.Point(736, 555);
            this.tesseractProgress.Margin = new System.Windows.Forms.Padding(6);
            this.tesseractProgress.Name = "tesseractProgress";
            this.tesseractProgress.Size = new System.Drawing.Size(339, 38);
            this.tesseractProgress.TabIndex = 6;
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox.Font = new System.Drawing.Font("MS PGothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox.Location = new System.Drawing.Point(610, 96);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(682, 450);
            this.richTextBox.TabIndex = 7;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // previewBox
            // 
            this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(585, 217);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 2;
            this.previewBox.TabStop = false;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer.Location = new System.Drawing.Point(19, 96);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.previewBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.processBox);
            this.splitContainer.Size = new System.Drawing.Size(585, 450);
            this.splitContainer.SplitterDistance = 217;
            this.splitContainer.TabIndex = 8;
            // 
            // processBox
            // 
            this.processBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.processBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.processBox.Location = new System.Drawing.Point(0, 0);
            this.processBox.Name = "processBox";
            this.processBox.Size = new System.Drawing.Size(585, 229);
            this.processBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.processBox.TabIndex = 2;
            this.processBox.TabStop = false;
            // 
            // parserLoadProgress
            // 
            this.parserLoadProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.parserLoadProgress.Location = new System.Drawing.Point(736, 598);
            this.parserLoadProgress.Margin = new System.Windows.Forms.Padding(6);
            this.parserLoadProgress.Name = "parserLoadProgress";
            this.parserLoadProgress.Size = new System.Drawing.Size(339, 38);
            this.parserLoadProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.parserLoadProgress.TabIndex = 6;
            this.parserLoadProgress.Value = 100;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(632, 562);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tesseract";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(566, 605);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Japanese Engine";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Screenulate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 651);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.parserLoadProgress);
            this.Controls.Add(this.tesseractProgress);
            this.Controls.Add(this.btnScreenshot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLoadTesseractFile);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(973, 606);
            this.Name = "Screenulate";
            this.Text = "Screenulate";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Screenulate_FormClosed);
            this.Load += new System.EventHandler(this.Screenulate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.processBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnScreenshot;
        private System.Windows.Forms.Button btnLoadTesseractFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialogTesseract;
        private System.Windows.Forms.ProgressBar tesseractProgress;
        private System.Windows.Forms.RichTextBox richTextBox;
        private Emgu.CV.UI.ImageBox previewBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Emgu.CV.UI.ImageBox processBox;
        private System.Windows.Forms.ProgressBar parserLoadProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

