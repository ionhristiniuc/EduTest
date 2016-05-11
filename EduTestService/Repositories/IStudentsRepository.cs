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
        IEnumerable<StudentModel> GetStudents(int page, int perPage);
    }
}
