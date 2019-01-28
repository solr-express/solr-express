# Benchmarks

Benchmarks using version 3.1.2

## SolrExpress.Core.Benchmarks.Query.SolrQueryableBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=SolrQueryableBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median          | StdDev         | Mean            | StdError      | StdDev         | Op/s        | Min             | Q1              | Median          | Q3              | Max             |
| ----------- | ----------- | ------- | ------------- | --------------- | -------------- | --------------- | ------------- | -------------- | ----------- | --------------- | --------------- | --------------- | --------------- | --------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **121.7155 us** | **5.3290 us**  | **122.9521 us** | **1.1916 us** | **5.3290 us**  | **8133.25** | **116.6130 us** | **119.0479 us** | **121.7155 us** | **125.9128 us** | **135.5022 us** |
| Execute     | Core        | Core    | 10            | 115.7788 us     | 2.0300 us      | 115.7669 us     | 0.4539 us     | 2.0300 us      | 8638.05     | 112.1497 us     | 115.0539 us     | 115.7788 us     | 116.7584 us     | 121.2453 us     |
| **Execute** | **Classic** | **Clr** | **100**       | **128.0522 us** | **9.3506 us**  | **131.4490 us** | **1.0587 us** | **9.3506 us**  | **7607.51** | **120.6117 us** | **123.9243 us** | **128.0522 us** | **137.7625 us** | **156.9488 us** |
| Execute     | Core        | Core    | 100           | 119.0098 us     | 6.4147 us      | 120.4858 us     | 1.3376 us     | 6.4147 us      | 8299.73     | 114.9955 us     | 117.8739 us     | 119.0098 us     | 120.1299 us     | 145.2057 us     |
| **Execute** | **Classic** | **Clr** | **500**       | **146.7207 us** | **12.7707 us** | **152.5020 us** | **1.7220 us** | **12.7707 us** | **6557.29** | **141.1870 us** | **144.8435 us** | **146.7207 us** | **156.3456 us** | **206.3309 us** |
| Execute     | Core        | Core    | 500           | 137.1667 us     | 3.2036 us      | 138.0903 us     | 0.7164 us     | 3.2036 us      | 7241.64     | 135.5310 us     | 136.5177 us     | 137.1667 us     | 137.8864 us     | 148.8503 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **168.0634 us** | **6.0566 us**  | **169.7174 us** | **1.2363 us** | **6.0566 us**  | **5892.15** | **160.7786 us** | **166.4793 us** | **168.0634 us** | **171.0969 us** | **183.1140 us** |
| Execute     | Core        | Core    | 1000          | 160.9440 us     | 4.7132 us      | 161.8092 us     | 1.0539 us     | 4.7132 us      | 6180.12     | 155.9909 us     | 159.4137 us     | 160.9440 us     | 163.0667 us     | 176.1440 us     |

