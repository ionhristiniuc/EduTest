using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestContract.Models.Enums;

namespace TeacherWebApp.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Enabled { get; set; }
        public QuestionType Type { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
    }
}