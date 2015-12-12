using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class ChaptersService : IChaptersService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string ServicePath = "/chapters";
        private HttpHelper HttpHelper { get; set; }

        public ChaptersService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<bool> AddChapter(int moduleId, ChapterModel chapter)
        {
            var path = ConfigManager.ServiceUrl + "/modules/" + moduleId + ServicePath;
            return await HttpHelper.PostEntity(chapter, path);
        }

        public async Task<bool> DeleteChapter(int id)
        {
            return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id);      
        }
    }
}
