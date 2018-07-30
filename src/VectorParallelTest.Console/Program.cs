using VectorParallelTest.Common;

namespace VectorParallelTest.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var test = new AverageComputationBenchmark();

            System.Console.WriteLine(test.SimpleAverage());
            System.Console.WriteLine(test.ParallelAverage());
            System.Console.WriteLine(test.VectorizedAverage());
            System.Console.WriteLine(test.ParallelVectorizedAverage());
        }
    }
}
