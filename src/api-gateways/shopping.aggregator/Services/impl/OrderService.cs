using shopping.aggregator.Extensions;
using shopping.aggregator.Models;

namespace shopping.aggregator.Services.impl
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
