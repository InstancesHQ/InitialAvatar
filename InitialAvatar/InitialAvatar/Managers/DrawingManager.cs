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
        public const string RANDOM_COLOR_OPTIONS = "red,indigo,blue,lightblue,cyan,green,amber,deeporange";
        private static readonly Dictionary<string, string> _colorList = new Dictionary<string, string>
        {
            {  "red", "F44336" },
            {  "indigo", "3F51B5" },
            {  "blue", "2196F3" },
            {  "lightblue", "03A9F4" },
            {  "cyan", "00BCD4" },
            {  "green", "4CAF50" },
            {  "amber", "FFC107" },
            {  "deeporange", "FF5722" },
            {  "pink", "E91E63" },
            {  "purple", "9C27B0" },
            {  "deeppurple", "673AB7" },
            {  "teal", "009688" },
            {  "lightgreen", "8BC34A" },
            {  "lime", "CDDC39" },
            {  "yellow", "FFEB3B" },
            {  "orange", "FF9800" },
            {  "brown", "795548" },
            {  "grey", "9E9E9E" },
            {  "white", "FFFFFF" },
            {  "bluegrey", "607D8B" },
            {  "black", "000000" },
        };
        public byte[] DrawInitialAvatar(string initials, int size, string color)
        {
            initials = initials.ToUpper();
            var fontSize = initials.Length > 2 ? size * .25 : size * .333;
            var font = new Font("Segoe UI", (float)fontSize);
            var stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.FitBlackBox
            };
            var image = new Bitmap(size, size);
            var drawing = Graphics.FromImage(image);
            var rect = new Rectangle(0, 0, size, size);
            drawing.Clear(GetColor(color));
            drawing.DrawString(initials, font, Brushes.White, rect, stringFormat);
            drawing.Flush();
            drawing.Dispose();
            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                return memory.ToArray();
            }
        }

        public Color GetColor(string color)
        {
            var isHex = IsValidHex(color);
            if (!isHex)
            {
                var selectedColor = string.Empty;
                if (color != null) _colorList.TryGetValue(color.ToLower(), out selectedColor);
                if (string.IsNullOrEmpty(selectedColor))
                {
                    var rnd = new Random();
                    var colorOptionsList = RANDOM_COLOR_OPTIONS.Split(',');
                    color = colorOptionsList[rnd.Next(0, colorOptionsList.Length - 1)];
                    return GetColor(color);
                }
                color = selectedColor;
            }
            return ColorTranslator.FromHtml($"#{color}");
        }

        public bool IsValidHex(string colorHex)
        {
            return colorHex != null && System.Text.RegularExpressions.Regex.IsMatch(colorHex, @"\A\b[0-9a-fA-F]+\b\Z") && colorHex.Length == 6;
        }
    }
}