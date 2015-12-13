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

        public async Task<int> AddTopic(int chapterId, TopicModel topicModel)
        {
            using (var dbContext = new EduTestEntities())
            {
                var topic = new Topic()
                {
                    Name = topicModel.Name,
                    ChapterId = chapterId
                };
                dbContext.Topics.Add(topic);

                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("TopicRepository.AddTopic: Could not add topic to db");

                return topic.Id;
            }  
        }

        public Task UpdateTopic(int id, TopicModel topic)
        {
            throw new NotImplementedException();
        }

        public async void RemoveTopic(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                var topic = await dbContext.Topics.FirstOrDefaultAsync(u => u.Id == id);

                if (topic == null)
                    throw new ObjectNotFoundException("TopicsRepository.RemoveTopic: Topic not found");

                if (topic.QuestionBases.Any() || await dbContext.Tests.AnyAsync(t => t.ParentId == id))
                    throw new InvalidOperationException("TopicsRepository.RemoveTopic cannot remove topic as it has children");

                dbContext.Topics.Remove(topic);
                if (await dbContext.SaveChangesAsync() == 0)
                    throw new Exception("TopicsRepository.RemoveTopic: Could not remove topic from db");
            }
        }

        public int GetCourseId(int topicId)
        {
            using (var dbContext = new EduTestEntities())
            {
                return dbContext.Topics.First(c => c.Id == topicId).Chapter.Module.CourseId;
            }
        }

        public async Task<bool> ExistsTopic(int id)
        {
            using (var dbContext = new EduTestEntities())
            {
                return await dbContext.Topics.AnyAsync(u => u.Id == id);
            }
        }
    }
}