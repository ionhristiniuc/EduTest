namespace EduTestClient.Services
{
    public interface IAccountService
    {
        bool Authenticate(string username, string password);
    }
}
