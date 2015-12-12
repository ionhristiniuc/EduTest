using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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

                return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }            
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
                return CreatedAtRoute("GetModule", new { moduleId = id }, module);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("modules/{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> DeleteModule(int id)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = ModulesRepository.GetCourseId(id);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                {
                    if (!await ModulesRepository.ExistsModule(id))
                        return NotFound();

                    ModulesRepository.RemoveModule(id);
                    return StatusCode(HttpStatusCode.NoContent);
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }            
        }
    }
}
