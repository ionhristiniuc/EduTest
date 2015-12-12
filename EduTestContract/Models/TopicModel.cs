using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduTestContract.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<TestModel> Tests { get; set; }
    }
}