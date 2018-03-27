using System.Linq;
using AspNetCoreIdentityBoilerplate.Context;
using MapMind.Domain.Authentication;
using MapMind.Domain.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //identity entities
            ConfigureDefaultIdentityTables(modelBuilder);
        }

        public virtual DbSet<MindMap> Maps { get; set; }
        public virtual DbSet<MapNode> MapNodes { get; set; }
    }
}