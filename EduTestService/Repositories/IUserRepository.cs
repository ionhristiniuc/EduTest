using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestService.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUser(string email, string password);
        Task<UserModel> GetUser(int id);
        Task<int> AddUser(UserModel userModel);
        Task UpdateUser(int id, UserModel userModel);
        void RemoveUser(int id);
        Task<bool> ExistsUser(string email);
        Task<bool> HaveSameCourse(int firstUserId, int secondUserId);
        Task<bool> UserHasCourse(int userId, int courseId);     
    }
}
