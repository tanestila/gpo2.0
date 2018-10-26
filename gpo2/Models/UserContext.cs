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
                // Look for any movies.
                if (context.Users.Any() && context.Certificates.Any())
                {
                    return;   // DB has been seeded
                }
                if (!context.Users.Any())
                    context.Users.Add(
                     new User
                     {
                         login = "admin",
                         password = "admin"
                     }
                );
                if (!context.Certificates.Any())
                    context.Certificates.Add(
                        new Certificate
                        {
                            userid = 1,
                            serialnumber = "123456QWERTY",
                            thumbprint = "tyktyk",
                            datefrom = new DateTime(1997, 1, 1),
                            dateto = new DateTime(2099, 1, 1),
                            actual = true,
                            valid = true,
                            subject = "gpoCA"
                        });
                context.SaveChanges();
            }
        }
    }
}
