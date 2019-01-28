# Result interceptors

## Feature

Intercept results of SOLR before parse in POCO

## How to

1.  Create a class that implements **IResultInterceptor**

```csharp
	public class MyInterceptor : IResultInterceptor
    {
        public void Execute(string requestHandler, ref string json) // method from interface
        {
			// some code
		}
	}
```

2.  Add instance using method **Add** in **DocumentCollection&lt;>**

```csharp
	var myInterceptor = new MyInterceptor();

	DocumentSearch<TechProduct> documentSearch; // From your DI provider
	documentSearch
		.Add(myInterceptor)
		// ...  Other settings
		.Execute();
```
