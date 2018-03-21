using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreIdentityBoilerplate.Service;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using MapMind.Domain.Map;

namespace MapMind.Service.Map
{
    public class MapNodeService: CrudService<MapNode>, IMapNodeService
    {
        public MapNodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
