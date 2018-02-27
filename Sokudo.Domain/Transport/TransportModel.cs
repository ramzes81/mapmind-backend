using AspNetCoreIdentityBoilerplate.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Transport
{
    public class TransportModel: Entity
    {
        public string Name { get; set; }

        public virtual TransportManufacturer Manufacturer { get; set; }
    }
}
