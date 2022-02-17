using shopping.aggregator.Extensions;
using shopping.aggregator.Models;

namespace shopping.aggregator.Services.impl
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client;
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
