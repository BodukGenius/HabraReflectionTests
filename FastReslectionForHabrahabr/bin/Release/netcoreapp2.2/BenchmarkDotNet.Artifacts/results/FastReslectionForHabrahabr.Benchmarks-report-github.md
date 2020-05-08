``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 21.90 μs | 0.367 μs | 0.343 μs |  1.23 |    0.02 | 3.4790 |     - |     - |  10.72 KB |
|       FastHydration | 18.85 μs | 0.368 μs | 0.465 μs |  1.06 |    0.03 | 2.8076 |     - |     - |   8.66 KB |
|   SlowHydrationLinq | 27.09 μs | 0.150 μs | 0.133 μs |  1.52 |    0.01 | 3.9673 |     - |     - |  12.22 KB |
|       SlowHydration | 24.41 μs | 0.484 μs | 0.453 μs |  1.37 |    0.03 | 3.2959 |     - |     - |  10.16 KB |
| ManualHydrationLinq | 21.20 μs | 0.423 μs | 0.470 μs |  1.19 |    0.03 | 3.4790 |     - |     - |  10.72 KB |
|     ManualHydration | 17.88 μs | 0.090 μs | 0.084 μs |  1.00 |    0.00 | 2.8076 |     - |     - |   8.66 KB |