## SolrExpress.Benchmarks.Solr4.Query.ParameterContainerBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=ParameterContainerBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median            | StdDev          | Mean              | StdError       | StdDev          | Op/s         | Min               | Q1                | Median            | Q3                | Max               |
| ----------- | ----------- | ------- | ------------- | ----------------- | --------------- | ----------------- | -------------- | --------------- | ------------ | ----------------- | ----------------- | ----------------- | ----------------- | ----------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **38.8562 us**    | **1.5766 us**   | **39.1054 us**    | **0.3525 us**  | **1.5766 us**   | **25571.93** | **37.2363 us**    | **37.9277 us**    | **38.8562 us**    | **39.5374 us**    | **44.6696 us**    |
| Execute     | Core        | Core    | 10            | 28.7275 us        | 0.8763 us       | 29.0050 us        | 0.1719 us      | 0.8763 us       | 34476.82     | 28.1278 us        | 28.4740 us        | 28.7275 us        | 29.1017 us        | 31.9762 us        |
| **Execute** | **Classic** | **Clr** | **100**       | **411.4630 us**   | **15.8311 us**  | **415.9684 us**   | **3.5399 us**  | **15.8311 us**  | **2404.03**  | **402.6728 us**   | **406.6122 us**   | **411.4630 us**   | **418.1349 us**   | **471.1278 us**   |
| Execute     | Core        | Core    | 100           | 341.2841 us       | 10.9070 us      | 344.1328 us       | 2.4389 us      | 10.9070 us      | 2905.86      | 337.0357 us       | 340.6224 us       | 341.2841 us       | 342.2050 us       | 388.2402 us       |
| **Execute** | **Classic** | **Clr** | **500**       | **2,314.0114 us** | **99.3688 us**  | **2,356.1617 us** | **20.2836 us** | **99.3688 us**  | **424.42**   | **2,272.0504 us** | **2,295.3126 us** | **2,314.0114 us** | **2,360.2838 us** | **2,635.0474 us** |
| Execute     | Core        | Core    | 500           | 1,902.3723 us     | 48.4587 us      | 1,900.3294 us     | 10.8357 us     | 48.4587 us      | 526.22       | 1,842.4942 us     | 1,852.7664 us     | 1,902.3723 us     | 1,936.5468 us     | 1,989.9914 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **5,346.8117 us** | **484.1726 us** | **5,465.0481 us** | **54.1321 us** | **484.1726 us** | **182.98**   | **4,712.0251 us** | **5,210.4859 us** | **5,346.8117 us** | **5,684.6650 us** | **6,888.0312 us** |
| Execute     | Core        | Core    | 1000          | 4,281.2719 us     | 144.0476 us     | 4,355.4550 us     | 28.2500 us     | 144.0476 us     | 229.6        | 4,230.2170 us     | 4,254.7135 us     | 4,281.2719 us     | 4,426.8590 us     | 4,726.5672 us     |

## SolrExpress.Benchmarks.Solr4.Query.Result.DocumentResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=DocumentResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median         | StdDev        | Mean           | StdError      | StdDev        | Op/s       | Min            | Q1             | Median         | Q3             | Max            |
| ----------- | ----------- | ------- | ------------- | -------------- | ------------- | -------------- | ------------- | ------------- | ---------- | -------------- | -------------- | -------------- | -------------- | -------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **5.8369 ms**  | **0.1640 ms** | **5.8860 ms**  | **0.0350 ms** | **0.1640 ms** | **169.89** | **5.7466 ms**  | **5.8021 ms**  | **5.8369 ms**  | **5.8852 ms**  | **6.4309 ms**  |
| Execute     | Core        | Core    | 10            | 7.0295 ms      | 0.1377 ms     | 7.0542 ms      | 0.0308 ms     | 0.1377 ms     | 141.76     | 6.8581 ms      | 6.9735 ms      | 7.0295 ms      | 7.0698 ms      | 7.4722 ms      |
| **Execute** | **Classic** | **Clr** | **100**       | **7.5265 ms**  | **0.2552 ms** | **7.6429 ms**  | **0.0521 ms** | **0.2552 ms** | **130.84** | **7.4253 ms**  | **7.4575 ms**  | **7.5265 ms**  | **7.8123 ms**  | **8.2310 ms**  |
| Execute     | Core        | Core    | 100           | 8.6110 ms      | 0.0982 ms     | 8.6329 ms      | 0.0220 ms     | 0.0982 ms     | 115.84     | 8.5094 ms      | 8.5708 ms      | 8.6110 ms      | 8.6767 ms      | 8.9473 ms      |
| **Execute** | **Classic** | **Clr** | **500**       | **15.6905 ms** | **0.2478 ms** | **15.7360 ms** | **0.0554 ms** | **0.2478 ms** | **63.55**  | **15.3717 ms** | **15.5937 ms** | **15.6905 ms** | **15.7763 ms** | **16.3918 ms** |
| Execute     | Core        | Core    | 500           | 16.6792 ms     | 0.6985 ms     | 16.9823 ms     | 0.1370 ms     | 0.6985 ms     | 58.88      | 16.4271 ms     | 16.5274 ms     | 16.6792 ms     | 17.2021 ms     | 19.2977 ms     |
| **Execute** | **Classic** | **Clr** | **1000**      | **24.7011 ms** | **0.5617 ms** | **24.5930 ms** | **0.1256 ms** | **0.5617 ms** | **40.66**  | **23.6521 ms** | **24.0656 ms** | **24.7011 ms** | **24.8633 ms** | **25.9396 ms** |
| Execute     | Core        | Core    | 1000          | 26.3526 ms     | 0.3649 ms     | 26.2965 ms     | 0.0816 ms     | 0.3649 ms     | 38.03      | 25.5416 ms     | 26.0299 ms     | 26.3526 ms     | 26.5113 ms     | 26.9871 ms     |

