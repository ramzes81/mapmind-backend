using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AspNetCoreIdentityBoilerplate.Entity;

namespace MapMind.Domain.Map
{
    public class MapNode: Entity
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public int? MapId { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(MapId))]
        public virtual MindMap Map { get; set; }
        public virtual ICollection<MapNode> Children { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual MapNode Parent { get; set; }
    }
}
