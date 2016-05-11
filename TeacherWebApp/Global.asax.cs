using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
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
            if (Request.IsAuthenticated)
            {
                if (FormsAuthentication.CookiesSupported)
                {
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            var formsAuthTiket = FormsAuthentication.Decrypt(
                                Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                            if (formsAuthTiket == null)
                            {
                                FormsAuthentication.SignOut();
                                return;
                                //throw new Exception("Forms Authentication ticket is null");
                            }


                            var serializer = new JsonSerializer();
                            var uData = serializer.Deserialize<UserData>(formsAuthTiket.UserData);

                            IUsersService usersRepo =
                                    new UsersService(uData.AuthTicket.access_token, serializer);
                            
                            var user = GetCurrentUser(usersRepo);
                            //var user = new UserModel()
                            //{
                            //    Username = "ionh",
                            //    Email = "ionhristiniuc@yahoo.com",
                            //    Roles = new string[] { "Admin" }
                            //};

                            if (user == null)
                            {
                                FormsAuthentication.SignOut();
                                return;
                            }

                            var principal = new CustomPrincipal(user, uData);
                            HttpContext.Current.User = principal;
                            Thread.CurrentPrincipal = principal;
                        }
                        catch (Exception)
                        {
                            FormsAuthentication.SignOut();
                        }
                    }
                }
            }
        }

        private static UserModel GetCurrentUser(IUsersService usersRepo)
        {            
            var task = Task.Run(async () => await usersRepo.GetUser());
            return task.Result;
        }
    }
}