## SolrExpress.Benchmarks.Solr4.Query.Result.FacetFieldResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetFieldResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median            | StdDev          | Mean              | StdError       | StdDev          | Op/s         | Min               | Q1                | Median            | Q3                | Max               |
| ----------- | ----------- | ------- | ------------- | ----------------- | --------------- | ----------------- | -------------- | --------------- | ------------ | ----------------- | ----------------- | ----------------- | ----------------- | ----------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **52.3435 us**    | **1.4899 us**   | **51.8324 us**    | **0.3107 us**  | **1.4899 us**   | **19292.96** | **49.3078 us**    | **50.0586 us**    | **52.3435 us**    | **52.6916 us**    | **54.8681 us**    |
| Execute     | Core        | Core    | 10            | 53.8367 us        | 2.8114 us       | 55.4060 us        | 0.6286 us      | 2.8114 us       | 18048.6      | 52.5448 us        | 53.1856 us        | 53.8367 us        | 57.4348 us        | 62.6588 us        |
| **Execute** | **Classic** | **Clr** | **100**       | **495.3866 us**   | **36.1160 us**  | **497.4967 us**   | **4.5867 us**  | **36.1160 us**  | **2010.06**  | **446.8246 us**   | **470.8987 us**   | **495.3866 us**   | **512.9499 us**   | **603.9462 us**   |
| Execute     | Core        | Core    | 100           | 493.1521 us       | 12.6685 us      | 496.4594 us       | 2.7645 us      | 12.6685 us      | 2014.26      | 483.4276 us       | 487.0273 us       | 493.1521 us       | 504.0430 us       | 530.2119 us       |
| **Execute** | **Classic** | **Clr** | **500**       | **2,857.8526 us** | **81.0684 us**  | **2,868.3807 us** | **18.1275 us** | **81.0684 us**  | **348.63**   | **2,791.1702 us** | **2,806.5786 us** | **2,857.8526 us** | **2,892.8261 us** | **3,103.2212 us** |
| Execute     | Core        | Core    | 500           | 2,806.9609 us     | 89.3366 us      | 2,836.0991 us     | 19.9763 us     | 89.3366 us      | 352.6        | 2,746.5559 us     | 2,770.7861 us     | 2,806.9609 us     | 2,857.8855 us     | 3,064.8270 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **5,736.3241 us** | **247.9246 us** | **5,837.7898 us** | **50.6074 us** | **247.9246 us** | **171.3**    | **5,623.4181 us** | **5,680.8791 us** | **5,736.3241 us** | **5,912.6421 us** | **6,633.7133 us** |
| Execute     | Core        | Core    | 1000          | 6,197.7910 us     | 374.5898 us     | 6,227.3120 us     | 83.7608 us     | 374.5898 us     | 160.58       | 5,861.6130 us     | 5,884.9125 us     | 6,197.7910 us     | 6,425.2842 us     | 7,119.7999 us     |

