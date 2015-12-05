using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using StudentEduTest.Services.Entities;

namespace EduTestClient.Services
{
    class AccountService : IAccountService
    {        
        private static string BaseUrl { get; set; }
        private static string AuthPath { get; set; }

        static AccountService()
        {
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            AuthPath = ConfigurationManager.AppSettings["AuthPath"];
        }

        public async Task<string> Authenticate(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var result = await client.PostAsync(BaseUrl + AuthPath, content);

                if (!result.IsSuccessStatusCode)                
                    throw new AuthenticationException("An error occurred while authenticating user. Status Code: " + result.StatusCode);
                
                var responseString = await result.Content.ReadAsStringAsync();
                ISerializer serializer = new JsonSerializer();
                var resp = serializer.Deserialize<AuthenticationResponse>(responseString);
                return resp.access_token;
            }
        }
    }
}
