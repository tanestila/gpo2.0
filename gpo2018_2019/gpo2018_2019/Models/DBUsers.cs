using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpo2018_2019.Models
{
    public class DBUsers: DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DBUsers()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=sqlitedb.db");
        }
    }
}
