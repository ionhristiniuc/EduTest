using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestClient.Services.Utils;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public class QuestionsService : IQuestionsService
    {
        private string AccessToken { get; set; }
        private ISerializer Serializer { get; set; }
        private const string ServicePath = "/questions";
        private HttpHelper HttpHelper { get; set; }

        public QuestionsService(string accessToken, ISerializer serializer)
        {
            AccessToken = accessToken;
            Serializer = serializer;
            HttpHelper = new HttpHelper(AccessToken, Serializer);
        }

        public async Task<bool> AddQuestion(int topicId, QuestionBaseModel question)
        {
            var path = $"{ConfigManager.ServiceUrl}/topics/{topicId}{ServicePath}";
            return await HttpHelper.PostEntity(question, path);
        }

        public Task<bool> DeleteQuestion(int id)
        {
            throw new NotImplementedException();
        }
    }
}