## SolrExpress.Benchmarks.Solr4.Query.Result.FacetQueryResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetQueryResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median          | StdDev         | Mean            | StdError      | StdDev         | Op/s          | Min             | Q1              | Median          | Q3              | Max             |
| ----------- | ----------- | ------- | ------------- | --------------- | -------------- | --------------- | ------------- | -------------- | ------------- | --------------- | --------------- | --------------- | --------------- | --------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **5.1792 us**   | **0.0796 us**  | **5.1856 us**   | **0.0178 us** | **0.0796 us**  | **192840.72** | **5.0600 us**   | **5.1497 us**   | **5.1792 us**   | **5.2144 us**   | **5.3736 us**   |
| Execute     | Core        | Core    | 10            | 5.5441 us       | 0.2213 us      | 5.6228 us       | 0.0397 us     | 0.2213 us      | 177846.55     | 5.4231 us       | 5.4703 us       | 5.5441 us       | 5.6614 us       | 6.1936 us       |
| **Execute** | **Classic** | **Clr** | **100**       | **39.2552 us**  | **0.9695 us**  | **39.6208 us**  | **0.2168 us** | **0.9695 us**  | **25239.27**  | **38.5973 us**  | **38.9011 us**  | **39.2552 us**  | **40.3270 us**  | **41.9485 us**  |
| Execute     | Core        | Core    | 100           | 40.5511 us      | 0.5019 us      | 40.5775 us      | 0.1122 us     | 0.5019 us      | 24644.23      | 39.6535 us      | 40.2797 us      | 40.5511 us      | 40.6899 us      | 41.9299 us      |
| **Execute** | **Classic** | **Clr** | **500**       | **195.2083 us** | **2.2992 us**  | **195.3760 us** | **0.5141 us** | **2.2992 us**  | **5118.34**   | **190.5789 us** | **194.0804 us** | **195.2083 us** | **196.4620 us** | **200.1367 us** |
| Execute     | Core        | Core    | 500           | 213.1791 us     | 14.9984 us     | 210.5604 us     | 2.8864 us     | 14.9984 us     | 4749.23       | 189.1064 us     | 196.2131 us     | 213.1791 us     | 219.1084 us     | 245.5476 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **385.4511 us** | **25.7640 us** | **393.5499 us** | **5.7610 us** | **25.7640 us** | **2540.97**   | **380.7744 us** | **383.5007 us** | **385.4511 us** | **389.3458 us** | **491.8627 us** |
| Execute     | Core        | Core    | 1000          | 393.0937 us     | 15.5670 us     | 398.9731 us     | 3.1776 us     | 15.5670 us     | 2506.43       | 383.4943 us     | 390.9076 us     | 393.0937 us     | 399.8473 us     | 444.6209 us     |

## SolrExpress.Benchmarks.Solr4.Query.Result.FacetRangeResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetRangeResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median            | StdDev         | Mean              | StdError      | StdDev         | Op/s        | Min               | Q1                | Median            | Q3                | Max               |
| ----------- | ----------- | ------- | ------------- | ----------------- | -------------- | ----------------- | ------------- | -------------- | ----------- | ----------------- | ----------------- | ----------------- | ----------------- | ----------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **21.7964 us**    | **0.1640 us**  | **21.8579 us**    | **0.0367 us** | **0.1640 us**  | **45750.1** | **21.6644 us**    | **21.7596 us**    | **21.7964 us**    | **21.9244 us**    | **22.3594 us**    |
| Execute     | Core        | Core    | 10            | 23.2023 us        | 0.2288 us      | 23.2145 us        | 0.0512 us     | 0.2288 us      | 43076.58    | 22.7915 us        | 23.0647 us        | 23.2023 us        | 23.3311 us        | 23.7069 us        |
| **Execute** | **Classic** | **Clr** | **100**       | **177.8087 us**   | **6.5773 us**  | **180.6777 us**   | **1.4353 us** | **6.5773 us**  | **5534.72** | **175.2981 us**   | **177.1102 us**   | **177.8087 us**   | **181.4481 us**   | **202.1896 us**   |
| Execute     | Core        | Core    | 100           | 184.5398 us       | 3.4710 us      | 185.4802 us       | 0.7761 us     | 3.4710 us      | 5391.41     | 183.6443 us       | 184.0658 us       | 184.5398 us       | 185.5060 us       | 199.7185 us       |
| **Execute** | **Classic** | **Clr** | **500**       | **905.6891 us**   | **56.8138 us** | **922.6862 us**   | **8.0347 us** | **56.8138 us** | **1083.79** | **868.7675 us**   | **877.6050 us**   | **905.6891 us**   | **940.7335 us**   | **1,072.4916 us** |
| Execute     | Core        | Core    | 500           | 900.3231 us       | 11.9994 us     | 905.5578 us       | 2.6832 us     | 11.9994 us     | 1104.29     | 895.9445 us       | 898.2899 us       | 900.3231 us       | 907.0923 us       | 937.7251 us       |
| **Execute** | **Classic** | **Clr** | **1000**      | **2,051.7682 us** | **27.6333 us** | **2,061.0847 us** | **6.1790 us** | **27.6333 us** | **485.18**  | **2,041.3355 us** | **2,045.5810 us** | **2,051.7682 us** | **2,059.0578 us** | **2,150.4672 us** |
| Execute     | Core        | Core    | 1000          | 2,202.3096 us     | 108.2005 us    | 2,242.5859 us     | 22.0863 us    | 108.2005 us    | 445.91      | 2,151.3378 us     | 2,170.0896 us     | 2,202.3096 us     | 2,273.0885 us     | 2,575.3354 us     |

