using AspNetCoreIdentityBoilerplate.Repository;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Sokudo.DataAccess.Context;
using Sokudo.DataAccess.Repository.Transport;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.DataAccess.UnitOfWork
{
    public class UnitOfWork : UnitOfWork<SokudoContext>
    {
        public UnitOfWork(SokudoContext dbContext) : base(dbContext)
        {
        }

        public override IRepository<TEntity> CreateRepository<TEntity>(DbContext dbContext)
        {
            var entityType = typeof(TEntity);
            var context = dbContext as SokudoContext;

            if(context is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if(entityType == typeof(TransportDefinition))
            {
                return (IRepository<TEntity>) new TransportDefinitionRepository(context);
            }

            if (entityType == typeof(TransportManufacturer))
            {
                return (IRepository<TEntity>) new TransportManufacturerRepository(context);
            }

            throw new NotImplementedException();
        }
    }
}
