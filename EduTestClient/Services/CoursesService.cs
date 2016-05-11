using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Abstract;
using EduTestClient.Services.Base;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class CoursesService : GenericService<CourseModel>, ICoursesService
    {        
        private const string ServicePath = "/courses";        

        public CoursesService(string accessToken)
            : base(accessToken, ServicePath)
        {
            
        }

        //public async Task<CoursesCollection> GetCourses(int skip, int limit)
        //{
        //    var parameters = string.Format("?skip={0}&limit={1}", skip, limit);
        //    return await HttpHelper.GetEntity<CoursesCollection>(ConfigManager.ServiceUrl + ServicePath + parameters);
        //}

        //public async Task<CourseModel> GetCourse(int id)
        //{
        //    var courseModel = await HttpHelper.GetEntity<CourseModel>(ConfigManager.ServiceUrl + ServicePath + "/" + id);
        //    return courseModel;
        //}

        //public async Task<bool> AddCourse(CourseModel course)
        //{            
        //    return await HttpHelper.PostEntity(course, ConfigManager.ServiceUrl + ServicePath);
        //}        

        //public async Task<bool> DeleteCourse(int id)
        //{
        //    return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id);            
        //}
    }
}
