using AspNetCoreIdentityBoilerplate.Entity;
using System.Collections.Generic;

namespace Sokudo.Domain.Transport
{
    public class DriverTransport: Entity
    {
        public string Color { get; set; }

        public virtual TransportDefinition Transport { get; set; }

        //public virtual List<Seat> Seats { get; set; }
    }
}