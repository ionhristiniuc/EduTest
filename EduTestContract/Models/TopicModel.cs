using System.Collections.Generic;

namespace EduTestContract.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TestModel> Tests { get; set; }
    }
}