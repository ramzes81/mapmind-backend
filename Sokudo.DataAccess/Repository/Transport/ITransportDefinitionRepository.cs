using AspNetCoreIdentityBoilerplate.Repository;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.DataAccess.Repository.Transport
{
    public interface ITransportDefinitionRepository: IRepository<TransportDefinition>
    {
    }
}
