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
        private const string ServicePath = "/courses";
        private HttpHelper HttpHelper { get; set; }

        public CoursesService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<CoursesCollection> GetCourses(int skip, int limit)
        {
            var parameters = string.Format("?skip={0}&limit={1}", skip, limit);
            return await HttpHelper.GetEntity<CoursesCollection>(ConfigManager.ServiceUrl + ServicePath + parameters);
        }

        public async Task<CourseModel> GetCourse(int id)
        {
            var courseModel = await HttpHelper.GetEntity<CourseModel>(ConfigManager.ServiceUrl + ServicePath + "/" + id);
            return courseModel;
        }

        public async Task<bool> AddCourse(CourseModel course)
        {            
            return await HttpHelper.PostEntity(course, ConfigManager.ServiceUrl + ServicePath);
        }        

        public async Task<bool> DeleteCourse(int id)
        {
            return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id);            
        }
    }
}
