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
    public class TransportDefinitionRepository: Repository<TransportDefinition>, ITransportDefinitionRepository
    {
        public TransportDefinitionRepository(SokudoContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<TransportDefinition> AddIncludes(IQueryable<TransportDefinition> query)
        {
            return query.Include(td => td.Manufacturer).Include(td => td.Model);
        }
    }
}
