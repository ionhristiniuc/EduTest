using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using EduTestContract.Models;
using EduTestService.Repositories;
using EduTestService.Security;
using log4net;

namespace EduTestService.Controllers
{
    [RoutePrefix("users")]
    [Authorize]
    public class UsersController : BaseApiController
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

        [Route("current")]
        [Authorize(Roles = "Teacher,Admin,Student")]
        public async Task<IHttpActionResult> GetUser()
        {
            Logger.DebugFormat("Get current user called");

            try
            {
                var currentUserId = SecurityHelper.GetUserId(User.Identity);
                var user = await UserRepository.GetUser(currentUserId);
                
                return Ok(user);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                Logger.Error("An exception occurred", e);
                return InternalServerError();
            }            
        }

        [Route("{id:int}", Name = "GetUser")]
        [Authorize(Roles = "Teacher,Admin,Student")]
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
                    if (user.Roles.Contains("Student") && await UserRepository.HaveSameCourse(currentUserId, id))
                        return Ok(user);
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return InternalServerError();
            }            
        }


        [Route("")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IHttpActionResult> PostUser([FromBody]UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (await UserRepository.ExistsUser(user.PersonalDetail.Email))
                {
                    ModelState.AddModelError("Email", "A user with such an email already exists");
                    return BadRequest(ModelState);
                }

                var id = await UserRepository.AddUser(user);
                return CreatedAtRoute("GetUser", new { id = id }, user);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
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
