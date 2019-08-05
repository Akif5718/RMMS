using RMMS.Framework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMMS.Controllers
{
    public class ManageProfileController : BaseController
    {
        // GET: ManageProfile
        [Authorize]
        public ActionResult UserRequests()
        {
            return View();
        }
    }
}