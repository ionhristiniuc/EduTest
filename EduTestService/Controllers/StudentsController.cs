using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestService.Repositories;
using log4net;

namespace EduTestService.Controllers
{
    [Authorize]
    [RoutePrefix("students")]
    public class StudentsController : ApiController
    {
        private IUserRepository UserRepository { get; set; }
        private static readonly ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public StudentsController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [Route("")]
        [Authorize(Roles = "Teacher,Admin")]
        [Route("")]
        public async Task<IHttpActionResult> GetStudents(int page = 0, int perPage = 10)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
