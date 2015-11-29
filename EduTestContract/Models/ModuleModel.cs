using System.Collections.Generic;

namespace EduTestContract.Models
{
    public class ModuleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ChapterModel> Chapters { get; set; }
        public IEnumerable<TestModel> Tests { get; set; }
    }
}