using System.Linq;
using AspNetCoreIdentityBoilerplate.Repository;
using Microsoft.EntityFrameworkCore;

namespace MapMind.DataAccess.Repository.Map
{
    public class MindMapRepository: Repository<Domain.Map.MindMap>, IMindMapRepository
    {
        public MindMapRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Domain.Map.MindMap> AddIncludes(IQueryable<Domain.Map.MindMap> query)
        {
            return query.Include(mm => mm.Root);
        }
    }
}
