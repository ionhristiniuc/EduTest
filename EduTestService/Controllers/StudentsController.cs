using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestService.Core;
using EduTestService.Repositories;
using EduTestService.Security;
using log4net;

namespace EduTestService.Controllers
{
    [Authorize(Roles = "Teacher,Admin")]
    [RoutePrefix("students")]
    public class StudentsController : ApiController
    {
        private IUserRepository UserRepository { get; set; }
        private IStudentsRepository StudentsRepository { get; set; }
        private static readonly ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public StudentsController(IUserRepository userRepository, IStudentsRepository studentsRepository)
        {
            UserRepository = userRepository;
            StudentsRepository = studentsRepository;
        }
                
        [Route("")]
        public async Task<IHttpActionResult> GetStudents(int page = 0, int perPage = 10)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {                                     
                    var students = await StudentsRepository.GetStudents(page, perPage);
                    var count = await StudentsRepository.GetTotalCount();
                    var result = ObjectMapper.ToItems(students, page, perPage, count);
                    return Ok(result);
                }
                else if (User.IsInRole("Teacher"))
                {
                    var userId = SecurityHelper.GetUserId(User.Identity);
                    var students = await StudentsRepository.GetStudents4Teacher(userId, page, perPage);
                    var count = await StudentsRepository.GetTotalCount4Teacher(userId);

                    var result = ObjectMapper.ToItems(students, page, perPage, count);
                    return Ok(result);
                }
                else                
                    throw new Exception("Unexpected role for caller");                
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return InternalServerError();
            }
        }

        [Route("{id:int}", Name = "GetStudent")]
        public async Task<IHttpActionResult> GetStudent(int id)
        {
            try
            {
                var student = await StudentsRepository.GetStudent(id);

                if (student == null)
                    return NotFound();

                return Ok(student);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return InternalServerError();
            }
        }
    }
}
