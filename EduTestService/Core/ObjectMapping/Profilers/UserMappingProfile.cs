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
            CreateMap<Role, string>().ConvertUsing(role => role.Name);            
        }
    }
}