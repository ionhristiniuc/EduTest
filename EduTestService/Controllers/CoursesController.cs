using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using EduTestService.Security;

namespace EduTestService.Controllers
{
    [Authorize]
    public class CoursesController : ApiController
    {
        // GET api/values        
        public IEnumerable<string> GetCourse()
        {            
            int? id = SecurityHelper.GetUserId(User.Identity);            
            return new string[] { "value1", "value2" };
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
