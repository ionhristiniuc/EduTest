using System.Threading.Tasks;
using EduTestClient.Services.Entities;

namespace EduTestClient.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Authenticate(string username, string password);        
    }
}
