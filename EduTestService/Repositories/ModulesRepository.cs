using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;
using EduTestData.Model;

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

        public async Task<int> AddModule(int courseId, ModuleModel moduleModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var module = new Module()
                {
                    Name = moduleModel.Name,
                    CourseId = courseId
                };
                dbContext.Modules.Add(module);

                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("UserRepository.AddUser: Could not add user to db");

                return module.Id;
            }
        }

        public Task UpdateModule(int id, ModuleModel moduleModel)
        {
            throw new NotImplementedException();
        }

        public async void RemoveModule(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var module = await dbContext.Modules.FirstOrDefaultAsync(u => u.Id == id);

                if (module == null)
                    throw new ObjectNotFoundException("UserRepository.RemoveModule: Module not found");

                dbContext.Modules.Remove(module);
                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("UserRepository.RemoveModule: Could not remove user from db");
            }
        }

        public int GetCourseId(int moduleId)
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Modules.First(m => m.Id == moduleId).CourseId;
            }
        }

        public async Task<bool> ExistsModule(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                return await dbContext.Modules.AnyAsync(u => u.Id == id);
            }
        }
    }
}