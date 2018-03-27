using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MapMind.Api.ViewModels.Base;
using MapMind.Domain.Map;

namespace MapMind.Api.ViewModels.Map
{
    public class PlainMapNodeViewModel: EntityViewModel
    {
        public string Text { get; set; }
        public string Color { get; set; }

        public class PlainMapNodeViewModelProfile : Profile
        {
            public PlainMapNodeViewModelProfile()
            {
                CreateMap<MapNode, PlainMapNodeViewModel>();
            }
        }
    }
}
