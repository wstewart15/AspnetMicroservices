namespace basket.api.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string ProductId { get; set; } = String.Empty;
        public string ProductName { get; set; } = String.Empty;
    }
}