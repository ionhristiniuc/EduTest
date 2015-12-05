using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTestClient.Services
{
    public static class ConfigManager
    {
        public static readonly string ServiceUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public static readonly string AuthenticationPath = ConfigurationManager.AppSettings["AuthPath"];       
    }
}
