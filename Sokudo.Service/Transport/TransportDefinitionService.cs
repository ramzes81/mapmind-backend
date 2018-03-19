using AspNetCoreIdentityBoilerplate.Service;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Service.Transport
{
    public class TransportDefinitionService: CrudService<TransportDefinition>, ITransportDefinitionService
    {
        public TransportDefinitionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
