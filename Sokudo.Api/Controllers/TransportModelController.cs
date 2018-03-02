using AspNetCoreIdentityBoilerplate.Controller;
using AspNetCoreIdentityBoilerplate.DataQuery.Specification;
using AspNetCoreIdentityBoilerplate.DataQuery.Specification.Implementation.Compare;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sokudo.Api.ViewModels.Transport;
using Sokudo.Domain.Transport;
using Sokudo.Service.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class TransportModelController :
        CrudController<TransportModel, TransportModelViewModel, ITransportModelService>
    {
        public TransportModelController(ITransportModelService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet("/TransportManufacturer/{manufacturerId}/Where")]
        public async Task<IActionResult> GetTransportManufacturerModelWhere([FromRoute]int manufacturerId, Specification<TransportModel> specification)
        {
            specification = specification.And(new EqualToSpecification<TransportModel, int>()
            {
                PropertyName = nameof(TransportModel.ManufacturerId),
                Value = manufacturerId,
            });
            return await base.Where(specification);
        }
    }
}
