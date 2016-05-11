using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
using log4net;

namespace EduTestService.Controllers
{
    [Authorize]
    [RoutePrefix("courses")]    
    public class CoursesController : ApiController
    {
        private ICoursesRepository CoursesRepository { get; set; }
        private IUserRepository UserRepository { get; set; }
        private static readonly ILog Log = 
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CoursesController(ICoursesRepository repository, IUserRepository userRepository)
        {
            CoursesRepository = repository;
            UserRepository = userRepository;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetCourses(int skip = 0, int limit = 10)
        {
            Log.DebugFormat("GetCourses called");

            try
            {
                var currentUserId = SecurityHelper.GetUserId(User.Identity);
                if (User.IsInRole("Admin"))
                {                    
                    var courses = await CoursesRepository.GetCourses(skip, limit);
                    var total = await CoursesRepository.GetNumberOfCourses();
                    return Ok(ObjectMapper.MapCollection(courses, total, skip, limit));
                }
                else
                {
                    var courses = CoursesRepository.GetCourses(currentUserId.Value, skip, limit);
                    var total = await CoursesRepository.GetNumberOfCourses(currentUserId.Value);
                    return Ok(ObjectMapper.MapCollection(courses, total, skip, limit));
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return InternalServerError();
            }            
        }
        
        [Route("{id:int}", Name = "GetCourse")]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, id))
                {
                    var content = await CoursesRepository.GetCourse(id);
                    return Ok(content);
                }
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

                    CoursesRepository.RemoveDeepCourse(id);
                    return StatusCode(HttpStatusCode.NoContent);
                }

                return Unauthorized();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }                        
        }
    }
}
