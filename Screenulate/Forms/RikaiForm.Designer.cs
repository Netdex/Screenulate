namespace Screenulate.Forms
{
    partial class RikaiForm
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
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageJapanese = new System.Windows.Forms.TabPage();
            this.tabPageKanji = new System.Windows.Forms.TabPage();
            this.tabPageNames = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Font = new System.Drawing.Font("MS PGothic", 20.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(1118, 435);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseMove);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.richTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(1118, 954);
            this.splitContainer.SplitterDistance = 435;
            this.splitContainer.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageJapanese);
            this.tabControl.Controls.Add(this.tabPageKanji);
            this.tabControl.Controls.Add(this.tabPageNames);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1118, 515);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageJapanese
            // 
            this.tabPageJapanese.Location = new System.Drawing.Point(4, 33);
            this.tabPageJapanese.Name = "tabPageJapanese";
            this.tabPageJapanese.Size = new System.Drawing.Size(1110, 478);
            this.tabPageJapanese.TabIndex = 0;
            this.tabPageJapanese.Text = "Japanese";
            this.tabPageJapanese.UseVisualStyleBackColor = true;
            // 
            // tabPageKanji
            // 
            this.tabPageKanji.Location = new System.Drawing.Point(4, 33);
            this.tabPageKanji.Name = "tabPageKanji";
            this.tabPageKanji.Size = new System.Drawing.Size(1110, 478);
            this.tabPageKanji.TabIndex = 1;
            this.tabPageKanji.Text = "Kanji";
            this.tabPageKanji.UseVisualStyleBackColor = true;
            // 
            // tabPageNames
            // 
            this.tabPageNames.Location = new System.Drawing.Point(4, 33);
            this.tabPageNames.Name = "tabPageNames";
            this.tabPageNames.Size = new System.Drawing.Size(1110, 478);
            this.tabPageNames.TabIndex = 2;
            this.tabPageNames.Text = "Names";
            this.tabPageNames.UseVisualStyleBackColor = true;
            // 
            // RikaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 954);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "RikaiForm";
            this.Text = "Rikai?";
            this.Load += new System.EventHandler(this.RikaiForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageJapanese;
        private System.Windows.Forms.TabPage tabPageKanji;
        private System.Windows.Forms.TabPage tabPageNames;
    }
}