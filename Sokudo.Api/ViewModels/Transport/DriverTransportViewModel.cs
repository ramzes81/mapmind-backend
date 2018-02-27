using AutoMapper;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.ViewModels.Transport
{
    public class DriverTransportViewModel
    {
        public class DriverTransportViewModelProfile : Profile
        {
            public DriverTransportViewModelProfile()
            {
                CreateMap<DriverTransport, DriverTransportViewModel>();
            }
        }

        public string Color { get; set; }

        public TransportDefinitionViewModel Transport { get; set; }
    }
}
