using System.Linq;
using AspNetCoreIdentityBoilerplate.Repository;
using MapMind.Domain.Map;
using Microsoft.EntityFrameworkCore;

namespace MapMind.DataAccess.Repository.Map
{
    public class MapNodeRepository: Repository<MapNode>, IMapNodeRepository
    {
        public MapNodeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<MapNode> AddIncludes(IQueryable<MapNode> query)
        {
            return query.Include(mn => mn.Parent).Include(mn => mn.Children);
        }
    }
}
