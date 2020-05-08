``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 21.61 μs | 0.279 μs | 0.261 μs |  1.22 |    0.01 | 3.2654 |     - |     - |  10.09 KB |
|       FastHydration | 18.93 μs | 0.374 μs | 0.473 μs |  1.06 |    0.03 | 2.8076 |     - |     - |   8.72 KB |
|   SlowHydrationLinq | 27.40 μs | 0.348 μs | 0.308 μs |  1.55 |    0.02 | 3.7537 |     - |     - |  11.59 KB |
|       SlowHydration | 24.22 μs | 0.400 μs | 0.374 μs |  1.36 |    0.02 | 3.2959 |     - |     - |  10.22 KB |
| ManualHydrationLinq | 20.78 μs | 0.411 μs | 0.549 μs |  1.18 |    0.04 | 3.2654 |     - |     - |  10.09 KB |
|     ManualHydration | 17.72 μs | 0.086 μs | 0.077 μs |  1.00 |    0.00 | 2.8076 |     - |     - |   8.72 KB |
