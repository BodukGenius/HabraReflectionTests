``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|--------:|------:|------:|----------:|
|   FastHydrationLinq | 61.87 μs | 1.163 μs | 1.088 μs |  1.70 |    0.03 | 14.4043 |     - |     - |  44.28 KB |
|       FastHydration | 42.22 μs | 0.841 μs | 0.826 μs |  1.16 |    0.03 |  4.4556 |     - |     - |  13.72 KB |
|   SlowHydrationLinq | 65.07 μs | 0.997 μs | 0.883 μs |  1.79 |    0.04 | 14.6484 |     - |     - |  45.09 KB |
|       SlowHydration | 43.30 μs | 0.349 μs | 0.310 μs |  1.19 |    0.02 |  4.6997 |     - |     - |  14.53 KB |
| ManualHydrationLinq | 53.69 μs | 0.575 μs | 0.480 μs |  1.48 |    0.02 | 13.2446 |     - |     - |  40.78 KB |
|     ManualHydration | 36.42 μs | 0.480 μs | 0.449 μs |  1.00 |    0.00 |  3.2959 |     - |     - |  10.22 KB |
