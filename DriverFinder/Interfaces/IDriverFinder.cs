using DriverFinder.Models;

namespace DriverFinder.Interfaces
{
    public interface IDriverFinder
    {
        List<Driver> FindNearestDrivers(Order order, List<Driver> drivers);
        string AlgorithmName { get; }
    }
}