using FastFoodSimulator.Domain;

namespace FastFoodSimulator.Services
{
    public class OrderTakerService
    {
        private int _orderCount;
        public event Action<Order>? OrderTaken;

        public OrderTakerService()
        {
            _orderCount = 0;
        }

        public void TakeOrder(Customer customer)
        {
            _orderCount++;
            var order = new Order(_orderCount, customer, $"Commande pour {customer.Name}");
            OrderTaken?.Invoke(order);
        }
    }
}