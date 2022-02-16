using AutoMapper;
using basket.api.Entities;
using EventBus.Messages.Events;

namespace basket.api.Mapper
{
	public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
