using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Profiles
{
    public class DriverProfile
    {
        public virtual List<DriverTransport> Transports { get; set; }
    }
}
