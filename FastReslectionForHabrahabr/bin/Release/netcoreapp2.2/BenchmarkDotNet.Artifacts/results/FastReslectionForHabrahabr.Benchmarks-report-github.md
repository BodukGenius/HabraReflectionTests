``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.720 (1903/May2019Update/19H1)
Intel Core i5-6440HQ CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|------:|-------:|------:|------:|----------:|
|   FastHydrationLinq | 22.54 μs | 0.171 μs | 0.160 μs |  1.14 | 3.8452 |     - |     - |  11.91 KB |
|       FastHydration | 19.81 μs | 0.084 μs | 0.066 μs |  1.00 | 3.0518 |     - |     - |   9.47 KB |
|   SlowHydrationLinq | 29.76 μs | 0.156 μs | 0.146 μs |  1.51 | 4.4556 |     - |     - |  13.72 KB |
|       SlowHydration | 26.34 μs | 0.202 μs | 0.189 μs |  1.33 | 3.6621 |     - |     - |  11.28 KB |
| ManualHydrationLinq | 22.75 μs | 0.114 μs | 0.106 μs |  1.15 | 3.9673 |     - |     - |  12.22 KB |
|     ManualHydration | 19.75 μs | 0.056 μs | 0.052 μs |  1.00 | 3.1738 |     - |     - |   9.78 KB |
