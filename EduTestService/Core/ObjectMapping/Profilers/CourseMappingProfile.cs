using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Core.ObjectMapping.Profilers
{
    public class CourseMappingProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<int, Course>().ConvertUsing((int id) => new Course() { Id = id });
            CreateMap<Course, int>().ConvertUsing(c => c.Id);

            CreateMap<Course, CourseModel>()
                .ForMember(c => c.Tests, c => c.Ignore());

            CreateMap<Module, ModuleModel>()
                .ForMember(m => m.Tests, c => c.Ignore());

            CreateMap<Chapter, ChapterModel>()
                .ForMember(c => c.Tests, c => c.Ignore());

            CreateMap<Topic, TopicModel>()
                .ForMember(t => t.Tests, c => c.Ignore());
        }
    }
}
