using System.Web.Script.Serialization;

namespace EduTestClient.Services.Utils
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj) 
        {
            var jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(obj);
        }

        public T Deserialize<T>(string str)
        {
            var jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Deserialize< T >(str);
        }
    }
}
