using ChallengeCrf.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MongoFramework;

namespace ChallengeCrf.Infra.Data.Context;

public class EventStoreSqlContext : MongoDbContext
{
    public DbSet<StoredEvent> StoredEvent { get; set; } = null!;
    private readonly IHostingEnvironment _env;

    public EventStoreSqlContext(IMongoDbConnection connection, IHostingEnvironment env) : base(connection)
    {
        _env = env;
    }
    //public EventStoreSqlContext (IHostingEnvironment env)
    //{
    //    _env = env;
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.ApplyConfiguration(new StoredEventMap());
    //    base.OnModelCreating(modelBuilder);
    //}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    var config = new ConfigurationBuilder()
    //            .SetBasePath(_env.ContentRootPath)
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //    optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
    //}
}
