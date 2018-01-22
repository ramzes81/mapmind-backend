using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Transport
{
    public class Seat
    {
        public virtual DriverTransport DriverTransport { get; set; }
    }
}
