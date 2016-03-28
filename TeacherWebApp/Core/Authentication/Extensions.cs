using System.Web.Script.Serialization;
using EduTestClient.Services.Utils;

namespace TeacherWebApp.Core.Authentication
{
    public static class Extensions
    {
        public static string ToJson<T>(this T userData)
        {
            if (userData == null)
                return null;

            var ser = new JsonSerializer();
            return ser.Serialize(userData);
        }
    }
}