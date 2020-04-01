using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Screenulate.Forms
{
    public partial class ScreenshotForm : Form
    {
        private Point _click1, _click2;

        public Bitmap Bitmap { get; private set; }
        public Rectangle Rectangle { get; set; }
        public int BorderWidth { get; set; } = 3;

        private Rectangle LastRectangle { get; set; }

        public ScreenshotForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {
            Rectangle allScreenBounds = new Rectangle();
            foreach (Screen s in Screen.AllScreens)
            {
                allScreenBounds = Rectangle.Union(allScreenBounds, s.Bounds);
            }

            Rectangle windowBounds = Screen.AllScreens.Aggregate(Rectangle.Empty,
                (rectangle, screen) => Rectangle.Union(rectangle, screen.Bounds));

            this.ClientSize = windowBounds.Size;
            this.Location = windowBounds.Location;
            this.BringToFront();
            this.Activate();
        }

        private void ScreenshotForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.Black);
            if (_click1 != Point.Empty)
            {
                var rect = CalculateBounds(_click1, MousePosition);
                g.DrawRectangle(new Pen(Color.Red, BorderWidth), rect);
            }
            else if (Rectangle != Rectangle.Empty)
            {
                g.DrawRectangle(new Pen(Color.Lime, BorderWidth), Rectangle);
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

        public void CaptureImage(Rectangle rect)
        {
            if (rect.Size == Size.Empty)
                return;
            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }

            Bitmap = bitmap;
            Rectangle = rect;
        }

        private void ScreenshotForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _click2 = e.Location;
                this.Opacity = 0;
                this.Close();

                CaptureImage(CalculateBounds(_click1, MousePosition));
            }
        }

        private void ScreenshotForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var rect = CalculateBounds(_click1, MousePosition);
                var invalidationTarget = Rectangle.Union(LastRectangle, rect);
                LastRectangle = rect;
                invalidationTarget.Inflate(BorderWidth, BorderWidth);
                Invalidate(invalidationTarget);
            }
        }

        private void ScreenshotForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _click1 = e.Location;
        }

        public static Rectangle CalculateBounds(Point a, Point b)
        {
            int minX = Math.Min(a.X, b.X);
            int minY = Math.Min(a.Y, b.Y);
            int maxX = Math.Max(a.X, b.X);
            int maxY = Math.Max(a.Y, b.Y);

            var rect = new Rectangle(minX, minY, maxX - minX, maxY - minY);
            return rect;
        }
    }
}