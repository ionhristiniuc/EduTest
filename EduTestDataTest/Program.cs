using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestData.Model;
using Ninject.Infrastructure.Language;

namespace EduTestDataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EduTestEntities())
            {
                //var users = context.Users.ToList();
                //foreach (var user in users)
                //{
                //    Console.WriteLine(user.Id + " " + user.FirstName + " " + user.LastName);

                //    Console.Write("Roles: ");
                //    foreach (var role in user.Roles)
                //    {
                //        Console.Write("{0} ", role.Name);
                //    }
                //    Console.WriteLine("\n");

                //    var course = user.Courses.FirstOrDefault();
                //}                                
                var user = context.Users.First();
                var roles = context.Roles.ToEnumerable();
                user.Roles.Clear();

                foreach (var ro in roles)
                {
                    user.Roles.Add(ro);
                }
                //user.Roles = roles.ToList();
                context.SaveChanges();

                var modules = context.Modules.Include(m => m.Chapters).ToList();                
                foreach (var module in modules)
                {
                    Console.WriteLine(module.Name + "\n");                    
                }
            }
        }
    }
}
