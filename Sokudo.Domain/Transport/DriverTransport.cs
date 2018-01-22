using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Transport
{
    public class DriverTransport
    {
        public virtual TransportDefinition TransportDefinition { get; set; }

        public virtual List<Seat> Seats { get; set; }
    }
}
