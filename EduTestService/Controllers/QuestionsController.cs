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
    [Authorize(Roles = "Admin,Teacher,Student")]
    [RoutePrefix("questions")]
    public class QuestionsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        [Authorize(Roles = "Teacher,Admin")]
        [Route("")]
        public async Task<IHttpActionResult> PostQuestion([FromBody]QuestionBaseModel question)
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
