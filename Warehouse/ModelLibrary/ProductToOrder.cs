namespace ModelLibrary
{
    public class ProductToOrder
    {
        public int prodToOrderId { get; set; }
        public string prodDescription { get; set; }
        public int categoryId { get; set; }
        public int quantity { get; set; }
        public long orderId { get; set; }
        public double price { get; set; }
        public double discountPercentage { get; set; }
        public virtual Order order { get; set; }
        public virtual Product product { get; set; }
    }
}
