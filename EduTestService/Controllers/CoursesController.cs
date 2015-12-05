using System;
using System.Collections.Generic;
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
using EduTestService.Repository;
using EduTestService.Security;

namespace EduTestService.Controllers
{
    [Authorize]
    [RoutePrefix("courses")]    
    public class CoursesController : ApiController
    {
        private ICoursesRepository CoursesRepository { get; set; }

        public CoursesController(ICoursesRepository repository)
        {
            CoursesRepository = repository;
        }
        
        [Route("")]
        public IEnumerable<CourseModel> GetCourses()
        {
            var id = SecurityHelper.GetUserId(User.Identity);
            return CoursesRepository.GetCoursesByUser(id.Value);
        }

        [Route("{id:int}")]
        [Authorize]
        public IEnumerable<CourseModel> GetCourses(int id)
        {            
            return CoursesRepository.GetCoursesByUser(id);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