## SolrExpress.Benchmarks.Solr5.Query.ParameterContainerBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [AttachedDebugger] [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=ParameterContainerBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median            | StdDev          | Mean              | StdError       | StdDev          | Op/s         | Min               | Q1                | Median            | Q3                | Max               |
| ----------- | ----------- | ------- | ------------- | ----------------- | --------------- | ----------------- | -------------- | --------------- | ------------ | ----------------- | ----------------- | ----------------- | ----------------- | ----------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **39.5761 us**    | **0.4694 us**   | **39.7251 us**    | **0.1050 us**  | **0.4694 us**   | **25173.02** | **39.0938 us**    | **39.4475 us**    | **39.5761 us**    | **39.8026 us**    | **41.0114 us**    |
| Execute     | Core        | Core    | 10            | 28.9582 us        | 2.0364 us       | 29.4352 us        | 0.2721 us      | 2.0364 us       | 33972.97     | 26.2861 us        | 27.9864 us        | 28.9582 us        | 29.9871 us        | 35.7889 us        |
| **Execute** | **Classic** | **Clr** | **100**       | **428.1895 us**   | **28.3624 us**  | **430.8201 us**   | **3.3196 us**  | **28.3624 us**  | **2321.15**  | **390.9543 us**   | **405.6106 us**   | **428.1895 us**   | **445.2420 us**   | **520.0892 us**   |
| Execute     | Core        | Core    | 100           | 374.0706 us       | 33.7806 us      | 387.9445 us       | 3.9811 us      | 33.7806 us      | 2577.69      | 357.1890 us       | 365.0074 us       | 374.0706 us       | 400.9977 us       | 493.6311 us       |
| **Execute** | **Classic** | **Clr** | **500**       | **2,501.2818 us** | **215.9063 us** | **2,551.4268 us** | **26.5762 us** | **215.9063 us** | **391.94**   | **2,235.2684 us** | **2,381.0302 us** | **2,501.2818 us** | **2,657.8886 us** | **3,307.4665 us** |
| Execute     | Core        | Core    | 500           | 1,892.6828 us     | 48.5579 us      | 1,903.3505 us     | 10.8579 us     | 48.5579 us      | 525.39       | 1,837.0287 us     | 1,870.7171 us     | 1,892.6828 us     | 1,919.4266 us     | 2,027.4736 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **5,411.1973 us** | **246.1758 us** | **5,502.9295 us** | **41.6113 us** | **246.1758 us** | **181.72**   | **5,226.2623 us** | **5,346.1330 us** | **5,411.1973 us** | **5,611.2471 us** | **6,107.2652 us** |
| Execute     | Core        | Core    | 1000          | 4,368.1379 us     | 172.6428 us     | 4,422.8665 us     | 35.2406 us     | 172.6428 us     | 226.1        | 4,238.7682 us     | 4,290.8826 us     | 4,368.1379 us     | 4,518.6822 us     | 4,941.5086 us     |

