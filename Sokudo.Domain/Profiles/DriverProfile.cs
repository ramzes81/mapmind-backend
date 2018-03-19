using System.Collections.Generic;
using AspNetCoreIdentityBoilerplate.Entity;
using Sokudo.Domain.Transport;

namespace Sokudo.Domain.Profiles
{
    public class DriverProfile: Entity
    {
        public virtual List<DriverTransport> Transports { get; set; }
    }
}