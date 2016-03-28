using System;
using EduTestContract.Models.Enums;

namespace EduTestContract.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public EnvironmentType EnvType { get; set; }
        public int Timeout { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public TestType Type { get; set; }
    }
}