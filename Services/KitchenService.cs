using FastFoodSimulator.Domain;

namespace FastFoodSimulator.Services
{
    public class KitchenService
    {
        private Queue<OrderTicket> _pendingOrders;
        private Timer? _preparationTimer;
        private int _preparationTimeMilliseconds;
        public event Action<OrderTicket>? OrderPrepared;

        public KitchenService(int preparationTimeMilliseconds)
        {
            _pendingOrders = new Queue<OrderTicket>();
            _preparationTimeMilliseconds = preparationTimeMilliseconds;
        }

        public void EnqueueOrder(Order order)
        {
            var ticket = new OrderTicket(order);
            _pendingOrders.Enqueue(ticket);
        }

        public void Start()
        {
            _preparationTimer = new Timer(PrepareNextOrder, null, 0, _preparationTimeMilliseconds);
        }

        public void Stop()
        {
            _preparationTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _preparationTimer?.Dispose();
        }

        private void PrepareNextOrder(object? state)
        {
            if (_pendingOrders.Count == 0)
                return;

            var ticket = _pendingOrders.Dequeue();
            ticket.Prepared.SetResult(true);
            OrderPrepared?.Invoke(ticket);
        }
    }
}