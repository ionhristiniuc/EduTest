using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using EduTestClient.Services.Entities;
using EduTestClient.Services.Utils;

namespace EduTestClient.Services
{
    public class AccountService : IAccountService
    {                
        public AuthenticationResponse AuthResponse { get; private set; }        

        public async Task<bool> Authenticate(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var result = client.PostAsync(ConfigManager.ServiceUrl + ConfigManager.AuthenticationPath, content).Result; // should change to async

                if (!result.IsSuccessStatusCode)                
                    throw new AuthenticationException("An error occurred while authenticating user. Status Code: " + result.StatusCode);
                
                var responseString = await result.Content.ReadAsStringAsync();
                ISerializer serializer = new JsonSerializer();
                AuthResponse = serializer.Deserialize<AuthenticationResponse>(responseString);
                return true;
            }
        }        
    }
}
