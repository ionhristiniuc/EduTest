using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class ModulesService : IModulesService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string ServicePath = "/modules";
        private HttpHelper HttpHelper { get; set; }

        public ModulesService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<bool> AddModule(int courseId, ModuleModel module)
        {
            var path = ConfigManager.ServiceUrl + "/courses/" + courseId + ServicePath;
            return await HttpHelper.PostEntity(module, path);
        }

        public async Task<bool> DeleteModule(int id)
        {
            return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id);      
        }
    }
}
