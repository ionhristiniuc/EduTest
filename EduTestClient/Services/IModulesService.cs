using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface IModulesService
    {
        Task<bool> AddModule(int courseId, ModuleModel module);
        Task<bool> DeleteModule(int id);
    }
}
