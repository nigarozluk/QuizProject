using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.DAL
{
   public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProjectContext>();

                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    var user = new[]
                    {
                        new User(){UserMail="admin_example_mail@gmail.com", UserName="admin_example_mail@gmail.com", UserPassword="123456", Role=RoleType.Admin},
                        new User(){UserMail="user_example_mail@gmail.com", UserName="user_example_mail@gmail.com", UserPassword="123456", Role=RoleType.User}
                    };
                    context.Users.AddRange(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
