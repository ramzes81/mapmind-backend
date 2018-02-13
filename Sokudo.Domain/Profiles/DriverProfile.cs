using System.Collections.Generic;
using Sokudo.Domain.Transport;

namespace Sokudo.Domain.Profiles
{
    public class DriverProfile
    {
        public virtual List<DriverTransport> Transports { get; set; }
    }
}