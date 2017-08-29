# Benchmarks

Benchmarks using version 5.0.0

# SolrExpress.Benchmarks.Solr4.Search.Result.DocumentResultBenchmarks

``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4810MQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-HYXTWV : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-YXUZJX : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day

```

 |                             Method | Runtime | ElementsCount |     Mean |     Error |    StdDev |      Max |      Min |   Median |        Op/s |  Gen 0 | Allocated |
 |----------------------------------- |-------- |-------------- |---------:|----------:|----------:|---------:|---------:|---------:|------------:|-------:|----------:|
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |            **10** | **373.5 ns** | **1.7157 ns** | **1.5210 ns** | **376.3 ns** | **370.9 ns** | **373.3 ns** | **2,677,580.1** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |            10 | 335.5 ns | 1.2961 ns | 1.2124 ns | 338.6 ns | 333.7 ns | 335.5 ns | 2,980,645.2 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |           **100** | **366.3 ns** | **3.9277 ns** | **3.6740 ns** | **374.2 ns** | **361.0 ns** | **367.2 ns** | **2,730,343.3** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |           100 | 338.1 ns | 3.1060 ns | 2.9054 ns | 341.4 ns | 331.8 ns | 338.9 ns | 2,957,977.3 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |           **500** | **373.1 ns** | **1.1327 ns** | **0.9458 ns** | **375.0 ns** | **371.6 ns** | **372.8 ns** | **2,680,370.4** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |           500 | 335.2 ns | 0.8575 ns | 0.8021 ns | 337.0 ns | 333.9 ns | 335.0 ns | 2,983,660.7 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |          **1000** | **366.0 ns** | **2.3216 ns** | **1.9386 ns** | **369.9 ns** | **363.7 ns** | **365.6 ns** | **2,731,952.2** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |          1000 | 329.1 ns | 2.9435 ns | 2.7533 ns | 332.7 ns | 323.5 ns | 330.1 ns | 3,038,633.9 | 1.7667 |   5.43 KB |

 # SolrExpress.Benchmarks.Solr4.Search.Result.DocumentResultBenchmarks

 ``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4810MQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-HYXTWV : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-YXUZJX : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day

```

 |                             Method | Runtime | ElementsCount |     Mean |     Error |    StdDev |      Max |      Min |   Median |        Op/s |  Gen 0 | Allocated |
 |----------------------------------- |-------- |-------------- |---------:|----------:|----------:|---------:|---------:|---------:|------------:|-------:|----------:|
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |            **10** | **373.5 ns** | **1.7157 ns** | **1.5210 ns** | **376.3 ns** | **370.9 ns** | **373.3 ns** | **2,677,580.1** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |            10 | 335.5 ns | 1.2961 ns | 1.2124 ns | 338.6 ns | 333.7 ns | 335.5 ns | 2,980,645.2 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |           **100** | **366.3 ns** | **3.9277 ns** | **3.6740 ns** | **374.2 ns** | **361.0 ns** | **367.2 ns** | **2,730,343.3** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |           100 | 338.1 ns | 3.1060 ns | 2.9054 ns | 341.4 ns | 331.8 ns | 338.9 ns | 2,957,977.3 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |           **500** | **373.1 ns** | **1.1327 ns** | **0.9458 ns** | **375.0 ns** | **371.6 ns** | **372.8 ns** | **2,680,370.4** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |           500 | 335.2 ns | 0.8575 ns | 0.8021 ns | 337.0 ns | 333.9 ns | 335.0 ns | 2,983,660.7 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.DocumentResult** |     **Clr** |          **1000** | **366.0 ns** | **2.3216 ns** | **1.9386 ns** | **369.9 ns** | **363.7 ns** | **365.6 ns** | **2,731,952.2** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.DocumentResult |    Core |          1000 | 329.1 ns | 2.9435 ns | 2.7533 ns | 332.7 ns | 323.5 ns | 330.1 ns | 3,038,633.9 | 1.7667 |   5.43 KB |

 # SolrExpress.Benchmarks.Solr4.Search.Result.FacetsResultBenchmarks

 ``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4810MQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-HYXTWV : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-YXUZJX : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day

