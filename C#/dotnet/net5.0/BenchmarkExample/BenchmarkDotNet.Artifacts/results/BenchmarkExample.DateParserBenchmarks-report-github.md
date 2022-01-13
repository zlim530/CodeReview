``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.18363.1556 (1909/November2019Update/19H2)
Intel Core i5-9500 CPU 3.00GHz, 1 CPU, 6 logical and 6 physical cores
.NET SDK=5.0.404
  [Host]     : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT
  DefaultJob : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT


```
|                              Method |         Mean |      Error |     StdDev | Ratio | RatioSD | Rank |  Gen 0 | Allocated |
|------------------------------------ |-------------:|-----------:|-----------:|------:|--------:|-----:|-------:|----------:|
| GetYearFromSpanWithManualConversion |     9.033 ns |  0.0410 ns |  0.0363 ns |  0.03 |    0.00 |    1 |      - |         - |
|                     GetYearFromSpan |    20.178 ns |  0.1224 ns |  0.1145 ns |  0.07 |    0.00 |    2 |      - |         - |
|                    GetYearFromSplit |   101.299 ns |  1.0531 ns |  0.9335 ns |  0.35 |    0.00 |    3 | 0.0340 |     160 B |
|                 GetYearFromDateTime |   286.667 ns |  2.6655 ns |  2.3629 ns |  1.00 |    0.00 |    4 |      - |         - |
|                GetYearFromSubstring | 4,855.670 ns | 64.4224 ns | 60.2607 ns | 16.91 |    0.24 |    5 |      - |      32 B |
