using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityBoilerplate.Controller;
using AspNetCoreIdentityBoilerplate.DataQuery.Specification.Implementation.Compare;
using AutoMapper;
using MapMind.Api.ViewModels.Map;
using MapMind.Domain.Authentication;
using MapMind.Domain.Map;
using MapMind.Service.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MapMind.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class MindMapController: UserAwareCrudController<User, MindMap, MindMapViewModel, IMindMapService>
    {
        public MindMapController(UserManager<User> userManager, 
            IMindMapService service, IMapper mapper) : base(userManager, service, mapper)
        {
        }

        [Authorize]
        public override async Task<ActionResult> GetAll()
        {
            var specification = new EqualToSpecification<MindMap, string>
            {
                PropertyName = nameof(MindMap.UserId),
                Value = (await GetCurrentUserAsync()).Id,
            };
            return await Where(specification);
        }
    }
}
