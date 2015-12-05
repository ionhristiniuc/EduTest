using System.Collections.Generic;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface ICoursesService
    {
        IEnumerable<CourseModel> GetCourses();
    }
}
