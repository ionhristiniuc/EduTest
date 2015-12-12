using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class TopicsService : ITopicsService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string ServicePath = "/topics";
        private HttpHelper HttpHelper { get; set; }

        public TopicsService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<bool> AddTopic(int chapterId, TopicModel topic)
        {
            var path = ConfigManager.ServiceUrl + "/chapters/" + chapterId + ServicePath;
            return await HttpHelper.PostEntity(topic, path);
        }

        public async Task<bool> DeleteTopic(int id)
        {
            return await HttpHelper.DeleteEntity(ConfigManager.ServiceUrl + ServicePath + "/" + id);      
        }
    }
}
