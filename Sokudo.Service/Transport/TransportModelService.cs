using AspNetCoreIdentityBoilerplate.Service;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Service.Transport
{
    public class TransportModelService : CrudService<TransportModel>, ITransportModelService
    {
        public TransportModelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
