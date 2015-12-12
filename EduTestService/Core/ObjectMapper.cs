using System.Collections.Generic;
using System.Linq;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Core
{
    public class ObjectMapper
    {
        public static IEnumerable<Role> MapRoles(string[] roleNames)
        {
            using (var dbContext = new EduTestEntities())
            {
                var allRoles = dbContext.Roles.ToList();

                foreach (var role in allRoles)
                {
                    if (roleNames.Contains(role.Name))
                        yield return role;
                }
            }
        }

        public static CourseModel MapCourse(Course c)
        {
            return new CourseModel()
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
            };
        }

        public static CoursesCollection MapCollection(IEnumerable<CourseModel> courses, int total, int skip, int limit)
        {
            return new CoursesCollection()
            {
                Courses = courses,
                Pagination = new Pagination()
                {
                    Total = total,
                    Returned = courses.Count(),
                    Offset = skip,
                    Limit = limit
                }
            };
        }
    }
}