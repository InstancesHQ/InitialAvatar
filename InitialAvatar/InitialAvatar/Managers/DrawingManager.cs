using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace InitialAvatar.Managers
{
    public class DrawingManager
    {
        public byte[] DrawInitialAvatar(string initials)
        {
            initials = initials.ToUpper();
            var fontSize = initials.Length > 2 ? 30 : 40;
            var font = new Font("Arial", fontSize);
            var stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.FitBlackBox
            };
            var image = new Bitmap(120, 120);
            var drawing = Graphics.FromImage(image);
            var rect = new Rectangle(0, 4, 120, 120);
            drawing.Clear(Color.Blue);
            drawing.DrawString(initials, font, Brushes.White, rect, stringFormat);
            drawing.Flush();
            drawing.Dispose();
            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                return memory.ToArray();
            }
        }
    }
}