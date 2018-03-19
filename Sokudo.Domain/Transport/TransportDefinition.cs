using AspNetCoreIdentityBoilerplate.Entity;

namespace Sokudo.Domain.Transport
{
    public class TransportDefinition: Entity
    {
        public TransportType Type { get; set; }

        public int SeatsCount { get; set; }

        public virtual TransportManufacturer Manufacturer { get; set; }

        public virtual TransportModel Model { get; set; }
    }

    public enum TransportType
    {
        Car
    }
}