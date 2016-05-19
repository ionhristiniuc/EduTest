using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EduTestClient.Services;
using TeacherWebApp.Core.Authentication;

namespace TeacherWebApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }        

        // GET: Students
        public async Task<ActionResult> Index()
        {
            _studentsService.SetAuthData(AuthHelper.GetTokens(User));
            var students = await _studentsService.GetList(0, 10);
            return View(students);
        }
    }
}