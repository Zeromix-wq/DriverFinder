using DriverFinder.Interfaces;
using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class HeapBasedDriverFinder : IDriverFinder
    {
        public string AlgorithmName => "На основе кучи";

        public List<Driver> FindNearestDrivers(Order order, List<Driver> drivers)
        {
            if (drivers == null || drivers.Count == 0)
                return new List<Driver>();

            var pq = new PriorityQueue<Driver, double>();

            foreach (var driver in drivers)
            {
                double distance = CalculateDistance(order, driver);

                if (pq.Count < 5)
                {
                    pq.Enqueue(driver, distance);
                }
                else if (pq.TryPeek(out _, out double currentMax) && distance < currentMax)
                {
                    pq.Dequeue();
                    pq.Enqueue(driver, distance);
                }
            }

            // Извлекаем результаты
            var result = new List<(Driver driver, double distance)>();
            while (pq.Count > 0)
            {
                var driver = pq.Dequeue();
                var dist = CalculateDistance(order, driver);
                result.Add((driver, dist));
            }

            return result.OrderBy(x => x.distance).Select(x => x.driver).ToList();
        }

        private double CalculateDistance(Order order, Driver driver)
        {
            return Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
        }
    }
}