## SolrExpress.Benchmarks.Solr5.Query.Result.DocumentResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=DocumentResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median         | StdDev        | Mean           | StdError      | StdDev        | Op/s       | Min            | Q1             | Median         | Q3             | Max            |
| ----------- | ----------- | ------- | ------------- | -------------- | ------------- | -------------- | ------------- | ------------- | ---------- | -------------- | -------------- | -------------- | -------------- | -------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **6.0980 ms**  | **0.4122 ms** | **6.1208 ms**  | **0.0541 ms** | **0.4122 ms** | **163.38** | **5.4489 ms**  | **5.7472 ms**  | **6.0980 ms**  | **6.3600 ms**  | **7.2326 ms**  |
| Execute     | Core        | Core    | 10            | 7.1930 ms      | 0.5299 ms     | 7.1642 ms      | 0.0696 ms     | 0.5299 ms     | 139.58     | 6.4776 ms      | 6.6091 ms      | 7.1930 ms      | 7.5040 ms      | 8.4413 ms      |
| **Execute** | **Classic** | **Clr** | **100**       | **7.5782 ms**  | **0.5903 ms** | **7.7117 ms**  | **0.0716 ms** | **0.5903 ms** | **129.67** | **6.9359 ms**  | **7.2188 ms**  | **7.5782 ms**  | **8.1660 ms**  | **9.2196 ms**  |
| Execute     | Core        | Core    | 100           | 8.7773 ms      | 0.5211 ms     | 9.0212 ms      | 0.0752 ms     | 0.5211 ms     | 110.85     | 8.4129 ms      | 8.5894 ms      | 8.7773 ms      | 9.4392 ms      | 10.4374 ms     |
| **Execute** | **Classic** | **Clr** | **500**       | **15.5437 ms** | **0.5857 ms** | **15.7251 ms** | **0.1310 ms** | **0.5857 ms** | **63.59**  | **15.1329 ms** | **15.3842 ms** | **15.5437 ms** | **15.8234 ms** | **17.6456 ms** |
| Execute     | Core        | Core    | 500           | 16.7019 ms     | 0.5672 ms     | 16.7990 ms     | 0.1112 ms     | 0.5672 ms     | 59.53      | 16.1401 ms     | 16.4867 ms     | 16.7019 ms     | 16.8648 ms     | 18.3274 ms     |
| **Execute** | **Classic** | **Clr** | **1000**      | **24.4055 ms** | **0.5555 ms** | **24.4568 ms** | **0.1242 ms** | **0.5555 ms** | **40.89**  | **23.7774 ms** | **24.0049 ms** | **24.4055 ms** | **24.6790 ms** | **25.8430 ms** |
| Execute     | Core        | Core    | 1000          | 25.9870 ms     | 0.5298 ms     | 26.1086 ms     | 0.1185 ms     | 0.5298 ms     | 38.3       | 25.4834 ms     | 25.7881 ms     | 25.9870 ms     | 26.2892 ms     | 27.2904 ms     |

