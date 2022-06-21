using System;
using System.Web;
using System.Web.Script.Serialization;

namespace ECommerce.Domain.CookiesModel
{
    public class UserCookiesHelper
    {
        public UserCookiesModel Get(string cookieName)
        {
            var settings = new UserCookiesModel();
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && !cookie.Value.Equals("none"))
            {
                string cookieString = HttpUtility.UrlDecode(cookie.Value);
                var jsHelper = new JavaScriptSerializer();
                var obj = jsHelper.Deserialize<UserJSON>(cookieString);
                if (obj != null)
                {
                    settings.UserID = obj.userid;
                }
            }

            return settings;
        }

        public UserCookiesViewModel CreateCookies(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
            {
                var settings = new UserCookiesModel()
                {
                    UserID = Guid.NewGuid().ToString()
                };
                var jsonCookies = new UserJSON()
                {
                    userid = settings.UserID
                };
                var jsHelper = new JavaScriptSerializer();
                cookie = new HttpCookie(cookieName);
                cookie.Value = jsHelper.Serialize(jsonCookies);
                cookie.Expires = DateTime.Now.AddYears(1);
                cookie.Path = "/";

                return new UserCookiesViewModel()
                {
                    Cookie = cookie,
                    Model = settings

                };
            }

            return null;
        }
    }
}
