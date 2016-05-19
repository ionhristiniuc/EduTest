using EduTestClient.Services.Base;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class StudentsService : GenericService<StudentModel>, IStudentsService
    {
        public StudentsService()
            : base(null, "/students")
        {

        }
    }
}