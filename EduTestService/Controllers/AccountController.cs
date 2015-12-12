using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EduTestContract.Models;
using EduTestService.Repositories;

namespace EduTestService.Controllers
{   
    [Authorize]
    [RoutePrefix("account")]
    public class AccountController : ApiController
    {
        public IUserRepository UserRepo { get; set; }

        public AccountController(IUserRepository userRepository)
        {
            UserRepo = userRepository;
        }

        [Authorize(Roles = "Teacher,Admin")]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            try
            {
                if (await UserRepo.ExistsUser(userModel.Email))
                {
                    ModelState.AddModelError("Email", "A user with such an email already exists");
                    return BadRequest(ModelState);
                }
                                    
                UserRepo.AddUser(userModel);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }        
    }
}
