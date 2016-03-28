using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models.Enums;

namespace EduTestContract.Models
{
    public abstract class QuestionBaseModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Enabled { get; set; }
        public QuestionType Type { get; set; }
        public int UserId { get; set; }       
        public int TopicId { get; set; }       
    }
}
