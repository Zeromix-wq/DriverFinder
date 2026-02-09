using DriverFinder.Algorithms;
using DriverFinder.Benchmarks;
using DriverFinder.Interfaces;
using DriverFinder.Models;
using BenchmarkDotNet.Running;

namespace DriverFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа поиска водителей");
            Console.WriteLine("========================");

            var drivers = GenerateSampleDrivers(1000);
            var order = new Order(50, 50);

            Console.WriteLine($"Сгенерировано {drivers.Count} водителей и заказ в точке ({order.X}, {order.Y})");

            var algorithms = new List<IDriverFinder>
            {
                new BruteForceDriverFinder(),
                new HeapBasedDriverFinder(),
                new GridBasedDriverFinder(),
                new QuickSelectDriverFinder()
            };

            Console.WriteLine("\nТестирование алгоритмов:");
            foreach (var algorithm in algorithms)
            {
                var nearest = algorithm.FindNearestDrivers(order, drivers);
                Console.WriteLine($"{algorithm.AlgorithmName}: Найдено {nearest.Count} водителей");

                if (nearest.Any())
                {
                    Console.WriteLine($"  Ближайший водитель: {nearest.First()}");
                }
            }

            Console.WriteLine("\nЗапуск бенчмарков...");
            BenchmarkRunner.Run<DriverFinderBenchmark>();
        }

        static List<Driver> GenerateSampleDrivers(int count)
        {
            var random = new Random(42);
            var drivers = new List<Driver>();

            for (int i = 0; i < count; i++)
            {
                drivers.Add(new Driver(i, random.Next(0, 100), random.Next(0, 100)));
            }

            return drivers;
        }
    }
}