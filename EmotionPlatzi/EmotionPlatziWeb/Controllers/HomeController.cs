using EmotionPlatziWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatziWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {            
            ViewBag.WelcomeMessage = "Hola Mundo desde viewbag";
            return View();
        }

        public ActionResult IndexAlt()
        {
            var modelo = new Home();
            modelo.WelcomeMessage =
            ViewBag.WelcomeMessage = "Hola Mundo desde modelo";
            return View(modelo);
        }
    }
}