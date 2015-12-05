using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestData.Model;

namespace EduTestService.Utils
{
    public class ObjectMapper
    {
        public static IEnumerable<Role> MapRoles(string[] roleNames)
        {
            using (var dbContext = new EduTestEntities())
            {
                var allRoles = dbContext.Roles.ToList();

                foreach (var role in allRoles)
                {
                    if (roleNames.Contains(role.Name))
                        yield return role;
                }
            }
        }
    }
}