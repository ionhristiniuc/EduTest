using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface IStudentsRepository
    {        
        Task<IEnumerable<StudentModel>> GetStudents(int page, int perPage);
        Task<IEnumerable<StudentModel>> GetStudents4Teacher(int teacherId, int page, int perPage);
        Task<int> GetTotalCount();
        Task<StudentModel> GetStudent(int id);
        Task<int> GetTotalCount4Teacher(int teacherId);
    }
}
