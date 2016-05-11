using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EduTestClient.Services;
using EduTestClient.Services.Entities;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
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

        public ActionResult LogOn(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
                ViewBag.ReturnUrl = ReturnUrl;
            var viewModel = new LogOnViewModel();            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOn(LogOnViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resp = await _accountService.Authenticate(model.Login.Trim(), model.Password.Trim());
                    
                    if (!string.IsNullOrEmpty(resp?.access_token))
                    {
                        var userData = new UserData() {AuthTicket = resp};
                        Response.SetAuthCookie(model.Login, false, userData);                                
                        
                        var usersService = new UsersService(resp.access_token, new JsonSerializer());
                        await usersService.AddUser(new UserModel()
                        {
                            Username = "user123",
                            Roles = new []{"Student"},
                            PersonalDetail = new PersonalDetailModel()
                            {
                                Email = "stud1",
                                FirstName = "StudentFN",
                                LastName = "StudentLN"
                            }
                        });                                                                                                      

                        return RedirectToLocal(returnUrl);
                    }
                    else                    
                        ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");                                            
                }
                else                
                    ModelState.AddModelError(string.Empty, "Invalid input");                

                model.Password = string.Empty;
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.ToString());
                model.Password = string.Empty;
                return View(model);
            }                       
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))            
                return Redirect(returnUrl);            
            else            
                return RedirectToAction("Index", "Home");            
        }
    }
}