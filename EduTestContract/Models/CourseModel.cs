using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduTestContract.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<ModuleModel> Modules { get; set; }
        public IEnumerable<TestModel> Tests { get; set; }
    }
}