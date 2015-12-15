using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduTestContract.Models
{
    public class ChapterModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<TopicModel> Topics { get; set; }
        public IEnumerable<ModuleModel> Tests { get; set; }
    }
}