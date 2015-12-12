using System;
using EduTestService.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class CoursesRepositoryTest
    {
        [TestMethod]
        public void GetCourseByTeacherTest()
        {
            var courseRep = new CoursesRepository();
            var courses = courseRep.GetCourses(1, 0, 10);
            Assert.IsNotNull(courses);
        }
    }
}
