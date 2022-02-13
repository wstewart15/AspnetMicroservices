using AutoMapper;
using ordering.application.Features.Orders.Queries.GetOrdersList;
using ordering.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordering.application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            //CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
