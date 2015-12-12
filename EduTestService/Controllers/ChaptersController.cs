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
        private IModulesRepository ModulesRepository { get; set; }
        private IChaptersRepository ChaptersRepository { get; set; }        
        private IUserRepository UserRepository { get; set; }        

        public ChaptersController(IModulesRepository modulesRepository, 
            IChaptersRepository chaptersRepository, IUserRepository userRepository)
        {
            ChaptersRepository = chaptersRepository;
            UserRepository = userRepository;
            ModulesRepository = modulesRepository;
        }

        [Route("modules/{moduleId:int}/chapters")]
        public async Task<IEnumerable<ChapterModel>> GetChapters(int moduleId)
        {
            var userId = SecurityHelper.GetUserId(User.Identity);
            var courseId = ModulesRepository.GetCourseId(moduleId);
            if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                return ChaptersRepository.GetChapters(courseId);

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("chapters/{chapterId:int}", Name = "GetChapter")]
        public async Task<IHttpActionResult> GetChapter(int chapterId)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = ChaptersRepository.GetCourseId(chapterId);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                {
                    var chapter = ChaptersRepository.GetChapter(chapterId);
                    if (chapter == null)
                        return NotFound();

                    return Ok(chapter);
                }                                                    
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("modules/{moduleId:int}/chapters")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostChapter(int moduleId, [FromBody]ChapterModel chapter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = ModulesRepository.GetCourseId(moduleId);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId.Value, courseId))
                {
                    var id = await ChaptersRepository.AddChapter(moduleId, chapter);
                    return CreatedAtRoute("GetChapter", new {chapterId = id}, chapter);
                }
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            catch (Exception)
            {
                return InternalServerError();
            }            
        }
    }
}
