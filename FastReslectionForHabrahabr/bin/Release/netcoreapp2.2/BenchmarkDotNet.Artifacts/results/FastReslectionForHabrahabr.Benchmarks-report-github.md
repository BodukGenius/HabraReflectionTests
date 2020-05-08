``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 21.34 μs | 0.284 μs | 0.266 μs |  1.18 |    0.03 | 3.6011 |     - |     - |  11.09 KB |
|       FastHydration | 18.68 μs | 0.080 μs | 0.071 μs |  1.03 |    0.02 | 2.8076 |     - |     - |   8.66 KB |
|   SlowHydrationLinq | 27.47 μs | 0.482 μs | 0.451 μs |  1.52 |    0.04 | 4.0894 |     - |     - |  12.59 KB |
|       SlowHydration | 24.62 μs | 0.475 μs | 0.547 μs |  1.37 |    0.04 | 3.2959 |     - |     - |  10.16 KB |
| ManualHydrationLinq | 20.13 μs | 0.107 μs | 0.100 μs |  1.11 |    0.02 | 3.6011 |     - |     - |  11.09 KB |
|     ManualHydration | 18.10 μs | 0.347 μs | 0.341 μs |  1.00 |    0.00 | 2.8076 |     - |     - |   8.66 KB |
