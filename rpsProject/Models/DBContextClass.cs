using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    public class RPS_DbContextClass : DbContext
    {
        public RPS_DbContextClass()
        { }

        public RPS_DbContextClass(DbContextOptions<RPS_DbContextClass> options)
            : base(options)
        { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlite("Data Source=RPS.db");
        }
    }
}
