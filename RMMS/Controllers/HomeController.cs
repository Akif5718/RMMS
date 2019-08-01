using Framework.Constants;
using Framework.Objects;
using Newtonsoft.Json;
using RMMS.Framework.Attributes;
using RMMS.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //var User = HttpUtil.UserProfile;
            return View();
        }
        //[RMMSAuthorize(EnumCollection.UserTypeEnum.Admin)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UnAuthorized()
        {
            //ViewBag.Message = "You";

            return View();
        }
    }
}