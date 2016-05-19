using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<CourseModel>> GetCourses(int userId, int page, int perPage);
        Task<IEnumerable<CourseModel>> GetCourses(int page, int perPage);
        Task<CourseModel> GetCourse(int id);
        Task<int> AddCourse(CourseModel course);
        Task<int> AddDeepCourse(CourseModel course);
        Task UpdateCourse(int id, CourseModel courseModel);
        void RemoveCourse(int id);
        void RemoveDeepCourse(int id);
        Task<int> GetNumberOfCourses();
        Task<int> GetNumberOfCourses(int userId);
        Task<bool> ExistsCourse(int courseId);
    }
}
