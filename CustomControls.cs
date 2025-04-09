using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PersonnelFinanceManager
{
    public static class CustomControls
    {
        public static void ApplyRoundedCorners(Button button, PaintEventArgs e)
        {
            if (button == null) return;

            int cornerRadius = 30;
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;
            int width = button.Width;
            int height = button.Height;

            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(width - diameter, 0, diameter, diameter, 270, 90);
            path.AddArc(width - diameter, height - diameter, diameter, diameter, 0, 90);
            path.AddArc(0, height - diameter, diameter, diameter, 90, 90); 
            path.CloseFigure();

            button.Region = new Region(path);

            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }
    }
}
