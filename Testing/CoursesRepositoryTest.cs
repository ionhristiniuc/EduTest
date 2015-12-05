using System;
using EduTestService.Repository;
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
            var courses = courseRep.GetCoursesByUser(1);
            Assert.IsNotNull(courses);
        }
    }
}
