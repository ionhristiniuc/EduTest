using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using TeacherWebApp.Core.Authentication;

namespace TeacherWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    var formsAuthTiket = FormsAuthentication.Decrypt(
                            Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                    if (formsAuthTiket == null)
                        throw new Exception("Forms Authentication ticket is null");

                    var serializer = new JsonSerializer();
                    var uData = serializer.Deserialize<UserData>(formsAuthTiket.UserData);

                    IUsersService usersRepo =
                            new UsersService(uData.AuthTicket.access_token, serializer);

                    var user = usersRepo.GetUser().Result;

                    HttpContext.Current.User = new CustomPrincipal(user, uData);
                }
            }
        }
    }
}
