using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class UsersService : IUsersService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string ServicePath = "/users";
        public HttpHelper HttpHelper { get; set; }

        public UsersService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;            
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<UserModel> GetUser()
        {
            return await HttpHelper.GetEntity<UserModel>(ConfigManager.ServiceUrl + ServicePath + "/x");    
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await HttpHelper.GetEntity<UserModel>(ConfigManager.ServiceUrl + ServicePath + "/" + id);                        
        }

        public async Task<bool> AddUser(UserModel user)
        {
            var httpHelper = new HttpHelper(AccessToken, Serializer);
            return await httpHelper.PostEntity(user, ConfigManager.ServiceUrl + ServicePath);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id); 
        }
    }
}
