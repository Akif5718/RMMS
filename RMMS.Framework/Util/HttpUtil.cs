using Framework.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMMS.Framework.Util
{
    public static class HttpUtil
    {
        public static UserProfile UserProfile
        {
            get
            {
                try
                {
                    var userProfile = JsonConvert.DeserializeObject<UserProfile>(HttpContext.Current.User.Identity.Name);
                    return userProfile;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
