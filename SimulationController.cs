using FastFoodSimulator.Domain;
using FastFoodSimulator.Services;
using FastFoodSimulator.UI;

namespace FastFoodSimulator
{
    public class SimulationController
    {
        private bool _isRunning;
        private SimulationUI _ui;
        private CustomerGeneratorService _customerGenerator;
        private OrderTakerService _orderTaker;
        private KitchenService _kitchenService;
        private ServerService _serverService;

        public SimulationController(int arrivalIntervalMs, int preparationIntervalMs, int servingIntervalMs)
        {
            _ui = new SimulationUI();
            _customerGenerator = new CustomerGeneratorService(arrivalIntervalMs, 0);
            _customerGenerator.CustomerGenerated += OnCustomerGenerated;

            _orderTaker = new OrderTakerService();
            _orderTaker.OrderTaken += OnOrderTaken;

            _kitchenService = new KitchenService(3000);
            _kitchenService.OrderPrepared += OnOrderPrepared;

            _serverService = new ServerService(2000);
        }

        public void Start()
        {
            _isRunning = true;
            _ui.DisplayMessage("Simulation démarrée!");

            _customerGenerator.Start();
            _kitchenService.Start();
            _serverService.Start();

            while (_isRunning)
            {
                Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _customerGenerator.Stop();
            _kitchenService.Stop();
            _serverService.Stop();
            _ui.DisplayMessage("Simulation arrêtée.");
        }

        private void OnCustomerGenerated(Customer customer)
        {
            _ui.DisplayMessage($"Nouveau client: {customer.Name}");
            _orderTaker.TakeOrder(customer);
        }

        private void OnOrderTaken(Order order)
        {
            _ui.DisplayMessage($"Commande prise: {order.Details}");
            _kitchenService.EnqueueOrder(order);
        }

        private void OnOrderPrepared(OrderTicket ticket)
        {
            _ui.DisplayMessage($"Commande prête: {ticket.Order.Details}");
            _serverService.EnqueueReadyOrder(ticket);
        }
    }
}