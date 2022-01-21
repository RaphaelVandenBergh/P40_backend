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
                new Users { email="admin@admin.com", Username = "Admin", Password = "Admin", userLevel=1},
                new Users { email="user@user.com", Username= "User", Password= "User", userLevel = 2});

            context.SaveChanges();
        }
    }
}
