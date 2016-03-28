using System.Security.Principal;
using EduTestContract.Models;
using System.Linq;

namespace TeacherWebApp.Core.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        public UserModel User { get; set; }
        public IIdentity Identity { get; private set; }
        public UserData UserData { get; set; }      

        public CustomPrincipal(UserModel user, UserData uData)
        {
            this.User = user;

            UserData = uData;

            if (User != null)
                Identity = new GenericIdentity(user.Id.ToString());
        }        

        public bool IsInRole(string role)
        {
            return User.Roles.Contains(role);
        }        
    }
}