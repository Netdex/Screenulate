using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenulate
{
    public partial class ScreenshotForm : Form
    {
        private Point _click1, _click2;

        public Image Image { get; private set; }
        public Rectangle Rectangle { get; set; }

        public ScreenshotForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = Screen.PrimaryScreen.Bounds.Size;
            this.Location = Point.Empty;
            this.BringToFront();
            this.Activate();
        }

        private void ScreenshotForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.Black);
            if (_click1 != Point.Empty)
            {
                int minX = Math.Min(_click1.X, MousePosition.X);
                int minY = Math.Min(_click1.Y, MousePosition.Y);
                int maxX = Math.Max(_click1.X, MousePosition.X);
                int maxY = Math.Max(_click1.Y, MousePosition.Y);

                var rect = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                g.DrawRectangle(new Pen(Color.Red, 3), rect);
            }
            else if (Rectangle != Rectangle.Empty)
            {
                g.DrawRectangle(new Pen(Color.Lime, 3), Rectangle);
            }
        }

        private void ScreenshotForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Space:
                    if (Rectangle != Rectangle.Empty)
                    {
                        CaptureImage(Rectangle);
                        this.Close();
                    }
                    break;
            }
        }

        private void ScreenshotForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _click2 = e.Location;
                this.Opacity = 0;
                this.Close();

                int minX = Math.Min(_click1.X, MousePosition.X);
                int minY = Math.Min(_click1.Y, MousePosition.Y);
                int maxX = Math.Max(_click1.X, MousePosition.X);
                int maxY = Math.Max(_click1.Y, MousePosition.Y);

                var rect = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                CaptureImage(rect);
            }
        }

        public void CaptureImage(Rectangle rect)
        {
            if (rect.Size == Size.Empty)
                return;
            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }
            Image = bitmap;
            Rectangle = rect;
        }

        private void ScreenshotForm_MouseMove(object sender, MouseEventArgs e)
        {
            Refresh();
        }

        private void ScreenshotForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _click1 = e.Location;
        }
    }
}
