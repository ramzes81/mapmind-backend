using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sokudo.DataAccess.Context
{
    public class SokudoContextFactory : IDesignTimeDbContextFactory<SokudoContext>
    {
        public SokudoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SokudoContext>();
            //put here connection string to create and apply migrations
            optionsBuilder.UseSqlServer("");

            return new SokudoContext(optionsBuilder.Options);
        }
    }
}