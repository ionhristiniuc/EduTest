using System.Threading.Tasks;
using EduTestClient.Services.Entities;

namespace EduTestClient.Services
{
    public interface IAccountService
    {
        Task<bool> Authenticate(string username, string password);
        AuthenticationResponse AuthResponse { get; }
    }
}
