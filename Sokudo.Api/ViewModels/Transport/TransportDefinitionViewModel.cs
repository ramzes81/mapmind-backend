using AutoMapper;
using Sokudo.Api.ViewModels.Base;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.ViewModels.Transport
{
    public class TransportDefinitionViewModel: EntityViewModel
    {
        public class TransportDefinitionViewModelProfile: Profile
        {
            public TransportDefinitionViewModelProfile()
            {
                CreateMap<TransportDefinition, TransportDefinitionViewModel>();
            }
        }

        public TransportType Type { get; set; }

        public int SeatsCount { get; set; }

        public TransportManufacturerViewModel Manufacturer { get; set; }

        public TransportModelViewModel Model { get; set; }
    }
}
