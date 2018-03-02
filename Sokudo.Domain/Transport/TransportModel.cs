using AspNetCoreIdentityBoilerplate.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sokudo.Domain.Transport
{
    public class TransportModel: Entity
    {
        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        public virtual TransportManufacturer Manufacturer { get; set; }
    }
}
