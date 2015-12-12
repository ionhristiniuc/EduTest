using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface ITopicsRepository
    {
        IEnumerable<TopicModel> GetTopics(int chapterId);
        Task<TopicModel> GetTopic(int id);
        Task<int> AddTopic(int chapterId, TopicModel topicModel);
        Task UpdateTopic(int id, TopicModel topic);
        void RemoveTopic(int id);
        int GetCourseId(int topicId);
        Task<bool> ExistsTopic(int id);
    }
}
