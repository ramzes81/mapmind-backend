using AspNetCoreIdentityBoilerplate.Repository;
using Microsoft.EntityFrameworkCore;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sokudo.DataAccess.Repository.Transport
{
    public class TransportModelRepository : Repository<TransportModel>, ITransportModelRepository
    {
        public TransportModelRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<TransportModel> AddIncludes(IQueryable<TransportModel> query)
        {
            return query.Include(tm => tm.Manufacturer);
        }
    }
}
