using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EduTestClient.Services;
using EduTestContract.Models;
using TeacherWebApp.Core.Authentication;
using TeacherWebApp.ViewModels;

namespace TeacherWebApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly ICoursesService _coursesService;

        public StudentsController(IStudentsService studentsService, ICoursesService coursesService)
        {
            _studentsService = studentsService;
            _coursesService = coursesService;            
        }

        public async Task<ActionResult> Index()
        {
            _studentsService.SetAuthData(AuthHelper.GetTokens(User));
            var students = await _studentsService.GetList(0, 10);
            return View(students);
        }

        public async Task<ActionResult> AddStudent()
        {            
            _coursesService.SetAuthData(AuthHelper.GetTokens(User));
            var courses = await _coursesService.GetList();
            ViewBag.Courses = courses.Data.ToDictionary(c => c.Id, c => c.Name);            

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentViewModel s)
        {
            if (!ModelState.IsValid)
                return View(s);

            try
            {
                var student = new StudentModel()
                {
                    User = new UserModel()
                    {
                        Username = s.Username,
                        PersonalDetail = new PersonalDetailModel()
                        {
                            Email = s.Email,
                            FirstName = s.FirstName,
                            LastName = s.LastName
                        }
                    }
                };

                _studentsService.SetAuthData(AuthHelper.GetTokens(User));

                if (await _studentsService.Add(student))
                {
                    TempData["studentAdded"] = "Student registration successfully created";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Failed to add student";
                    return RedirectToAction("AddStudent");
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                TempData["error"] = "An error occurred while adding new student";
                return RedirectToAction("AddStudent");
            }                        
        }
    }
}