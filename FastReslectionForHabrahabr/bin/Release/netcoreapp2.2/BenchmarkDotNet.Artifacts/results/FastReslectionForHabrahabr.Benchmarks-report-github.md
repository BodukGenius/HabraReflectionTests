``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 21.96 μs | 0.075 μs | 0.070 μs |  1.21 |    0.03 | 3.4790 |     - |     - |  10.72 KB |
|       FastHydration | 18.61 μs | 0.098 μs | 0.087 μs |  1.02 |    0.02 | 2.8076 |     - |     - |   8.66 KB |
|   SlowHydrationLinq | 28.43 μs | 0.567 μs | 0.630 μs |  1.56 |    0.04 | 3.9673 |     - |     - |  12.22 KB |
|       SlowHydration | 24.12 μs | 0.200 μs | 0.167 μs |  1.33 |    0.03 | 3.2959 |     - |     - |  10.16 KB |
| ManualHydrationLinq | 21.17 μs | 0.418 μs | 0.410 μs |  1.16 |    0.03 | 3.4790 |     - |     - |  10.72 KB |
|     ManualHydration | 18.20 μs | 0.343 μs | 0.381 μs |  1.00 |    0.00 | 2.8076 |     - |     - |   8.66 KB |
