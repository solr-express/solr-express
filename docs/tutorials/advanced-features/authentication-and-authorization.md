# Authentication and Authorization

## Feature

Use authentication to connect with SOLR server

## How to use basic authentication

1.  You need activate this feature in SOLR server, to do this, follow steps in **[SOLR wiki](https://cwiki.apache.org/confluence/display/solr/Authentication+and+Authorization+Plugins)**
2.  Change your **SolrExpressOptions** and set **SecurityOptions**

```csharp
	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options =>
            {
				options.Security.AuthenticationType = AuthenticationType.Basic;
				options.Security.BasicAuthentication.Password = "<YOUR PASSWORD>";
				options.Security.BasicAuthentication.UserName = "<YOUR USER NAME>";
            });
```

## How to use bearer token authentication

If you use a proxy to use SOLR and need to use Bearer Token

1.  Change your **SolrExpressOptions** and set **SecurityOptions**

```csharp
	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options =>
            {
				options.Security.AuthenticationType = AuthenticationType.BearerToken;
				options.Security.BearerToken.Token = "<YOUR TOKEN>";
            });
```

## How to use custom authentication

If you use a proxy to use SOLR and need to use a custom authentication mechanism

1.  Create a class that implements **ICustomSolrConnectionAuthenticationSettings**

2.  Do your stuffs

3.  Change your **SolrExpressOptions** and set **SecurityOptions**

```csharp
	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseCustomConnectionAuthentication<YOUR_CLASS>()
			.UseOptions(options =>
            {
				options.Security.AuthenticationType = AuthenticationType.Custom;
            });
```