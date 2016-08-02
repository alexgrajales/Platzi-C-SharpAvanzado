using EmotionPlatziWeb.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatziWeb.Controllers
{
    public class EmoUpLoadController : Controller
    {
        string serverFolderPath;
        EmotionHelper emoHelper;
        string key;
        public EmoUpLoadController()
        {
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            emoHelper = new EmotionHelper();
        }
        // GET: EmoUpLoad
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file?.ContentLength > 0)
            {
                var pictureName = Guid.NewGuid().ToString();
                pictureName += Path.GetExtension(file.FileName);
                var route = Server.MapPath(serverFolderPath);//convertir una ruta de servidor a una local
                route = route + "/" + pictureName;                
                file.SaveAs(route);
                emoHelper.DetectAndExtracFaces(file.InputStream);
            }

            return View();
        }
    }
}