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
    [Authorize(Roles = "Admin,Teacher")]
    [RoutePrefix("questions")]
    public class QuestionsController : BaseApiController
    {        
        [Route("")]
        public async Task<IHttpActionResult> PostQuestion([FromBody]QuestionBaseModel question)
        {
            try
            {
                if (question is VariantQuestionModel)
                {
                    Logger.DebugFormat("Question of type {0} posted!", question.Type);
                }

                return Ok();

                //return Unauthorized();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("{id:int}", Name = "GetQuestion")]
        public async Task<IHttpActionResult> GetQuestion(int id)
        {
            try
            {
               

                return Unauthorized();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
