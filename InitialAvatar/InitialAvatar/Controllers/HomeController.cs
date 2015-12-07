using InitialAvatar.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public FileResult DownloadImage(string initials, string color, int size = 120)
        {
            var manager = new DrawingManager();
            return File(manager.DrawInitialAvatar(initials, size, color), "image/png");
        }
    }
}