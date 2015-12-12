using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface IUsersService
    {
        Task<UserModel> GetUser();
        Task<UserModel> GetUser(int id);
        Task<bool> AddUser(UserModel user);
        Task<bool> DeleteUser(int id);
    }
}
