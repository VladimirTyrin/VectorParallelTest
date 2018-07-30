using BenchmarkDotNet.Running;
using VectorParallelTest.Common;

namespace VectorParallelTest.Runner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<AverageComputationBenchmark>();
        }
    }
}