```

 |                           Method | Runtime | FacetTypes | ElementsCount |     Mean |     Error |    StdDev |      Max |      Min |   Median |        Op/s |  Gen 0 | Allocated |
 |--------------------------------- |-------- |----------- |-------------- |---------:|----------:|----------:|---------:|---------:|---------:|------------:|-------:|----------:|
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Field** |            **10** | **402.1 ns** | **1.7375 ns** | **1.6252 ns** | **405.1 ns** | **398.8 ns** | **402.0 ns** | **2,486,822.5** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Field |            10 | 334.5 ns | 0.5783 ns | 0.5127 ns | 335.2 ns | 333.7 ns | 334.5 ns | 2,989,715.4 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Field** |           **100** | **400.1 ns** | **1.4689 ns** | **1.3021 ns** | **402.4 ns** | **398.1 ns** | **399.9 ns** | **2,499,064.7** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Field |           100 | 329.9 ns | 3.0416 ns | 2.8451 ns | 335.0 ns | 326.7 ns | 328.8 ns | 3,031,088.3 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Field** |           **500** | **401.5 ns** | **1.1201 ns** | **0.9930 ns** | **403.8 ns** | **400.2 ns** | **401.3 ns** | **2,490,658.8** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Field |           500 | 335.2 ns | 0.6959 ns | 0.6510 ns | 336.1 ns | 334.0 ns | 335.4 ns | 2,983,016.8 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Field** |          **1000** | **368.5 ns** | **0.7250 ns** | **0.6781 ns** | **369.6 ns** | **367.4 ns** | **368.5 ns** | **2,713,831.1** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Field |          1000 | 337.2 ns | 1.0602 ns | 0.9918 ns | 338.5 ns | 335.3 ns | 337.3 ns | 2,965,750.3 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Query** |            **10** | **403.7 ns** | **7.8025 ns** | **8.3486 ns** | **426.6 ns** | **392.2 ns** | **400.5 ns** | **2,476,900.8** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Query |            10 | 337.5 ns | 1.5823 ns | 1.4027 ns | 340.7 ns | 335.5 ns | 337.4 ns | 2,962,958.4 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Query** |           **100** | **369.3 ns** | **0.5547 ns** | **0.4632 ns** | **370.3 ns** | **368.7 ns** | **369.3 ns** | **2,707,794.4** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Query |           100 | 335.0 ns | 0.9035 ns | 0.8451 ns | 336.4 ns | 333.5 ns | 334.9 ns | 2,985,419.3 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Query** |           **500** | **368.8 ns** | **0.9566 ns** | **0.8480 ns** | **370.3 ns** | **367.3 ns** | **369.1 ns** | **2,711,403.6** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Query |           500 | 345.5 ns | 5.8418 ns | 5.4644 ns | 353.0 ns | 335.3 ns | 346.9 ns | 2,894,303.6 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Query** |          **1000** | **368.4 ns** | **1.2846 ns** | **1.1388 ns** | **371.2 ns** | **366.5 ns** | **368.2 ns** | **2,714,509.8** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Query |          1000 | 332.2 ns | 3.2677 ns | 3.0566 ns | 335.5 ns | 326.7 ns | 333.1 ns | 3,010,266.6 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Range** |            **10** | **400.2 ns** | **1.5949 ns** | **1.4138 ns** | **403.4 ns** | **398.5 ns** | **399.8 ns** | **2,498,966.7** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Range |            10 | 333.4 ns | 0.9807 ns | 0.8693 ns | 334.8 ns | 331.8 ns | 333.3 ns | 2,999,008.2 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Range** |           **100** | **370.8 ns** | **1.6542 ns** | **1.5473 ns** | **374.2 ns** | **369.1 ns** | **370.3 ns** | **2,696,981.7** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Range |           100 | 335.0 ns | 0.8538 ns | 0.6173 ns | 336.1 ns | 333.6 ns | 335.0 ns | 2,985,154.2 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Range** |           **500** | **371.7 ns** | **1.1604 ns** | **1.0287 ns** | **373.5 ns** | **370.3 ns** | **371.3 ns** | **2,690,439.6** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Range |           500 | 329.3 ns | 0.8360 ns | 0.6981 ns | 330.7 ns | 328.4 ns | 329.2 ns | 3,036,974.2 | 1.7667 |   5.43 KB |
 | **Solr4.Search.Result.FacetsResult** |     **Clr** |      **Range** |          **1000** | **370.9 ns** | **0.9461 ns** | **0.8850 ns** | **372.2 ns** | **369.6 ns** | **371.0 ns** | **2,695,921.7** | **1.7772** |   **5.46 KB** |
 | Solr4.Search.Result.FacetsResult |    Core |      Range |          1000 | 339.2 ns | 1.7856 ns | 1.4911 ns | 341.4 ns | 335.5 ns | 339.6 ns | 2,948,251.2 | 1.7667 |   5.43 KB |

 # SolrExpress.Benchmarks.Solr5.Search.Result.DocumentResultBenchmarks

 ``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4810MQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-HYXTWV : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-YXUZJX : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day

```

 |                             Method | Runtime | ElementsCount |     Mean |     Error |    StdDev |      Max |      Min |   Median |        Op/s |  Gen 0 | Allocated |
 |----------------------------------- |-------- |-------------- |---------:|----------:|----------:|---------:|---------:|---------:|------------:|-------:|----------:|
 | **Solr5.Search.Result.DocumentResult** |     **Clr** |            **10** | **367.1 ns** | **2.2268 ns** | **1.9740 ns** | **369.4 ns** | **362.4 ns** | **367.7 ns** | **2,723,892.4** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.DocumentResult |    Core |            10 | 336.8 ns | 2.4878 ns | 2.2054 ns | 340.7 ns | 331.7 ns | 337.2 ns | 2,969,209.9 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.DocumentResult** |     **Clr** |           **100** | **376.1 ns** | **1.2452 ns** | **1.0398 ns** | **377.2 ns** | **373.3 ns** | **376.5 ns** | **2,658,806.4** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.DocumentResult |    Core |           100 | 335.2 ns | 4.5563 ns | 4.2620 ns | 341.3 ns | 329.0 ns | 336.4 ns | 2,983,196.4 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.DocumentResult** |     **Clr** |           **500** | **370.7 ns** | **1.0274 ns** | **0.9610 ns** | **372.6 ns** | **369.4 ns** | **370.6 ns** | **2,697,530.9** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.DocumentResult |    Core |           500 | 331.8 ns | 1.2658 ns | 0.9883 ns | 333.5 ns | 330.5 ns | 331.7 ns | 3,013,615.9 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.DocumentResult** |     **Clr** |          **1000** | **371.9 ns** | **1.1249 ns** | **0.9394 ns** | **373.5 ns** | **370.1 ns** | **371.9 ns** | **2,689,039.7** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.DocumentResult |    Core |          1000 | 332.7 ns | 0.4046 ns | 0.3159 ns | 333.1 ns | 332.0 ns | 332.8 ns | 3,005,463.0 | 1.7667 |   5.43 KB |
 
 # SolrExpress.Benchmarks.Solr5.Search.Result.FacetsResultBenchmarks

 ``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4810MQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-HYXTWV : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2102.0
  Job-YXUZJX : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day

