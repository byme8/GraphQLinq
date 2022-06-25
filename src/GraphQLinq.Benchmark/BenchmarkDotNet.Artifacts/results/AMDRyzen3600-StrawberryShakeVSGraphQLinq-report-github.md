``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.400-preview.22301.10
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| StrawberryShake | 140.1 μs | 0.97 μs | 0.91 μs |
|      GraphQLinq | 548.4 μs | 4.15 μs | 3.89 μs |
