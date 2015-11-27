using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InitialAvatar.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("avatar/{initials}")]
        public FileResult DownloadImage(string initials)
        {
            var fontFamily = new FontFamily("Arial");
            var font = new Font(fontFamily, 60, FontStyle.Regular, GraphicsUnit.Pixel);
            var stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            var img = new Bitmap(120, 120);
            var drawing = Graphics.FromImage(img);
            drawing.Clear(Color.Blue);
            var textBrush = new SolidBrush(Color.White);
            var pointf = new Point(60, 66);
            drawing.DrawString(initials, font, textBrush, pointf, stringFormat);
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Jpeg);
                return File(memory.ToArray(), "image/jpeg");
            }
        }
    }
}