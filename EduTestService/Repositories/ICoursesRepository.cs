using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface ICoursesRepository
    {
        IEnumerable<CourseModel> GetCourses(int userId, int skip, int limit);
        Task<IEnumerable<CourseModel>> GetCourses(int skip, int limit);
        Task<CourseModel> GetCourse(int id);
        Task<int> AddCourse(CourseModel course);
        Task UpdateCourse(int id, CourseModel courseModel);
        void RemoveCourse(int id);
        Task<int> GetNumberOfCourses();
        Task<int> GetNumberOfCourses(int userId);
        Task<bool> ExistsCourse(int courseId);
    }
}
