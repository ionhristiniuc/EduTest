using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestContract.Models;
using EduTestService.Repository;
using EduTestService.Security;

namespace EduTestService.Controllers
{
    [RoutePrefix("users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserRepository UserRepository { get; set; }

        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<UserModel> GetUser(int id)
        {            
            return await UserRepository.GetUser(id);                     
        }

        // GET: api/User/5
        public async Task<UserModel> GetUser()
        {
            var id = SecurityHelper.GetUserId(User.Identity);
            return await UserRepository.GetUser(id.Value); 
        }
    }
}
