using System.Collections.Generic;

namespace Sokudo.Domain.Transport
{
    public class DriverTransport
    {
        public virtual TransportDefinition TransportDefinition { get; set; }

        public virtual List<Seat> Seats { get; set; }
    }
}