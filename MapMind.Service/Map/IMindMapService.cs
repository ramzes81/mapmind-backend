using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreIdentityBoilerplate.Service;
using MapMind.Domain.Map;

namespace MapMind.Service.Map
{
    public interface IMindMapService: ICrudService<MindMap>
    {
    }
}
