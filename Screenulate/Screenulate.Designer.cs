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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.previewBox = new Emgu.CV.UI.ImageBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.processBox = new Emgu.CV.UI.ImageBox();
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
            this.btnScreenshot.Font = new System.Drawing.Font("MS PGothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScreenshot.Location = new System.Drawing.Point(1223, 478);
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
            this.btnLoadTesseractFile.Font = new System.Drawing.Font("MS PGothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadTesseractFile.Location = new System.Drawing.Point(19, 54);
            this.btnLoadTesseractFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnLoadTesseractFile.Name = "btnLoadTesseractFile";
            this.btnLoadTesseractFile.Size = new System.Drawing.Size(1409, 33);
            this.btnLoadTesseractFile.TabIndex = 0;
            this.btnLoadTesseractFile.Text = "...";
            this.btnLoadTesseractFile.UseVisualStyleBackColor = true;
            this.btnLoadTesseractFile.Click += new System.EventHandler(this.btnLoadTesseractFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS PGothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tesseract-OCR Executable";
            // 
            // openFileDialogTesseract
            // 
            this.openFileDialogTesseract.FileName = "C:\\Program Files\\Tesseract-OCR\\tesseract.exe";
            this.openFileDialogTesseract.Filter = "Tesseract-OCR|tesseract.exe";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(872, 478);
            this.progressBar.Margin = new System.Windows.Forms.Padding(6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(339, 81);
            this.progressBar.TabIndex = 6;
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
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(818, 373);
            this.richTextBox.TabIndex = 7;
            this.richTextBox.Text = "";
            // 
            // previewBox
            // 
            this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(585, 180);
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
            this.splitContainer.Size = new System.Drawing.Size(585, 373);
            this.splitContainer.SplitterDistance = 180;
            this.splitContainer.TabIndex = 8;
            // 
            // processBox
            // 
            this.processBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.processBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processBox.Location = new System.Drawing.Point(0, 0);
            this.processBox.Name = "processBox";
            this.processBox.Size = new System.Drawing.Size(585, 189);
            this.processBox.TabIndex = 2;
            this.processBox.TabStop = false;
            // 
            // Screenulate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 574);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnScreenshot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLoadTesseractFile);
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox richTextBox;
        private Emgu.CV.UI.ImageBox previewBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Emgu.CV.UI.ImageBox processBox;
    }
}

