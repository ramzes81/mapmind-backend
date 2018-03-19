using AspNetCoreIdentityBoilerplate.Repository;
using Microsoft.EntityFrameworkCore;
using Sokudo.DataAccess.Context;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sokudo.DataAccess.Repository.Transport
{
    public class TransportManufacturerRepository : Repository<TransportManufacturer>, ITransportManufacturerRepository
    {
        public TransportManufacturerRepository(SokudoContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<TransportManufacturer> AddIncludes(IQueryable<TransportManufacturer> query)
        {
            return query.Include(tm => tm.Models);
        }
    }
}
