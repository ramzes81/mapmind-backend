using AspNetCoreIdentityBoilerplate.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Transport
{
    public class TransportManufacturer: Entity
    {
        public string Name { get; set; }

        public virtual ICollection<TransportModel> Models { get; set; }
    }
}
