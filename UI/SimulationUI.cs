namespace FastFoodSimulator.UI
{
    public class SimulationUI
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}