using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;
using EduTestService.Utils;

namespace EduTestService.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<UserModel> GetUser(string email, string password)
        {
            using (var dbContext = new EduTestEntities())
            {
                var user = await dbContext.Users
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user == null)
                    return null;

                var userModel = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,                    
                    Roles = user.Roles.Select(r => r.Name).ToArray()
                };
                return userModel;
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var user = await dbContext.Users
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return null;

                var userModel = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,                    
                    Roles = user.Roles.Select(r => r.Name).ToArray()
                };
                return userModel;
            }
        }

        public async void AddUser(UserModel userModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var user = new User()
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email,
                    Password = userModel.Password,
                    Roles = ObjectMapper.MapRoles(userModel.Roles).ToList()
                };

                dbContext.Users.Add(user);

                if (await dbContext.SaveChangesAsync() < 0)
                    throw new Exception("UserRepository.AddUser: Could not add user to db");
            }
        }

        public async void UpdateUser(int id, UserModel userModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("UserRepository.UpdateUser: User not found");

                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Email = userModel.Email;
                user.Roles = ObjectMapper.MapRoles(userModel.Roles).ToList();                

                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("UserRepository.AddUser: Could not add user to db");
            }
        }

        public async void RemoveUser(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("UserRepository.RemoveUser: User not found");

                dbContext.Users.Remove(user);
                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("UserRepository.RemoveUser: Could not remove user from db");
            }
        }

        public async Task<bool> ExistsUser(string email)
        {
            using (var dbContext = new EduTestEntities())
            {
                return await dbContext.Users.AnyAsync(u => u.Email == email);
            }
        }
    }
}