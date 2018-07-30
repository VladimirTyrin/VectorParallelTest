# Average test

Compute average of 256 * 1024 * 1024 (2^28) floats (1 GB of data)

Result on i7-6820HK (4 cores, 8 threads)

```
                    Method |  Job | Runtime |      Mean |     Error |    StdDev | Scaled |
-------------------------- |----- |-------- |----------:|----------:|----------:|-------:|
             SimpleAverage |  Clr |     Clr | 300.49 ms | 4.0043 ms | 3.7456 ms |   1.00 |
           ParallelAverage |  Clr |     Clr |  43.32 ms | 0.8991 ms | 1.5022 ms |   0.14 |
         VectorizedAverage |  Clr |     Clr |  53.31 ms | 1.0086 ms | 1.2756 ms |   0.18 |
 ParallelVectorizedAverage |  Clr |     Clr |  36.71 ms | 0.4342 ms | 0.4061 ms |   0.12 |
                           |      |         |           |           |           |        |
             SimpleAverage | Core |    Core | 311.89 ms | 6.1827 ms | 8.6672 ms |   1.00 |
           ParallelAverage | Core |    Core |  42.44 ms | 0.8409 ms | 1.0934 ms |   0.14 |
         VectorizedAverage | Core |    Core |  54.53 ms | 1.0659 ms | 1.4590 ms |   0.17 |
 ParallelVectorizedAverage | Core |    Core |  36.14 ms | 0.3844 ms | 0.3596 ms |   0.12 |
```