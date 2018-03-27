using System.Linq;
using AspNetCoreIdentityBoilerplate.Repository;
using MapMind.Domain.Map;
using Microsoft.EntityFrameworkCore;

namespace MapMind.DataAccess.Repository.Map
{
    public class MindMapRepository: Repository<MindMap>, IMindMapRepository
    {
        public MindMapRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<MindMap> AddIncludes(IQueryable<MindMap> query)
        {
            return query.Include(mm => mm.Root);
        }
    }
}
