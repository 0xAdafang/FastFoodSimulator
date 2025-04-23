# 🍔 FastFoodSimulator

**FastFoodSimulator** is a simple console-based simulation of a fast-food restaurant built in C#. It models the basic operations of a restaurant including customer arrivals, order taking, food preparation, and order serving — all handled asynchronously.

---

## Features

- Real-time simulation using `System.Threading.Timer`
- Customer → Order → Kitchen → Server workflow
- Modular service architecture
- Event-driven system with `Action<T>` delegates
- Clean code base, ideal for learning & experimentation

---

## Structure

- `CustomerGeneratorService`: Generates customers at intervals
- `OrderTakerService`: Converts a customer into an order
- `KitchenService`: Prepares orders after a delay
- `ServerService`: Serves ready orders to customers
- `SimulationController`: Coordinates the entire simulation
- `Domain/`: Contains core data models (Customer, Order, etc.)

---

## Requirements

- [.NET 7.0 or later](https://dotnet.microsoft.com/)
- Visual Studio / Rider / VS Code with C# support

---

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/FastFoodSimulator.git
   cd FastFoodSimulator
