using ChallengeCrf.Domain.Models;
using ChallengeCrf.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChallengeCrf.Infra.Data.Context
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration _config;
        public DbSet<CashFlow> CashFlows { get; set; }
        public DbContextClass(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashFlowMap());
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_config.GetConnectionString("Default"));
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
