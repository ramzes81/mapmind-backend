using Sokudo.Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Ride
{
    public class Ride
    {
        public DateTime ScheduledDate { get; set; }

        public virtual DriverProfile Driver { get; set; }
        public virtual Location StartLocation { get; set; }
        public virtual List<Location> Stops { get; set; } = new List<Location>();
        public virtual Location EndLocation { get; set; }
    }

    public enum RideStatus
    {

    }
}
