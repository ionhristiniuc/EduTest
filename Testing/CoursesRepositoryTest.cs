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
            ICoursesRepository courseRep = new CoursesRepository();
            var courses = courseRep.GetCourses(1, 0, 10);
            Assert.IsNotNull(courses);
        }

        [TestMethod]
        public void GetCoursesTest()
        {
            ICoursesRepository courseRep = new CoursesRepository();
            var coursesCollection = courseRep.GetCourses(0, 10).Result;
            Assert.IsNotNull(coursesCollection);
        }
    }
}
