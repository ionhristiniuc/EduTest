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
    public class ChaptersController : ApiController
    {
       private IChaptersRepository ModulesRepository { get; set; }
        private IUserRepository UserRepository { get; set; }

        public ChaptersController(IChaptersRepository modulesRepository, IUserRepository userRepository)
        {
            ModulesRepository = modulesRepository;
            UserRepository = userRepository;
        }

    //    [Route("modules/{moduleId:int}/chapters")]
    //    public async Task<IEnumerable<ChapterModel>> GetChapters(int moduleId)
    //    {            
    //        var userId = SecurityHelper.GetUserId(User.Identity);
    //        if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
    //            return ModulesRepository.GetChapters(courseId);                
           
    //        throw new HttpResponseException(HttpStatusCode.Unauthorized);
    //    }

    //    [Route("courses/{courseId:int}/modules/{moduleId:int}", Name = "GetModule")]
    //    public async Task<IHttpActionResult> GetModule(int courseId, int moduleId)
    //    {
    //        try
    //        {
    //            var userId = SecurityHelper.GetUserId(User.Identity);
    //            if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
    //            {
    //                var module = ModulesRepository.GetModule(moduleId);
    //                if (module == null)
    //                    return NotFound();

    //                return Ok(module);
    //            }                                                    
    //        }
    //        catch (Exception)
    //        {
    //            return InternalServerError();
    //        }

    //        throw new HttpResponseException(HttpStatusCode.Unauthorized);
    //    }

    //    [Route("courses/{courseId:int}/modules")]
    //    [Authorize(Roles = "Teacher,Admin")]
    //    public async Task<IHttpActionResult> PostModule(int courseId, [FromBody]ModuleModel module)
    //    {
    //        try
    //        {
    //            if (!ModelState.IsValid)
    //                return BadRequest();

    //            var id = await ModulesRepository.AddModule(courseId, module);
    //            return CreatedAtRoute("GetModule", new { courseId = courseId, moduleId = id }, module);
    //        }
    //        catch (Exception)
    //        {
    //            return InternalServerError();
    //        }
    //    }
    }
}
