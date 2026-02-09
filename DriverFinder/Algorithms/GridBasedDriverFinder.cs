using DriverFinder.Interfaces;
using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class GridBasedDriverFinder : IDriverFinder
    {
        public string AlgorithmName => "На основе сетки";

        public List<Driver> FindNearestDrivers(Order order, List<Driver> drivers)
        {
            if (drivers == null || drivers.Count == 0)
                return new List<Driver>();

            var grid = new Dictionary<(int, int), List<Driver>>();
            foreach (var driver in drivers)
            {
                var pos = (driver.X, driver.Y);
                if (!grid.ContainsKey(pos))
                    grid[pos] = new List<Driver>();
                grid[pos].Add(driver);
            }

            var result = new List<Driver>();
            var visited = new HashSet<(int, int)>();

            var queue = new Queue<(int x, int y, int distance)>();
            queue.Enqueue((order.X, order.Y, 0));
            visited.Add((order.X, order.Y));

            while (result.Count < 5 && queue.Count > 0)
            {
                var (currentX, currentY, currentDist) = queue.Dequeue();

                if (grid.ContainsKey((currentX, currentY)))
                {
                    foreach (var driver in grid[(currentX, currentY)])
                    {
                        if (result.Count >= 5) break;
                        result.Add(driver);
                    }
                }

                if (result.Count >= 5) break;

                var directions = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
                foreach (var (dx, dy) in directions)
                {
                    var nextX = currentX + dx;
                    var nextY = currentY + dy;

                    if (!visited.Contains((nextX, nextY)))
                    {
                        visited.Add((nextX, nextY));
                        queue.Enqueue((nextX, nextY, currentDist + 1));
                    }
                }
            }

            if (result.Count < 5)
            {
                var remainingDrivers = drivers.Except(result)
                    .Select(d => new { Driver = d, Distance = CalculateDistance(order, d) })
                    .OrderBy(x => x.Distance)
                    .Take(5 - result.Count)
                    .Select(x => x.Driver)
                    .ToList();

                result.AddRange(remainingDrivers);
            }

            return result.Take(5).ToList();
        }

        private double CalculateDistance(Order order, Driver driver)
        {
            return Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
        }
    }
}