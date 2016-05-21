using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EduTestContract.Models;
using EduTestData.Model;

namespace EduTestService.Core.ObjectMapping.Profilers
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            //base.Configure();        

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>()
                .ForMember(u => u.Password, c => c.Ignore())
                .ForMember(u => u.QuestionBases, c => c.Ignore())
                .ForMember(u => u.Tests, c => c.Ignore())
                .ForMember(u => u.TestInstances, c => c.Ignore())
                .ForMember(u => u.Courses, c => c.Ignore())
                .ForMember(u => u.Student, c => c.Ignore())
                .ForMember(u => u.Teacher, c => c.Ignore());

            CreateMap<PersonalDetail, PersonalDetailModel>();
            CreateMap<PersonalDetailModel, PersonalDetail>()
                .ForMember(u => u.User, c => c.Ignore());

            CreateMap<Student, StudentModel>();

            CreateMap<StudentModel, Student>();

            CreateMap<Role, string>().ConvertUsing(role => role.Name);
            CreateMap<string, Role>().ConvertUsing((string role) => new Role() {Name = role});                 
        }
    }
}