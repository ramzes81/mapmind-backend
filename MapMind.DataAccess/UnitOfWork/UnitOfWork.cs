using System;
using System.Collections.Generic;
using AspNetCoreIdentityBoilerplate.Repository;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using MapMind.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace MapMind.DataAccess.UnitOfWork
{
    public class UnitOfWork : UnitOfWork<MapMindContext>
    {
        private static readonly Dictionary<Type, Type> EntityToRepositoryMap = new Dictionary<Type, Type>
        {
        };

        public UnitOfWork(MapMindContext dbContext) : base(dbContext)
        {
        }

        public override IRepository<TEntity> CreateRepository<TEntity>(DbContext dbContext)
        {
            var entityType = typeof(TEntity);

            if (!(dbContext is MapMindContext))
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var repositoryType = EntityToRepositoryMap[entityType];

            if (repositoryType is null)
            {
                throw new NotImplementedException();
            }

            var repository = Activator.CreateInstance(repositoryType, dbContext);

            return (IRepository<TEntity>) repository;
        }
    }
}