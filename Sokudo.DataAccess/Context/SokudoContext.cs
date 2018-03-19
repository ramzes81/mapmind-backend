using AspNetCoreIdentityBoilerplate.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sokudo.Domain.Authentication;
using Sokudo.Domain.Transport;

namespace Sokudo.DataAccess.Context
{
    public class SokudoContext : BaseIdentityDbContext<User>
    {
        public SokudoContext(DbContextOptions<SokudoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //identity entities
            ConfigureDefaultIdentityTables(modelBuilder);

            modelBuilder.Entity<TransportDefinition>().ToTable("TransportDefinitions");
        }

        DbSet<TransportDefinition> TransportDefinitions { get; set; }
    }
}