## SolrExpress.Benchmarks.Solr5.Query.Result.FacetFieldResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetFieldResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median             | StdDev          | Mean               | StdError       | StdDev          | Op/s         | Min                | Q1                 | Median             | Q3                 | Max                |
| ----------- | ----------- | ------- | ------------- | ------------------ | --------------- | ------------------ | -------------- | --------------- | ------------ | ------------------ | ------------------ | ------------------ | ------------------ | ------------------ |
| **Execute** | **Classic** | **Clr** | **10**        | **93.9219 us**     | **1.0462 us**   | **93.8986 us**     | **0.2339 us**  | **1.0462 us**   | **10649.78** | **91.8399 us**     | **93.1840 us**     | **93.9219 us**     | **94.4462 us**     | **96.4595 us**     |
| Execute     | Core        | Core    | 10            | 96.3069 us         | 1.1270 us       | 96.5666 us         | 0.2520 us      | 1.1270 us       | 10355.55     | 94.7621 us         | 96.0767 us         | 96.3069 us         | 96.9928 us         | 99.8261 us         |
| **Execute** | **Classic** | **Clr** | **100**       | **908.2636 us**    | **10.6545 us**  | **909.8426 us**    | **2.3824 us**  | **10.6545 us**  | **1099.09**  | **895.8142 us**    | **903.7024 us**    | **908.2636 us**    | **913.1871 us**    | **948.0088 us**    |
| Execute     | Core        | Core    | 100           | 925.3380 us        | 12.7705 us      | 928.3654 us        | 2.8556 us      | 12.7705 us      | 1077.16      | 909.3711 us        | 921.5922 us        | 925.3380 us        | 932.2367 us        | 970.8157 us        |
| **Execute** | **Classic** | **Clr** | **500**       | **5,038.3690 us**  | **49.2105 us**  | **5,044.4711 us**  | **11.0038 us** | **49.2105 us**  | **198.24**   | **4,964.9111 us**  | **5,012.3004 us**  | **5,038.3690 us**  | **5,061.1301 us**  | **5,175.9005 us**  |
| Execute     | Core        | Core    | 500           | 5,246.2856 us      | 307.9297 us     | 5,293.6620 us      | 68.8552 us     | 307.9297 us     | 188.91       | 4,961.4345 us      | 5,028.4318 us      | 5,246.2856 us      | 5,495.6234 us      | 6,022.3780 us      |
| **Execute** | **Classic** | **Clr** | **1000**      | **10,539.2560 us** | **157.0263 us** | **10,543.7406 us** | **35.1122 us** | **157.0263 us** | **94.84**    | **10,324.6297 us** | **10,449.8269 us** | **10,539.2560 us** | **10,597.4645 us** | **10,939.0178 us** |
| Execute     | Core        | Core    | 1000          | 10,443.7272 us     | 124.1916 us     | 10,441.1280 us     | 27.7701 us     | 124.1916 us     | 95.78        | 10,251.0658 us     | 10,347.0127 us     | 10,443.7272 us     | 10,518.5454 us     | 10,747.3873 us     |

## SolrExpress.Benchmarks.Solr5.Query.Result.FacetQueryResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetQueryResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median          | StdDev         | Mean            | StdError      | StdDev         | Op/s          | Min             | Q1              | Median          | Q3              | Max             |
| ----------- | ----------- | ------- | ------------- | --------------- | -------------- | --------------- | ------------- | -------------- | ------------- | --------------- | --------------- | --------------- | --------------- | --------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **6.4238 us**   | **0.8548 us**  | **6.7184 us**   | **0.1303 us** | **0.8548 us**  | **148844.13** | **6.2640 us**   | **6.3389 us**   | **6.4238 us**   | **6.8457 us**   | **11.7041 us**  |
| Execute     | Core        | Core    | 10            | 6.5963 us       | 0.1599 us      | 6.6710 us       | 0.0358 us     | 0.1599 us      | 149903.21     | 6.4998 us       | 6.5483 us       | 6.5963 us       | 6.8587 us       | 6.9462 us       |
| **Execute** | **Classic** | **Clr** | **100**       | **57.5865 us**  | **1.1378 us**  | **57.8623 us**  | **0.2544 us** | **1.1378 us**  | **17282.41**  | **56.3873 us**  | **56.9408 us**  | **57.5865 us**  | **58.6119 us**  | **60.7717 us**  |
| Execute     | Core        | Core    | 100           | 58.0472 us      | 1.7388 us      | 58.6190 us      | 0.3888 us     | 1.7388 us      | 17059.33      | 57.1919 us      | 57.7477 us      | 58.0472 us      | 58.4821 us      | 63.9816 us      |
| **Execute** | **Classic** | **Clr** | **500**       | **283.0029 us** | **8.3105 us**  | **286.5455 us** | **1.8583 us** | **8.3105 us**  | **3489.85**   | **276.9738 us** | **281.8451 us** | **283.0029 us** | **289.9110 us** | **307.9846 us** |
| Execute     | Core        | Core    | 500           | 288.9278 us     | 15.1009 us     | 295.9186 us     | 3.3767 us     | 15.1009 us     | 3379.31       | 285.5942 us     | 286.8092 us     | 288.9278 us     | 300.2772 us     | 335.0554 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **570.6931 us** | **15.3125 us** | **574.0943 us** | **3.4240 us** | **15.3125 us** | **1741.87**   | **560.2855 us** | **563.0830 us** | **570.6931 us** | **573.3295 us** | **616.1708 us** |
| Execute     | Core        | Core    | 1000          | 606.3703 us     | 52.3751 us     | 622.1681 us     | 11.7114 us    | 52.3751 us     | 1607.28       | 561.7353 us     | 576.0837 us     | 606.3703 us     | 667.4272 us     | 727.5032 us     |

