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
    public class TransportManufacturerController :
        CrudController<TransportManufacturer, TransportManufacturerViewModel, ITransportManufacturerService>
    {
        public TransportManufacturerController(ITransportManufacturerService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
