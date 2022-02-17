using shopping.aggregator.Models;

namespace shopping.aggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
