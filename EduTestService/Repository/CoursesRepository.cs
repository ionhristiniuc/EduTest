using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Repository
{
    public class CoursesRepository : ICoursesRepository
    {
        public IEnumerable<CourseModel> GetCoursesByUser(int teacherId)
        {
            using (var dbContext = new EduTestEntities())
            {
                var courses = dbContext.Users
                    .Include(u => u.Courses
                        .Select(c => c.Modules
                            .Select(m => m.Chapters
                                .Select(ch => ch.Topics))))
                    .First(usr => usr.Id == teacherId)
                    .Courses.ToList();

                return courses.Select(c => new CourseModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Modules = c.Modules.Select(m => new ModuleModel()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Chapters = m.Chapters.Select(ch => new ChapterModel()
                        {
                            Id = ch.Id,
                            Name = ch.Name,
                            Topics = ch.Topics.Select(t => new TopicModel()
                            {
                                Id = t.Id,
                                Name = t.Name                                
                            })
                        })
                    })
                });
            }
        }
    }
}