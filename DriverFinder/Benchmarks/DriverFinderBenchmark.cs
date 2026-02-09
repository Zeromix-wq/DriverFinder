using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using DriverFinder.Algorithms;
using DriverFinder.Models;

namespace DriverFinder.Benchmarks
{
    [MemoryDiagnoser]
    // Просто указываем .NET 8 и baseline — без сложных параметров
    [SimpleJob(RuntimeMoniker.Net80, baseline: true)]
    [Config(typeof(FastConfig))]
    public class DriverFinderBenchmark
    {
        private readonly List<Driver> _drivers = new()
        {
            new Driver(1, 5, 5),
            new Driver(2, 6, 6),
            new Driver(3, 7, 7),
            new Driver(4, 4, 4),
            new Driver(5, 5, 6)
        };

        private readonly Order _order = new(5, 5);

        [Benchmark(Baseline = true)]
        public List<Driver> BruteForce()
        {
            var finder = new BruteForceDriverFinder();
            return finder.FindNearestDrivers(_order, _drivers);
        }

        [Benchmark]
        public List<Driver> HeapBased()
        {
            var finder = new HeapBasedDriverFinder();
            return finder.FindNearestDrivers(_order, _drivers);
        }

        [Benchmark]
        public List<Driver> GridBased()
        {
            var finder = new GridBasedDriverFinder();
            return finder.FindNearestDrivers(_order, _drivers);
        }

        [Benchmark]
        public List<Driver> QuickSelect()
        {
            var finder = new QuickSelectDriverFinder();
            return finder.FindNearestDrivers(_order, _drivers);
        }
    }

    public class FastConfig : ManualConfig
    {
        public FastConfig()
        {
            // В v0.13.5 — просто добавляем Job.Default
            Add(BenchmarkDotNet.Jobs.Job.Default.WithId("Fast"));
            // Отключаем проверку оптимизаций
            WithOptions(BenchmarkDotNet.Configs.ConfigOptions.DisableOptimizationsValidator);
        }
    }
}