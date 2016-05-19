using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
    public class TopicsController : ApiController
    {
        private IChaptersRepository ChaptersRepository { get; set; }
        private ITopicsRepository TopicsRepository { get; set; }
        private IUserRepository UserRepository { get; set; }

        public TopicsController(ITopicsRepository topicsRepository, 
            IChaptersRepository chaptersRepository, IUserRepository userRepository)
        {
            ChaptersRepository = chaptersRepository;
            UserRepository = userRepository;
            TopicsRepository = topicsRepository;
        }

        [Route("chapters/{chapterId:int}/topics")]
        public async Task<IEnumerable<TopicModel>> GetTopics(int chapterId)
        {
            var userId = SecurityHelper.GetUserId(User.Identity);
            var courseId = ChaptersRepository.GetCourseId(chapterId);
            if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId, courseId))
                return TopicsRepository.GetTopics(chapterId);

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("topics/{topicId:int}", Name = "GetTopic")]
        public async Task<IHttpActionResult> GetTopic(int topicId)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = TopicsRepository.GetCourseId(topicId);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId, courseId))
                {
                    var topic = TopicsRepository.GetTopics(topicId);
                    if (topic == null)
                        return NotFound();

                    return Ok(topic);
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }            
        }

        [Route("chapters/{chapterId:int}/topics")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostTopics(int chapterId, [FromBody]TopicModel topic)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = ChaptersRepository.GetCourseId(chapterId);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId, courseId))
                {
                    var id = await TopicsRepository.AddTopic(chapterId, topic);
                    return CreatedAtRoute("GetTopic", new { topicId = id }, topic);
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("topics/{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> DeleteTopic(int id)
        {
            try
            {
                var userId = SecurityHelper.GetUserId(User.Identity);
                var courseId = TopicsRepository.GetCourseId(id);
                if (User.IsInRole("Admin") || await UserRepository.UserHasCourse(userId, courseId))
                {
                    if (!await TopicsRepository.ExistsTopic(id))
                        return NotFound();

                    TopicsRepository.RemoveTopic(id);
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
