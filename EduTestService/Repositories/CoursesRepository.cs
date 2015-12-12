using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using EduTestContract.Models;
using EduTestData.Model;
using EduTestService.Core;

namespace EduTestService.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        public IEnumerable<CourseModel> GetCourses(int userId, int skip, int limit)
        {
            using (var dbContext = new EduTestEntities())
            {
                var courses = dbContext.Users
                    .Include(u => u.Courses
                        .Select(c => c.Modules
                            .Select(m => m.Chapters
                                .Select(ch => ch.Topics))))
                    .First(usr => usr.Id == userId)
                    .Courses
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

                return courses.Select(ObjectMapper.MapCourse);
            }
        }

        public async Task<IEnumerable<CourseModel>> GetCourses(int skip, int limit)
        {
            using (var dbContext = new EduTestEntities())
            {
                var courses = await dbContext.Courses
                    .Include(c => c.Modules
                        .Select(m => m.Chapters
                            .Select(ch => ch.Topics)))
                    .Skip(skip)
                    .Take(limit)
                    .ToListAsync();

                return courses.Select(ObjectMapper.MapCourse);
            }
        }

        public async Task<CourseModel> GetCourse(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var course = await dbContext.Courses
                    .Include(c => c.Modules
                        .Select(m => m.Chapters
                            .Select(ch => ch.Topics)))
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                    throw new ObjectNotFoundException("Course with id " + id + " was not found in the database");

                return ObjectMapper.MapCourse(course);
            }
        }

        public async Task<int> AddCourse(CourseModel courseModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var course = new Course()
                {
                    Name = courseModel.Name,
                    Modules = courseModel.Modules.Select(m => new Module()
                    {
                       Name = m.Name,
                       Chapters = m.Chapters.Select(c => new Chapter()
                       {
                           Name = c.Name,
                           Topics = c.Topics.Select(t => new Topic()
                           {
                               Name = t.Name                               
                           }).ToList()
                       }).ToList()
                    }).ToList()
                };

                dbContext.Courses.Add(course);

                if (await dbContext.SaveChangesAsync() < 0)
                    throw new Exception("CoursesRepository.AddCourse: Could not add course to db");

                return course.Id;
            }
        }

        public async Task UpdateCourse(int id, CourseModel courseModel)
        {
            throw new NotImplementedException();
            /*using (var dbContext = new EduTestEntities())
            {
                var course = await dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);                               

                var course = new Course()
                {
                    Name = courseModel.Name,
                    Modules = courseModel.Modules.Select(m => new Module()
                    {
                        Name = m.Name,
                        Chapters = m.Chapters.Select(c => new Chapter()
                        {
                            Name = c.Name,
                            Topics = c.Topics.Select(t => new Topic()
                            {
                                Name = t.Name
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };                

                if (await dbContext.SaveChangesAsync() < 0)
                    throw new Exception("CoursesRepository.UpdateCourse: Could not update course");
            }*/
        }

        public async void RemoveCourse(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var course = dbContext.Courses
                    .Include(c => c.Modules
                        .Select(m => m.Chapters
                            .Select(ch => ch.Topics)))
                    .First(crs => crs.Id == id);                

                foreach (var module in course.Modules)
                {
                    foreach (var chapter in module.Chapters)                   
                        dbContext.Topics.RemoveRange(chapter.Topics);
                    
                    dbContext.Chapters.RemoveRange(module.Chapters);
                }
                dbContext.Modules.RemoveRange(course.Modules);
                dbContext.Courses.Remove(course);                

                if (await dbContext.SaveChangesAsync() < 0)
                    throw new Exception("CoursesRepository.RemoveCourse: Could not remove course from db");
            }
        }

        public Task<int> GetNumberOfCourses()
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Courses.CountAsync();
            }
        }

        public Task<int> GetNumberOfCourses(int userId)
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Courses.CountAsync(c => c.Users.Any(u => u.Id == userId));
            }
        }

        public Task<bool> ExistsCourse(int courseId)
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Courses.AnyAsync(c => c.Id == courseId);
            } 
        }
    }
}