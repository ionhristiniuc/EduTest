using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Repository
{
    public interface ICoursesRepository
    {
        IEnumerable<CourseModel> GetCoursesByUser(int teacherId);
    }
}
