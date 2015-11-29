using System.Collections.Generic;

namespace EduTestContract.Models
{
    public class ChapterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TopicModel> Topics { get; set; }
        public IEnumerable<ModuleModel> Tests { get; set; }
    }
}