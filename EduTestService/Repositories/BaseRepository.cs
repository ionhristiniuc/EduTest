using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace EduTestService.Repositories
{
    public class BaseRepository
    {
        protected IMapper Mapper { get; }

        public BaseRepository()
        {
            Mapper = AutoMapper.Mapper.Instance;
        }
    }
}