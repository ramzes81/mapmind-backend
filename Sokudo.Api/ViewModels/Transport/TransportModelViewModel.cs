using AutoMapper;
using Sokudo.Api.ViewModels.Base;
using Sokudo.Domain.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.ViewModels.Transport
{
    public class TransportModelViewModel: EntityViewModel
    {
        public class TransportModelViewModelProfile : Profile
        {
            public TransportModelViewModelProfile()
            {
                CreateMap<TransportModel, TransportModelViewModel>();
            }
        }
        public string Name { get; set; }
    }
}
