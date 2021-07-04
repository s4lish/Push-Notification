using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PushNotification.Models;
using Microsoft.Extensions.Configuration;

namespace PushNotification.Context
{
    public class PushDBContext :DbContext
    {

        private IConfiguration _config;

        public PushDBContext(DbContextOptions<PushDBContext> options, IConfiguration configuration) : base(options){

            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:PushDB"]);
            }
        }

        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<LogConnect> LogConnect { get; set; }


        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<Notifications>();
            Builder.Entity<LogConnect>();


        }

    }
}
