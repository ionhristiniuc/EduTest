using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestContract.Models;
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
        private readonly IUserRepository _userRepository;
        private readonly IStudentsRepository _studentsRepository;
        private static readonly ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public StudentsController(IUserRepository userRepository, IStudentsRepository studentsRepository)
        {
            _userRepository = userRepository;
            _studentsRepository = studentsRepository;
        }
                
        [Route("")]
        public async Task<IHttpActionResult> GetStudents(int page = 0, int perPage = 10)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {                                     
                    var students = await _studentsRepository.GetStudents(page, perPage);
                    var count = await _studentsRepository.GetTotalCount();
                    var result = ObjectMapper.ToItems(students, page, perPage, count);
                    return Ok(result);
                }
                else if (User.IsInRole("Teacher"))
                {
                    var userId = SecurityHelper.GetUserId(User.Identity);
                    var students = await _studentsRepository.GetStudents4Teacher(userId, page, perPage);
                    var count = await _studentsRepository.GetTotalCount4Teacher(userId);

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
                var student = await _studentsRepository.GetStudent(id);

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

        [Route("")]
        public async Task<IHttpActionResult> PostStudent([FromBody] StudentModel student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (await _userRepository.ExistsUser(student.User.PersonalDetail.Email))
                    return Conflict();

                student.User.Roles = new[] {"Student"};

                var id = await _studentsRepository.AddStudent(student);
                return CreatedAtRoute("GetStudent", new { id = id }, student);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
