using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreIdentityBoilerplate.Entity;

namespace MapMind.Domain.Map
{
    public class MindMap: Entity
    {
        public string Name { get; set; }

        public virtual MapNode Root { get; set; }
    }
}
