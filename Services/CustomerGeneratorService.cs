using FastFoodSimulator.Domain;

namespace FastFoodSimulator.Services
{
    public class CustomerGeneratorService
    {
        private Timer? _timer;
        private int _intervalMilliseconds;
        private int _customerCount;

        public event Action<Customer>? CustomerGenerated;

        public CustomerGeneratorService(int intervalMilliseconds, int startCustomerCount)
        {
            _intervalMilliseconds = intervalMilliseconds;
            _customerCount = startCustomerCount;
        }

        public void Start()
        {
            _timer = new Timer(GenerateCustomer, null, 0, _intervalMilliseconds);
        }

        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
        }

        private void GenerateCustomer(object? state)
        {
            _customerCount++;
            var customer = new Customer(_customerCount, $"Customer {_customerCount}");
            CustomerGenerated?.Invoke(customer);
        }
    }
}
