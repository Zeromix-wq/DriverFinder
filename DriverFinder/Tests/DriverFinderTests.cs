using DriverFinder.Algorithms;
using DriverFinder.Interfaces;
using DriverFinder.Models;
using NUnit.Framework;

namespace DriverFinder.Tests
{
    [TestFixture]
    public class DriverFinderTests
    {
        [Test]
        public void BruteForceDriverFinder_FindNearestDrivers_ReturnsFiveClosestDrivers()
        {
            var finder = new BruteForceDriverFinder();
            var order = new Order(0, 0);
            var drivers = new List<Driver>
            {
                new Driver(1, 1, 1),
                new Driver(2, 2, 2),
                new Driver(3, 0, 1),
                new Driver(4, 3, 4),
                new Driver(5, 0, 0),
                new Driver(6, 1, 0),
                new Driver(7, 5, 5)
            };

            var result = finder.FindNearestDrivers(order, drivers);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result[0].Id, Is.EqualTo(5)); // Closest driver
        }

        [Test]
        public void AllAlgorithms_ReturnSameNumberOfResults()
        {
            var order = new Order(0, 0);
            var drivers = new List<Driver>
            {
                new Driver(1, 1, 1),
                new Driver(2, 2, 2),
                new Driver(3, 0, 1),
                new Driver(4, 3, 4),
                new Driver(5, 0, 0),
                new Driver(6, 1, 0),
                new Driver(7, 5, 5),
                new Driver(8, 2, 1),
                new Driver(9, 1, 2),
                new Driver(10, 4, 3)
            };

            var algorithms = new List<IDriverFinder>
            {
                new BruteForceDriverFinder(),
                new HeapBasedDriverFinder(),
                new GridBasedDriverFinder(),
                new QuickSelectDriverFinder()
            };

            foreach (var algorithm in algorithms)
            {
                var result = algorithm.FindNearestDrivers(order, drivers);
                Assert.That(result.Count, Is.EqualTo(5), $"{algorithm.GetType().Name} должен вернуть 5 водителей");
            }
        }
    }
}