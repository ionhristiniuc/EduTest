using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Entities;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services.Base
{
    public class GenericService<T> : IGenericService<T>
        where T : new()
    {
        protected string AccessToken { get; set; }
        protected string ServicePath { get; set; }
        protected ISerializer Serializer { get; set; }

        public GenericService(string accessToken, string servicePath)
        {
            AccessToken = accessToken;
            ServicePath = servicePath;
            Serializer = new JsonSerializer();
        }

        public async Task<T> Get(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var path = $"{ConfigManager.ServiceUrl}{ServicePath}/{id}";
                var result = await client.GetStringAsync(path);
                return Serializer.Deserialize<T>(result);
            }
        }

        public async Task<Items<T>> GetList(int page = 0, int perPage = 10)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var path = $"{ConfigManager.ServiceUrl}{ServicePath}?page={page}&perPage={perPage}";
                var result = await client.GetStringAsync(path);
                return Serializer.Deserialize<Items<T>>(result);
            }
        }

        public async Task<bool> Add(T entity, params KeyValuePair<string, object>[] addParams)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var path = $"{ConfigManager.ServiceUrl}{ServicePath}";

                var pathStr = new StringBuilder(path);
                foreach (var pair in addParams)                
                    pathStr.AppendFormat("?{0}={1}", pair.Key, pair.Value);                

                var response = await client.PostAsJsonAsync(pathStr.ToString(), entity);
                return response.IsSuccessStatusCode;    // TODO should change to return resource id
            }
        }

        public void Update(T entity, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(int id)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var path = $"{ConfigManager.ServiceUrl}{ServicePath}/{id}";
                var result = await client.DeleteAsync(path);
                return result.IsSuccessStatusCode;
            }
        }

        public void SetAuthData(AuthenticationResponse data)
        {
            AccessToken = data.access_token;
        }        

        protected void PrepareHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        }
    }
}
