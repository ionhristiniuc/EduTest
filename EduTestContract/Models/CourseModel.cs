using System.Collections.Generic;

namespace EduTestContract.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ModuleModel> Modules { get; set; }
        public IEnumerable<TestModel> Tests { get; set; }
    }
}