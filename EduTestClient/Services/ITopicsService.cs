using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestClient.Services
{
    public interface ITopicsService
    {
        Task<bool> AddTopic(int chapterId, TopicModel topic);
        Task<bool> DeleteTopic(int id); 
    }
}