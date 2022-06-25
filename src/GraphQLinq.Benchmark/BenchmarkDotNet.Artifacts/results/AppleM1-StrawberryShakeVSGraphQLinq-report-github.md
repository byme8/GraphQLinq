``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.4 (21F79) [Darwin 21.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.301
  [Host]     : .NET 6.0.6 (6.0.622.26707), Arm64 RyuJIT
  DefaultJob : .NET 6.0.6 (6.0.622.26707), Arm64 RyuJIT


```
|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| StrawberryShake | 196.6 μs |  0.88 μs |  0.73 μs |
|      GraphQLinq | 524.3 μs | 10.48 μs | 19.68 μs |
