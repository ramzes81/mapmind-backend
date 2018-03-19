using AutoMapper;
using Sokudo.Api.ViewModels.Base;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.ViewModels.Transport
{
    public class TransportManufacturerViewModel: EntityViewModel
    {
        public class TransportManufacturerViewModelProfile: Profile
        {
            public TransportManufacturerViewModelProfile()
            {
                CreateMap<TransportManufacturer, TransportManufacturerViewModel>();
            }
        }

        public string Name { get; set; }

        public ICollection<TransportModelViewModel> Models { get; set; }
    }
}
