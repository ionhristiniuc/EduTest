using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EduTestClient.Services;
using EduTestContract.Models;
using TeacherWebApp.Core.Authentication;

namespace TeacherWebApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        // GET: Courses
        public async Task<ViewResult> Index()
        {            
            _coursesService.SetAuthData(AuthHelper.GetTokens(User));
            var courses = await _coursesService.GetList(0, 10);

            if (courses.Data == null)            
                courses.Data = new List<CourseModel>();            

            return View(courses);
        }
    }
}