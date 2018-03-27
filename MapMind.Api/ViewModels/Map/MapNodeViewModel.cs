using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MapMind.Domain.Map;

namespace MapMind.Api.ViewModels.Map
{
    public class MapNodeViewModel: PlainMapNodeViewModel
    {
        public PlainMapNodeViewModel Parent { get; set; }
        public ICollection<PlainMapNodeViewModel> Children { get; set; }

        public class MapNodeViewModelProfile : Profile
        {
            public MapNodeViewModelProfile()
            {
                CreateMap<MapNode, MapNodeViewModel>();
            }
        }
    }
}
