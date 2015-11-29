using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace EduTestService.Security
{
    public class SecurityHelper
    {
        public static int? GetUserId(IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity) identity;
            var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }
    }
}