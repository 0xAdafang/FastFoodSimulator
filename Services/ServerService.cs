using FastFoodSimulator.Domain;

namespace FastFoodSimulator.Services
{
    public class ServerService
    {
        private Queue<OrderTicket> _readyOrders;
        private Timer? _servingTimer;
        private int _servingIntervalMilliseconds;

        public ServerService(int servingIntervalMilliseconds)
        {
            _readyOrders = new Queue<OrderTicket>();
            _servingIntervalMilliseconds = servingIntervalMilliseconds;
        }

        public void EnqueueReadyOrder(OrderTicket ticket)
        {
            _readyOrders.Enqueue(ticket);
        }

        public void Start()
        {
            _servingTimer = new Timer(ServeNextOrder, null, 0, _servingIntervalMilliseconds);
        }

        public void Stop()
        {
            _servingTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _servingTimer?.Dispose();
        }

        private void ServeNextOrder(object? state)
        {
            if (_readyOrders.Count == 0)
                return;

            var ticket = _readyOrders.Dequeue();
        }
    }
}