## SolrExpress.Benchmarks.Solr5.Query.Result.FacetRangeResultBenchmarks

```ini
Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4810MQ CPU 2.80GHz, ProcessorCount=8
Frequency=2728070 ticks, Resolution=366.5595 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=FacetRangeResultBenchmarks  Mode=Throughput  GarbageCollection=Concurrent Workstation  

Time unit definitions
ns=Nanoseconds
us=Microseconds
ms=Millisecond
s=Second
m=Minute
h=Hour
d=Day
```

| Method      | Toolchain   | Runtime | ElementsCount | Median          | StdDev         | Mean            | StdError      | StdDev         | Op/s          | Min             | Q1              | Median          | Q3              | Max             |
| ----------- | ----------- | ------- | ------------- | --------------- | -------------- | --------------- | ------------- | -------------- | ------------- | --------------- | --------------- | --------------- | --------------- | --------------- |
| **Execute** | **Classic** | **Clr** | **10**        | **8.1300 us**   | **0.0610 us**  | **8.1388 us**   | **0.0136 us** | **0.0610 us**  | **122867.72** | **8.0558 us**   | **8.1049 us**   | **8.1300 us**   | **8.1581 us**   | **8.3311 us**   |
| Execute     | Core        | Core    | 10            | 8.9789 us       | 0.3248 us      | 8.9431 us       | 0.0726 us     | 0.3248 us      | 111818.06     | 8.5501 us       | 8.6342 us       | 8.9789 us       | 9.1263 us       | 9.7614 us       |
| **Execute** | **Classic** | **Clr** | **100**       | **56.6817 us**  | **2.6865 us**  | **57.7341 us**  | **0.4607 us** | **2.6865 us**  | **17320.8**   | **54.8658 us**  | **55.9582 us**  | **56.6817 us**  | **58.1902 us**  | **66.2780 us**  |
| Execute     | Core        | Core    | 100           | 62.2771 us      | 3.3774 us      | 62.2453 us      | 0.7552 us     | 3.3774 us      | 16065.47      | 58.3454 us      | 59.3768 us      | 62.2771 us      | 64.0903 us      | 70.9588 us      |
| **Execute** | **Classic** | **Clr** | **500**       | **274.2968 us** | **4.2738 us**  | **274.9518 us** | **0.9557 us** | **4.2738 us**  | **3637**      | **268.8363 us** | **271.9737 us** | **274.2968 us** | **277.1934 us** | **287.2599 us** |
| Execute     | Core        | Core    | 500           | 302.2096 us     | 12.6340 us     | 303.2778 us     | 2.8250 us     | 12.6340 us     | 3297.31       | 288.2574 us     | 291.3948 us     | 302.2096 us     | 311.4089 us     | 331.1128 us     |
| **Execute** | **Classic** | **Clr** | **1000**      | **561.5251 us** | **13.1116 us** | **567.6597 us** | **2.9318 us** | **13.1116 us** | **1761.62**   | **556.2469 us** | **559.5005 us** | **561.5251 us** | **574.4349 us** | **609.4217 us** |
| Execute     | Core        | Core    | 1000          | 605.0416 us     | 22.9797 us     | 613.4650 us     | 4.4224 us     | 22.9797 us     | 1630.08       | 592.6888 us     | 598.3225 us     | 605.0416 us     | 617.9313 us     | 669.4823 us     |
