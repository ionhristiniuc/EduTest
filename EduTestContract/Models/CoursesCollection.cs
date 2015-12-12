using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTestContract.Models
{
    public class CoursesCollection
    {
        public IEnumerable<CourseModel> Courses { get; set; }
        public Pagination Pagination { get; set; }
    }
}
