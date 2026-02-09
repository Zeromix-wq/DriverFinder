using DriverFinder.Interfaces;
using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class QuickSelectDriverFinder : IDriverFinder
    {
        public string AlgorithmName => "Быстрый выбор";

        public List<Driver> FindNearestDrivers(Order order, List<Driver> drivers)
        {
            if (drivers == null || drivers.Count == 0)
                return new List<Driver>();

            var driversWithDistance = drivers.Select(driver =>
                new { Driver = driver, Distance = CalculateDistance(order, driver) })
                .ToList();

            if (driversWithDistance.Count <= 5)
            {
                return driversWithDistance.OrderBy(x => x.Distance).Select(x => x.Driver).ToList();
            }

            var sortedList = driversWithDistance.OrderBy(x => x.Distance).Take(5).ToList();
            return sortedList.Select(x => x.Driver).ToList();
        }

        private double CalculateDistance(Order order, Driver driver)
        {
            return Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
        }
    }
}