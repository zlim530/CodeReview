``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.18363.1556 (1909/November2019Update/19H2)
Intel Core i5-9500 CPU 3.00GHz, 1 CPU, 6 logical and 6 physical cores
.NET SDK=5.0.404
  [Host]     : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT
  DefaultJob : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT


```
|            Method |     Mean |   Error |  StdDev |     Gen 0 | Allocated |
|------------------ |---------:|--------:|--------:|----------:|----------:|
|      LoadDataTask | 131.4 ms | 0.49 ms | 0.46 ms | 9250.0000 |     42 MB |
| LoadDataValueTask | 134.5 ms | 0.33 ms | 0.26 ms | 4750.0000 |     21 MB |
