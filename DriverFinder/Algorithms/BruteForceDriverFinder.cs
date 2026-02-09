using DriverFinder.Interfaces;
using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class BruteForceDriverFinder : IDriverFinder
    {
        public string AlgorithmName => "Полный перебор";

        public List<Driver> FindNearestDrivers(Order order, List<Driver> drivers)
        {
            if (drivers == null || drivers.Count == 0)
                return new List<Driver>();

            var driversWithDistance = drivers.Select(driver =>
                new { Driver = driver, Distance = CalculateDistance(order, driver) })
                .OrderBy(x => x.Distance)
                .Take(5)
                .Select(x => x.Driver)
                .ToList();

            return driversWithDistance;
        }

        private double CalculateDistance(Order order, Driver driver)
        {
            return Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
        }
    }
}