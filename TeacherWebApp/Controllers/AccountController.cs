using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EduTestClient.Services;
using TeacherApp.ViewModels;
using TeacherWebApp.Core.Authentication;

namespace TeacherWebApp.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult LogOn()
        {
            var viewModel = new LogOnViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var resp = await _accountService.Authenticate(model.Login.Trim(), model.Password.Trim());

                if (!string.IsNullOrEmpty(resp?.access_token))
                {
                    var userData = new UserData() { AuthTicket = resp };
                    Response.SetAuthCookie(model.Login, false, userData);
                    //FormsAuthentication.SetAuthCookie(model.Login, false);
                    //var json = JsonConvert.SerializeObject(resp);
                    //var userCookie = new HttpCookie("token", json);
                    //userCookie.Expires.AddDays(2);
                    //HttpContext.Response.SetCookie(userCookie);
                    //HttpContext.User = new CustomPrincipal(null, resp);

                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            model.Password = string.Empty;
            return View(model);
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}