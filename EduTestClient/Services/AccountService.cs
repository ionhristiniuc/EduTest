using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using EduTestClient.Services.Utils;
using StudentEduTest.Services.Entities;

namespace EduTestClient.Services
{
    class AccountService : IAccountService
    {
        public static AuthenticationResponse AuthResponse { get; set; }
        private const string BaseUrl = "http://localhost:35521";
        private const string AuthPath = "/token";

        public bool Authenticate(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var result = client.PostAsync(BaseUrl + AuthPath, content).Result;

                if (!result.IsSuccessStatusCode)                
                    throw new AuthenticationException("An error occurred while authenticating user. Status Code: " + result.StatusCode);
                
                var responseString = result.Content.ReadAsStringAsync().Result;
                ISerializer serializer = new JsonSerializer();
                AuthResponse = serializer.Deserialize<AuthenticationResponse>(responseString);

                return true;
            }
        }
    }
}
