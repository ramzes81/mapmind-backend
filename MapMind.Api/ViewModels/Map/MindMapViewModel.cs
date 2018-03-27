using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MapMind.Api.ViewModels.Base;
using MapMind.Api.ViewModels.User;
using MapMind.Domain.Map;

namespace MapMind.Api.ViewModels.Map
{
    public class MindMapViewModel: EntityViewModel
    {
        public string Name { get; set; }
        public PlainMapNodeViewModel Root { get; set; }

        public class MindMapViewModelProfile : Profile
        {
            public MindMapViewModelProfile()
            {
                CreateMap<MindMap, MindMapViewModel>();
            }
        }
    }
}
