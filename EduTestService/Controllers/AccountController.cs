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
        private IUserRepository UserRepository { get; set; }

        public AccountController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }       
    }
}
