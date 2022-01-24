using P4._0_backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class DBInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            context.AddRange(
                new Users { email="admin@admin.com", Password = "Admin", FirstName="Admin",LastName="Admin", userLevel=1},
                new Users { email="user@user.com", Password= "User", FirstName = "User", LastName = "User", userLevel = 2});

            context.SaveChanges();

            context.AddRange(
                new Activity { Action = "admin created", Description = "admin created", UsersId = 1, Created_at = DateTime.Now },
                new Activity { Action = "user created", Description = "user created", UsersId = 2, Created_at = DateTime.Now });

            context.SaveChanges();
        }
    }
}
