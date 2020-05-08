``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 23.09 μs | 0.169 μs | 0.158 μs |  1.19 |    0.02 | 3.8452 |     - |     - |  11.84 KB |
|       FastHydration | 22.34 μs | 0.442 μs | 0.726 μs |  1.15 |    0.04 | 3.1738 |     - |     - |   9.78 KB |
|   SlowHydrationLinq | 30.38 μs | 0.347 μs | 0.324 μs |  1.57 |    0.02 | 4.3335 |     - |     - |  13.34 KB |
|       SlowHydration | 25.81 μs | 0.396 μs | 0.370 μs |  1.33 |    0.02 | 3.6621 |     - |     - |  11.28 KB |
| ManualHydrationLinq | 22.67 μs | 0.355 μs | 0.332 μs |  1.17 |    0.02 | 3.8452 |     - |     - |  11.84 KB |
|     ManualHydration | 19.37 μs | 0.177 μs | 0.166 μs |  1.00 |    0.00 | 3.1738 |     - |     - |   9.78 KB |
