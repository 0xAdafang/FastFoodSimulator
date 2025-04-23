using FastFoodSimulator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenue dans FastFoodSimulator!");
        Console.Write("Entrez l'intervalle d'arrivée des clients (en ms) : ");
        int arrivalInterval = int.Parse(Console.ReadLine() ?? "2000");

        Console.Write("Entrez le temps de préparation des commandes (en ms) : ");
        int preparationInterval = int.Parse(Console.ReadLine() ?? "3000");

        Console.Write("Entrez le temps de service des commandes (en ms) : ");
        int servingInterval = int.Parse(Console.ReadLine() ?? "2000");

        var simulation = new SimulationController(arrivalInterval, preparationInterval, servingInterval);
        simulation.Start();
    }
}
