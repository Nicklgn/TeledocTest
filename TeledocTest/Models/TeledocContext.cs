using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeledocTest.Models
{
    public class TeledocContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }

        public TeledocContext(DbContextOptions<TeledocContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
