using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpo2.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
}
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(
                serviceProvider.GetRequiredService<DbContextOptions<UserContext>>()))
            {
                if (context.Users.Any() && context.Certificates.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.Add(
                     new User
                     {
                         login = "admin",
                         password = "admin"
                     }
                );
                context.Certificates.Add(
                    new Certificate()
                    {
                        userid = 1,
                        actual = true,
                        datefrom = DateTime.Parse("1.1.1997"),
                        dateto = DateTime.Parse("1.1.2099"),
                        valid = true,
                        serialnumber = "123456QWERTY",
                        subject = "GROTEST",
                        thumbprint = "123456"
                    });
                context.SaveChanges();
            }
        }
    }
}
