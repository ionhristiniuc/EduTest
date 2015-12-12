using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using EduTestContract.Models;
using EduTestService.Core;
using EduTestService.Repositories;
using EduTestService.Security;

namespace EduTestService.Controllers
{
    [Authorize]
    [RoutePrefix("courses")]    
    public class CoursesController : ApiController
    {
        private ICoursesRepository CoursesRepository { get; set; }
        private IUserRepository UserRepository { get; set; }

        public CoursesController(ICoursesRepository repository, IUserRepository userRepository)
        {
            CoursesRepository = repository;
            UserRepository = userRepository;
        }

        [Route("")]
        public async Task<CoursesCollection> GetCourses(int skip = 0, int limit = 10)
        {
            var currentUserId = SecurityHelper.GetUserId(User.Identity);
            if (User.IsInRole("Admin"))
            {
                var courses = await CoursesRepository.GetCourses(skip, limit);
                var total = await CoursesRepository.GetNumberOfCourses();
                return ObjectMapper.MapCollection(courses, total, skip, limit);
            }
            else
            {
                var courses = CoursesRepository.GetCourses(currentUserId.Value, skip, limit);
                var total = await CoursesRepository.GetNumberOfCourses(currentUserId.Value);
                return ObjectMapper.MapCollection(courses, total, skip, limit);
            }
        }
        
        [Route("{id:int}", Name = "GetCourse")]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, id))
                    return Ok(CoursesRepository.GetCourse(id));
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        
        [Route("")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostCourse([FromBody]CourseModel course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var id = await CoursesRepository.AddCourse(course);
                return CreatedAtRoute("GetCourse", new { id = id }, course);
            }
            catch (Exception)
            {
                return InternalServerError();
            } 
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PutCourse(int id, [FromBody] CourseModel course)
        {
            throw new NotImplementedException();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await CoursesRepository.UpdateCourse(id, course);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, id))
                {
                    if (!await CoursesRepository.ExistsCourse(id))
                        return NotFound();

                    CoursesRepository.RemoveCourse(id);
                    return Ok();
                }                
            }
            catch (Exception)
            {
                return InternalServerError();
            }            

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
