using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MapMind.DataAccess.Context
{
    public class MapMindContextFactory : IDesignTimeDbContextFactory<MapMindContext>
    {
        private const string ConnectionString =
            "Server=.;Initial Catalog=MapMind;Persist Security Info=False;User ID=user;" +
            "Password=password;MultipleActiveResultSets=True;Encrypt=True;" +
            "TrustServerCertificate=True;Connection Timeout=30;";

        public MapMindContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MapMindContext>();

            // for publishing:
            // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#generating-a-sql-script

            optionsBuilder.UseSqlServer(ConnectionString);

            return new MapMindContext(optionsBuilder.Options);
        }
    }
}