using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> GetUser(string email, string password);
        Task<UserModel> GetUser(int id);
        void AddUser(UserModel userModel);
        void UpdateUser(int id, UserModel userModel);
        void RemoveUser(int id);
        Task<bool> ExistsUser(string email);
    }
}
