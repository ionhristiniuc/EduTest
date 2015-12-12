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
        private const string UsersServicePath = "/users";

        public UsersService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;            
        }

        public async Task<UserModel> GetUser()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                var result = await client.GetStringAsync(ConfigManager.ServiceUrl + UsersServicePath + "/x");
                return Serializer.Deserialize<UserModel>(result);
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                var result = await client.GetStringAsync(ConfigManager.ServiceUrl + UsersServicePath + "/" + id);
                return Serializer.Deserialize<UserModel>(result);
            }
        }
    }
}
