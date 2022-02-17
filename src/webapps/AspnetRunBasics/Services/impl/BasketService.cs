using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services.impl
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
            var response = await _client.GetAsync($"/basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var response = await _client.PostAsJson($"/basket", model);
            response.EnsureSuccessStatusCode();
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var response = await _client.PostAsJson($"/basket/Checkout", model);
            response.EnsureSuccessStatusCode();
        }
    }
}
