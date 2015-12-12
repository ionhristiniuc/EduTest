using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EduTestClient.Services.Utils
{
    public class HttpHelper
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }

        public HttpHelper(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
        }

        public async Task<bool> PostEntity<T>(T entity, string path) where T : class
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var strEntity = new StringContent(Serializer.Serialize(entity));
                var response = await client.PostAsync(path, strEntity);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<T> GetEntity<T>(string path) where T : class
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var result = await client.GetStringAsync(path);
                return Serializer.Deserialize<T>(result);
            }
        }

        public async Task<bool> DeleteEntity(string path)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var result = await client.DeleteAsync(path);
                return result.IsSuccessStatusCode;
            }
        }

        private void PrepareHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        }
    }
}
