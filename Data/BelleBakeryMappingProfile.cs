using AutoMapper;
using RebeccaResources.Data.Entities;
using RebeccaResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Data
{
    public class BelleBakeryMappingProfile : Profile
    {
        public BelleBakeryMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                 .ForMember(o => o.OrderItemId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}
