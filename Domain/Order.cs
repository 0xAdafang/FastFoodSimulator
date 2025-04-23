namespace FastFoodSimulator.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public string Details { get; set; }

        public Order(int orderId, Customer customer, string details)
        {
            OrderId = orderId;
            Customer = customer;
            Details = details;
        }
    }
}