```

 |                           Method | Runtime | FacetTypes | ElementsCount |     Mean |      Error |     StdDev |      Max |      Min |   Median |        Op/s |  Gen 0 | Allocated |
 |--------------------------------- |-------- |----------- |-------------- |---------:|-----------:|-----------:|---------:|---------:|---------:|------------:|-------:|----------:|
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Field** |            **10** | **374.9 ns** |  **3.8284 ns** |  **3.3938 ns** | **380.6 ns** | **370.7 ns** | **374.4 ns** | **2,667,300.3** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Field |            10 | 335.7 ns |  0.9083 ns |  0.7585 ns | 337.1 ns | 334.5 ns | 335.6 ns | 2,979,110.5 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Field** |           **100** | **366.1 ns** |  **1.0310 ns** |  **0.9139 ns** | **367.7 ns** | **364.5 ns** | **366.0 ns** | **2,731,585.5** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Field |           100 | 329.7 ns |  1.2964 ns |  1.0826 ns | 330.9 ns | 327.7 ns | 330.2 ns | 3,032,840.0 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Field** |           **500** | **368.5 ns** |  **1.5869 ns** |  **1.4068 ns** | **371.0 ns** | **366.1 ns** | **368.6 ns** | **2,713,481.6** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Field |           500 | 339.0 ns |  1.9642 ns |  1.8373 ns | 342.0 ns | 335.8 ns | 338.3 ns | 2,950,093.3 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Field** |          **1000** | **365.2 ns** |  **0.7738 ns** |  **0.7238 ns** | **366.7 ns** | **364.2 ns** | **365.0 ns** | **2,738,015.1** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Field |          1000 | 335.3 ns |  5.9404 ns |  5.2660 ns | 347.5 ns | 330.5 ns | 332.9 ns | 2,982,121.1 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Query** |            **10** | **370.7 ns** |  **1.3490 ns** |  **1.1958 ns** | **373.3 ns** | **368.9 ns** | **370.6 ns** | **2,697,913.5** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Query |            10 | 336.3 ns |  3.8837 ns |  3.0321 ns | 340.2 ns | 331.5 ns | 337.0 ns | 2,973,391.6 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Query** |           **100** | **366.9 ns** |  **4.3397 ns** |  **4.0593 ns** | **373.0 ns** | **361.2 ns** | **368.9 ns** | **2,725,496.3** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Query |           100 | 334.2 ns |  0.9446 ns |  0.7888 ns | 335.8 ns | 333.1 ns | 334.1 ns | 2,991,962.9 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Query** |           **500** | **368.0 ns** |  **0.7469 ns** |  **0.6621 ns** | **369.1 ns** | **367.0 ns** | **368.0 ns** | **2,717,031.7** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Query |           500 | 332.9 ns |  0.9717 ns |  0.8614 ns | 334.6 ns | 331.4 ns | 332.7 ns | 3,003,961.2 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Query** |          **1000** | **370.6 ns** |  **0.7463 ns** |  **0.6232 ns** | **371.9 ns** | **369.8 ns** | **370.6 ns** | **2,698,255.1** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Query |          1000 | 341.8 ns |  1.3511 ns |  1.2638 ns | 344.0 ns | 339.5 ns | 341.6 ns | 2,925,623.9 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Range** |            **10** | **372.5 ns** |  **1.9047 ns** |  **1.7817 ns** | **375.8 ns** | **369.9 ns** | **372.6 ns** | **2,684,280.3** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Range |            10 | 345.1 ns | 24.0875 ns | 23.6571 ns | 398.8 ns | 331.9 ns | 334.4 ns | 2,897,810.0 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Range** |           **100** | **370.9 ns** |  **1.2035 ns** |  **1.1257 ns** | **372.5 ns** | **368.6 ns** | **370.9 ns** | **2,696,129.5** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Range |           100 | 336.0 ns |  5.2818 ns |  4.4106 ns | 343.2 ns | 328.1 ns | 337.0 ns | 2,975,847.6 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Range** |           **500** | **369.4 ns** |  **1.2162 ns** |  **1.0156 ns** | **370.9 ns** | **367.9 ns** | **369.1 ns** | **2,707,127.4** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Range |           500 | 334.8 ns |  0.6337 ns |  0.5928 ns | 335.9 ns | 334.1 ns | 334.7 ns | 2,986,640.3 | 1.7667 |   5.43 KB |
 | **Solr5.Search.Result.FacetsResult** |     **Clr** |      **Range** |          **1000** | **368.8 ns** |  **0.5726 ns** |  **0.5076 ns** | **370.0 ns** | **367.9 ns** | **368.8 ns** | **2,711,134.9** | **1.7772** |   **5.46 KB** |
 | Solr5.Search.Result.FacetsResult |    Core |      Range |          1000 | 333.2 ns |  1.2391 ns |  1.1591 ns | 336.3 ns | 331.9 ns | 332.9 ns | 3,001,167.7 | 1.7667 |   5.43 KB |
