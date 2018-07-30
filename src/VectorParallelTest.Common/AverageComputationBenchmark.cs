﻿using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace VectorParallelTest.Common
{
    [CoreJob]
    public class AverageComputationBenchmark
    {
        private readonly float[] _data;
        private const int DataSize = 256 * 1024 * 1024;

        public AverageComputationBenchmark()
        {
            _data = GenerateRandomArray(DataSize);
        }

        [Benchmark(Baseline = true)]
        public double SimpleAverage()
        {
            return SimpleRangeSum(_data, 0, DataSize) / DataSize;
        }

        [Benchmark]
        public double ParallelAverage()
        {
            return ParallelAverage(SimpleRangeSum);
        }

        [Benchmark]
        public double Vector4Average()
        {
            return Vector4RangeSum(_data, 0, DataSize) / DataSize;
        }

        [Benchmark]
        public double ParallelVector4Average()
        {
            return ParallelAverage(Vector4RangeSum);
        }

        [Benchmark]
        public double VectorizedAverage()
        {
            return VectorRangeSum(_data, 0, DataSize) / DataSize;
        }

        [Benchmark]
        public double ParallelVectorizedAverage()
        {
            return ParallelAverage(VectorRangeSum);
        }

        private double ParallelAverage(Func<float[], int, int, double> rangeSumMethod)
        {
            var parts = Environment.ProcessorCount;
            var partSize = DataSize / parts;
            var averages = new double[parts];

            Parallel.For(0, parts, part =>
            {
                var lowerBound = partSize * part;
                var upperBound = lowerBound + partSize;
                averages[part] = rangeSumMethod(_data, lowerBound, upperBound) / partSize;
            });

            return averages.Average();
        }

        private static double VectorRangeSum(float[] array, int lowerInclusive, int upperExclusize)
        {
            var count = upperExclusize - lowerInclusive;
            var vectorSize = Vector<float>.Count;
            var vectorCount = count / vectorSize;
            var sum = new Vector<float>();
            for (var i = 0; i < vectorCount; ++i)
            {
                sum += new Vector<float>(array, lowerInclusive + i * vectorSize);
            }

            var totalSum = 0d;
            for (var i = 0; i < vectorSize; ++i)
            {
                totalSum += sum[i];
            }

            return totalSum;
        }

        private static double Vector4RangeSum(float[] array, int lowerInclusive, int upperExclusize)
        {
            var count = upperExclusize - lowerInclusive;
            var vectorCount = count / 4;
            var sum = new Vector4();
            for (var i = 0; i < vectorCount; ++i)
            {
                var offset = lowerInclusive + i * 4;
                sum += new Vector4(array[offset], array[offset + 1], array[offset + 2], array[offset + 3]);
            }

            return sum.X + sum.Y + sum.Z + sum.W;
        }

        private static double SimpleRangeSum(float[] array, int lowerInclusive, int upperExclusize)
        {
            var sum = 0d;
            for (var i = lowerInclusive; i < upperExclusize; ++i)
            {
                sum += array[i];
            }
            return sum;
        }

        private static float[] GenerateRandomArray(int length)
        {
            var random = new Random();
            var array = new float[length];
            for (var i = 0; i < length; i++)
            {
                array[i] = (float)random.NextDouble();
            }

            return array;
        }
    }
}
