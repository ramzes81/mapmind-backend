using AspNetCoreIdentityBoilerplate.Context;
using MapMind.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace MapMind.DataAccess.Context
{
    public class MapMindContext : BaseIdentityDbContext<User>
    {
        public MapMindContext(DbContextOptions<MapMindContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //identity entities
            ConfigureDefaultIdentityTables(modelBuilder);
        }
    }
}