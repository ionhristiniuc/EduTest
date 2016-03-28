using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestContract.Models;
using EduTestService.Security;
using log4net;

namespace EduTestService.Controllers
{
    public class QuestionsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Route("topics/{topicId:int}/questions")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostQuestion(int topicId, [FromBody]QuestionBaseModel question)
        {
            try
            {
                if (question is VariantQuestionModel)
                    Log.DebugFormat("Question of type {0} posted!", question.Type);

                return Ok();

                //return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("questions/{questionId:int}", Name = "GetQuestion")]
        public async Task<IHttpActionResult> GetQuestion(int questionId)
        {
            try
            {
               

                return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
