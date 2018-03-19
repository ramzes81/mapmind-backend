using Sokudo.Api.ViewModels.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Models.Auth
{
    public class DriverCompleteRegistrationModel: CompleteRegistrationModel
    {
        public DriverTransportViewModel Transport { get; set; }
    }
}
