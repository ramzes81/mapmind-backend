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
        private static readonly Dictionary<Type, Type> EntityToRepositoryMap = new Dictionary<Type, Type>
        {
            {typeof(TransportDefinition), typeof(TransportDefinitionRepository)},
            {typeof(TransportManufacturer), typeof(TransportManufacturerRepository)},
            {typeof(TransportModel), typeof(TransportModelRepository)},
        };

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

            var repositoryType = EntityToRepositoryMap[entityType];

            if(repositoryType is null)
            {
                throw new NotImplementedException();
            }

            return (IRepository<TEntity>) Activator.CreateInstance(repositoryType, dbContext);
        }
    }
}
