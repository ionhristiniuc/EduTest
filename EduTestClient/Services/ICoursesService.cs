using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface ICoursesService
    {
        Task<IEnumerable<CourseModel>> GetCourses();
        Task<IEnumerable<CourseModel>> GetCourses(int userId);
    }
}
