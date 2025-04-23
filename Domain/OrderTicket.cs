namespace FastFoodSimulator.Domain
{
    public class OrderTicket
    {
        public Order Order { get; set; }
        public TaskCompletionSource<bool> Prepared{ get; set; }
        
        public OrderTicket(Order order)
        {
                Order = order;
                Prepared = new TaskCompletionSource<bool>();
        }
        

    }
}