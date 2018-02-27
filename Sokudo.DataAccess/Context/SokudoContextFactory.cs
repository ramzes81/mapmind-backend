using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sokudo.DataAccess.Context
{
    public class SokudoContextFactory : IDesignTimeDbContextFactory<SokudoContext>
    {
        private const string CONNECTION_STRING =
            "Server=.,1433;Initial Catalog=Sokudo;Persist Security Info=False;User ID=user;" +
            "Password=password;MultipleActiveResultSets=True;Encrypt=True;" +
            "TrustServerCertificate=True;Connection Timeout=30;";

        public SokudoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SokudoContext>();

            // for publishing:
            // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#generating-a-sql-script

            optionsBuilder.UseSqlServer(CONNECTION_STRING);

            return new SokudoContext(optionsBuilder.Options);
        }
    }
}