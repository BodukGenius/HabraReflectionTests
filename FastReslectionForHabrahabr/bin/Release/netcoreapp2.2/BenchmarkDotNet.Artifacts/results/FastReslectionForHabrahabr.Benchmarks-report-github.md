``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|--------:|------:|------:|----------:|
|   FastHydrationLinq | 52.48 μs | 0.287 μs | 0.254 μs |  1.46 |    0.02 | 13.2446 |     - |     - |  40.78 KB |
|       FastHydration | 36.20 μs | 0.220 μs | 0.195 μs |  1.01 |    0.01 |  3.2959 |     - |     - |  10.22 KB |
|   SlowHydrationLinq | 60.16 μs | 0.429 μs | 0.402 μs |  1.67 |    0.02 | 13.7329 |     - |     - |  42.28 KB |
|       SlowHydration | 41.00 μs | 0.259 μs | 0.242 μs |  1.14 |    0.01 |  3.7842 |     - |     - |  11.72 KB |
| ManualHydrationLinq | 52.27 μs | 0.676 μs | 0.632 μs |  1.45 |    0.02 | 13.2446 |     - |     - |  40.78 KB |
|     ManualHydration | 35.95 μs | 0.389 μs | 0.345 μs |  1.00 |    0.00 |  3.2959 |     - |     - |  10.22 KB |
