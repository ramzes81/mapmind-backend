using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreIdentityBoilerplate.Service;
using AspNetCoreIdentityBoilerplate.UnitOfWork;
using MapMind.Domain.Map;

namespace MapMind.Service.Map
{
    public class MindMapService: CrudService<MindMap>, IMindMapService
    {
        public MindMapService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
