using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface IModulesRepository
    {
        IEnumerable<ModuleModel> GetModules(int courseId);        
        Task<ModuleModel> GetModule(int id);
        Task<int> AddModule(int courseId, ModuleModel moduleModel);
        Task UpdateModule(int id, ModuleModel moduleModel);
        void RemoveModule(int id);        
        int GetCourseId(int moduleId);
        Task<bool> ExistsModule(int id);
    }
}
