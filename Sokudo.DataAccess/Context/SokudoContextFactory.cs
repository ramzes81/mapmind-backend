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
            optionsBuilder.UseSqlServer("Server=.,1433;Initial Catalog=Sokudo;Persist Security Info=False;User ID=user;Password=LetMeIn1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

            return new SokudoContext(optionsBuilder.Options);
        }
    }
}