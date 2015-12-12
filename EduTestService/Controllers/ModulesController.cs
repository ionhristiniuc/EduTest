using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestContract.Models;
using EduTestService.Repositories;
using EduTestService.Security;

namespace EduTestService.Controllers
{
    [Authorize]
    public class ModulesController : ApiController
    {
        private IModulesRepository ModulesRepository { get; set; }
        private IUserRepository UserRepository { get; set; }

        public ModulesController(IModulesRepository modulesRepository, IUserRepository userRepository)
        {
            ModulesRepository = modulesRepository;
            UserRepository = userRepository;
        }

        [Route("courses/{courseId:int}/modules")]
        public async Task<IEnumerable<ModuleModel>> GetModules(int courseId)
        {            
            var userId = SecurityHelper.GetUserId(User.Identity);
            if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                return ModulesRepository.GetModules(courseId);                
           
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("modules/{moduleId:int}", Name = "GetModule")]
        public async Task<IHttpActionResult> GetModule(int moduleId)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = ModulesRepository.GetCourseId(moduleId);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                {
                    var module = ModulesRepository.GetModule(moduleId);
                    if (module == null)
                        return NotFound();

                    return Ok(module);
                }                                                    
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("courses/{courseId:int}/modules")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostModule(int courseId, [FromBody]ModuleModel module)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var id = await ModulesRepository.AddModule(courseId, module);
                return CreatedAtRoute("GetModule", new { courseId = courseId, moduleId = id }, module);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
