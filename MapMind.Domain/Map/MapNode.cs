﻿using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreIdentityBoilerplate.Entity;

namespace MapMind.Domain.Map
{
    public class MapNode: Entity
    {
        public string Text { get; set; }
        public string Color { get; set; }

        public virtual ICollection<MapNode> Children { get; set; }
        public virtual MapNode Parent { get; set; }
    }
}
