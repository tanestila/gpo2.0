using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpo2.Models
{
    //public class UserContext:DbContext
    //{
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlite("Filename=MyDatabase.db");
    //    }
    //}
    //public static class SeedData
    //{
    //    public static void Initialize(IServiceProvider serviceProvider)
    //    {
    //        using (var context = new UserContext(
    //            serviceProvider.GetRequiredService<DbContextOptions<UserContext>>()))
    //        {
    //            // Look for any movies.
    //            if (context.User.Any())
    //            {
    //                return;   // DB has been seeded
    //            }

    //            context.User.Add(
    //                 new User
    //                 {
    //                     login = "admib",
    //                     password = "admin"
    //                 }
    //            );
    //            context.SaveChanges();
    //        }
    //    }
    //}
}
