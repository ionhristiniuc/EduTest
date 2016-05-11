using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Abstract;
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

        public Items<T> GetList(int page = 0, int perPage = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(T entity)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);
                var path = $"{ConfigManager.ServiceUrl}{ServicePath}";
                var response = await client.PostAsJsonAsync(path, entity);
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

        protected void PrepareHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        }
    }
}
