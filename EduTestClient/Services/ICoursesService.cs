using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface ICoursesService
    {
        Task<CoursesCollection> GetCourses(int skip = 0, int limit = 20);        
        Task<CourseModel> GetCourse(int id);
        Task<bool> AddCourse(CourseModel course);
        Task<bool> DeleteCourse(int id);
    }
}
