# Average test

Compute average of 256 * 1024 * 1024 (2^28) floats (1 GB of data)

Sequential methods:

* Simple cycle
* 128 bit SIMD cycle
* Maximum available SIMD cycle

Parallel methods: sequential methods with #threads = #cores parallelism

## Usage

Run `benchmark.sh` from the repo root

## Test results



Result on i7-6820HK (4 cores, 8 threads, `Vector<float>.Count == 8`)

```
                    Method |      Mean |     Error |    StdDev |    Median | Scaled |
-------------------------- |----------:|----------:|----------:|----------:|-------:|
             SimpleAverage | 299.23 ms | 3.5739 ms | 3.3431 ms | 298.19 ms |   1.00 |
           ParallelAverage |  41.61 ms | 0.9273 ms | 0.7743 ms |  41.47 ms |   0.14 |
            Vector4Average | 133.14 ms | 1.5449 ms | 1.4451 ms | 133.51 ms |   0.44 |
    ParallelVector4Average |  37.54 ms | 0.2755 ms | 0.2442 ms |  37.51 ms |   0.13 |
         VectorizedAverage |  56.89 ms | 1.2904 ms | 3.2136 ms |  55.19 ms |   0.19 |
 ParallelVectorizedAverage |  36.03 ms | 0.3677 ms | 0.3440 ms |  35.94 ms |   0.12 |
```