using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace EduTestService.Repositories
{
    public class StudentsRepository : BaseRepository, IStudentsRepository
    {        
        public IEnumerable<StudentModel> GetStudents(int page, int perPage)
        {
            using (var dbModel = new EduTestEntities())
            {
                var students = dbModel.Students
                    .Include(s => s.User)
                    .Include(s => s.User.PersonalDetail)
                    .Skip(page * perPage)
                    .Take(perPage)
                    .ToList();

                return base.Mapper.Map<IEnumerable<StudentModel>>(students);
            }
        }
    }
}