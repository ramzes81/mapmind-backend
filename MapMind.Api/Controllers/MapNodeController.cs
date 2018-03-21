using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityBoilerplate.Controller;
using AutoMapper;
using MapMind.Api.ViewModels.Map;
using MapMind.Domain.Map;
using MapMind.Service.Map;
using Microsoft.AspNetCore.Mvc;

namespace MapMind.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class MapNodeController: CrudController<MapNode, MapNodeViewModel, IMapNodeService>
    {
        public MapNodeController(IMapNodeService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
