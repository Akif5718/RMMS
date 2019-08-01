using Framework.Constants;
using RMMS.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RMMS.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class RMMSAuthorize : FilterAttribute, IAuthorizationFilter
    {
        public EnumCollection.UserTypeEnum UserTypeEnum;
        public RMMSAuthorize(EnumCollection.UserTypeEnum userTypeEnum)
        {
            UserTypeEnum = userTypeEnum;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated || HttpUtil.UserProfile == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if(HttpUtil.UserProfile.UserTypeID!=(int)UserTypeEnum)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        Controller = "Home",
                        Action = "UnAuthorized"
                    }
                    
                    ));
                return;
            }
        }
    }
}
