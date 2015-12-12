using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public class ModulesRepository : IModulesRepository
    {
        public IEnumerable<ModuleModel> GetModules(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleModel> GetModule(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddModule(int courseId, ModuleModel module)
        {
            throw new NotImplementedException();
        }

        public Task UpdateModule(int id, ModuleModel moduleModel)
        {
            throw new NotImplementedException();
        }

        public void RemoveModule(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCourseId(int moduleId)
        {
            throw new NotImplementedException();
        }
    }
}