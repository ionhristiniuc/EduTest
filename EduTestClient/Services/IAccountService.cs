using System.Threading.Tasks;

namespace EduTestClient.Services
{
    public interface IAccountService
    {
        Task<string> Authenticate(string username, string password);        
    }
}
