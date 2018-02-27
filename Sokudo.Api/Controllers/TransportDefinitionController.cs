using AspNetCoreIdentityBoilerplate.Controller;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sokudo.Api.ViewModels.Transport;
using Sokudo.Domain.Transport;
using Sokudo.Service.Transport;

namespace Sokudo.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class TransportDefinitionController : 
        CrudController<TransportDefinition, TransportDefinitionViewModel, ITransportDefinitionService>
    {
        public TransportDefinitionController(ITransportDefinitionService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
