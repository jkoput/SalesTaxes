namespace SalesTaxes
{
    public class ShoppingBasketItem
    {
        public int Quantity { get; set; }
        public string Item { get; set; }
        public decimal Cost { get; set; } 
        public decimal Total { get; set; }

        public ShoppingBasketItem(int quantity, string item, decimal cost, decimal total=0.00m)
        {
            this.Quantity = quantity;
            this.Item = item;
            this.Cost = cost;
            this.Total = total;
        }
    }
}
