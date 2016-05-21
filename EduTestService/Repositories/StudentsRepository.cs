using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Threading.Tasks;

namespace EduTestService.Repositories
{
    public class StudentsRepository : BaseRepository, IStudentsRepository
    {
        public async Task<IEnumerable<StudentModel>> GetStudents(int page, int perPage)
        {
            using (var dbModel = new EduTestEntities())
            {
                var students = await dbModel.Students
                    .Include(s => s.User)
                    .Include(s => s.User.PersonalDetail)
                    .OrderBy(s => s.UserId)
                    .Skip(page * perPage)
                    .Take(perPage)
                    .ToListAsync();

                return base.Mapper.Map<IEnumerable<StudentModel>>(students);
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudents4Teacher(int teacherId, int page, int perPage)
        {
            using (var dbModel = new EduTestEntities())
            {
                var students = await dbModel.Students
                    .Include(s => s.User)
                    .Include(s => s.User.PersonalDetail)
                    .Where(s => s.User.Courses.Any(c => c.Users.Any(u => u.Id == teacherId)))
                    .OrderBy(s => s.UserId)
                    .Skip(page * perPage)
                    .Take(perPage)
                    .ToListAsync();

                return base.Mapper.Map<IEnumerable<StudentModel>>(students);
            }
        }

        public Task<int> GetTotalCount()
        {
            using (var dbModel = new EduTestEntities())
            {
                return dbModel.Students.CountAsync();
            }
        }

        public async Task<StudentModel> GetStudent(int id)
        {
            using (var dbModel = new EduTestEntities())
            {
                var st = await dbModel.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.UserId == id);

                return Mapper.Map<StudentModel>(st);
            }
        }

        public Task<int> GetTotalCount4Teacher(int teacherId)
        {
            using (var dbModel = new EduTestEntities())
            {
                return dbModel.Students
                    .CountAsync(s => s.User.Courses.Any(
                        c => c.Users.Any(u => u.Id == teacherId)));
            }
        }

        public async Task<int> AddStudent(StudentModel student)
        {
            using (var dbModel = new EduTestEntities())
            {
                var dbStud = Mapper.Map<Student>(student);
                dbModel.Students.Add(dbStud);
                await dbModel.SaveChangesAsync();
                return dbStud.UserId;
            }
        }
    }
}