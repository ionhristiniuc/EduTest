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
    public class CoursesService : ICoursesService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string CoursesServicePath = "/courses";

        public CoursesService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
        }

        public async Task<CoursesCollection> GetCourses(int skip, int limit)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                var parameters = string.Format("?skip={0}&limit={1}", skip, limit);
                var result = await client.GetStringAsync(ConfigManager.ServiceUrl + CoursesServicePath + parameters);
                return Serializer.Deserialize<CoursesCollection>(result);
            }
        }        
    }
}
