using AutoMapper;
using EventBus.Messages.Events;
using ordering.application.Features.Orders.Commands.CheckoutOrder;

namespace ordering.api.Mappings
{
	public class OrderingProfile : Profile
	{
		public OrderingProfile()
		{
			CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
