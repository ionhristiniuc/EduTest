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

        public async Task<IEnumerable<CourseModel>> GetCourses()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                var result = await client.GetStringAsync(ConfigManager.ServiceUrl + CoursesServicePath);
                return Serializer.Deserialize<IEnumerable<CourseModel>>(result);
            }
        }

        public async Task<IEnumerable<CourseModel>> GetCourses(int userId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                var result = await client.GetStringAsync(ConfigManager.ServiceUrl + CoursesServicePath + "/" + userId);
                return Serializer.Deserialize<IEnumerable<CourseModel>>(result);
            }
        }
    }
}
