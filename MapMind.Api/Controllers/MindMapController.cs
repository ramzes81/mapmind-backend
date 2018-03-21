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
    public class MindMapController: CrudController<MindMap, MindMapViewModel, IMindMapService>
    {
        public MindMapController(IMindMapService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
