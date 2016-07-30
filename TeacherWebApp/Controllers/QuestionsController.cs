using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduTestClient.Services;
using TeacherWebApp.Core.Authentication;

namespace TeacherWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        // GET: Questions
        public ActionResult Index()
        {
            _questionsService.SetAuthData(AuthHelper.GetTokens(User));
            var students = await _questionsService.GetList(page, perPage);
            return System.Web.UI.WebControls.View(students);
        }

        public ActionResult AddQuestion()
        {
            return View();
        }
    }
}