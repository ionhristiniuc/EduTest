using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [RoutePrefix("users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserRepository UserRepository { get; set; }

        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [Route("")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            throw new NotImplementedException();
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            try
            {
                var currentUserId = SecurityHelper.GetUserId(User.Identity);
                var user = await UserRepository.GetUser(id);

                if (user == null)
                    return NotFound();

                if (User.IsInRole("Admin") || id == currentUserId)
                    return Ok(user);

                if (User.IsInRole("Teacher"))
                {                    
                    if (user.Roles.Contains("Student") && await UserRepository.HaveSameCourse(currentUserId.Value, id))
                        return Ok(user);
                }                
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return InternalServerError();
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
               

        [Route("")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostUser([FromBody]UserModel user)
        {            
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var id = await UserRepository.AddUser(user);
                return CreatedAtRoute("users", new {id = id}, user);
            }
            catch (Exception)
            {
                return InternalServerError();
            }            
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PatchUser([FromBody]UserModel user)
        {
            throw new NotImplementedException();
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PutUser(int id, [FromBody]UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await UserRepository.UpdateUser(id, user);
                return Ok();
            }
            catch (Exception e)
        {
                return InternalServerError();
            }
        }
    }
}
