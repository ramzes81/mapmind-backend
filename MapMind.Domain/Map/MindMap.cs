using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreIdentityBoilerplate.Entity;
using MapMind.Domain.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MapMind.Domain.Map
{
    public class MindMap: Entity
    {
        public string Name { get; set; }
        public string UserId { get; set; }

        [InverseProperty(nameof(MapNode.Map))]
        public virtual MapNode Root { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
