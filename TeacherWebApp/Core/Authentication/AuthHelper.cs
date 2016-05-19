using System.Security.Principal;
using EduTestClient.Services.Entities;
using EduTestContract.Models;

namespace TeacherWebApp.Core.Authentication
{
    public static class AuthHelper
    {
        public static AuthenticationResponse GetTokens(IPrincipal user)
        {
            var custUser = user as CustomPrincipal;
            return custUser?.UserData?.AuthTicket;
        }

        public static string GetAccessToken(IPrincipal user)
        {
            return GetTokens(user)?.access_token;
        }

        public static UserModel GetCurrentUser(IPrincipal user)
        {
            var custUser = user as CustomPrincipal;
            return custUser?.User;
        }
    }
}