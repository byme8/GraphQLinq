``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.4 (21F79) [Darwin 21.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.301
  [Host]     : .NET 6.0.6 (6.0.622.26707), Arm64 RyuJIT
  DefaultJob : .NET 6.0.6 (6.0.622.26707), Arm64 RyuJIT


```
|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
|      RawRequest | 188.5 μs | 0.94 μs | 0.83 μs |
| StrawberryShake | 195.7 μs | 0.80 μs | 0.74 μs |
|      GraphQLinq | 514.2 μs | 1.41 μs | 1.25 μs |
