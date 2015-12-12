using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public class TopicsRepository : ITopicsRepository
    {
        public IEnumerable<TopicModel> GetTopics(int chapterId)
        {
            throw new NotImplementedException();
        }

        public Task<TopicModel> GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddTopic(int chapterId, TopicModel topic)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopic(int id, TopicModel topic)
        {
            throw new NotImplementedException();
        }

        public void RemoveTopic(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCourseId(int topicId)
        {
            throw new NotImplementedException();
        }
    }
}