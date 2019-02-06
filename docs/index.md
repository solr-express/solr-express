# Solr Express

Documentation on getting started with SolrExpress is available at [getting started](http://solr-express.readthedocs.io/en/stable/tutorials/getting-started).

Tutorials is available at [tutorials](http://solr-express.readthedocs.io/en/stable/tutorials).

And release notes is available at [release notes](http://solr-express.readthedocs.io/en/stable/about/release-notes).

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/b7831786d85b4d78ab4e81ae976de0f4)](https://app.codacy.com/app/diego-l-brum/solr-express?utm_source=github.com&utm_medium=referral&utm_content=solr-express/solr-express&utm_campaign=Badge_Grade_Settings)
[![Travis build status](https://img.shields.io/travis/solr-express/solr-express.svg?label=travis-ci&branch=dev&style=flat-square)](https://travis-ci.org/solr-express/solr-express.svg/branches)

![.Net 4.0](https://img.shields.io/badge/.Net_4.0-Full_Compatibility-green.svg?style=flat-square)
![.Net 4.5](https://img.shields.io/badge/.Net_4.5-Full_Compatibility-green.svg?style=flat-square)
![.Net Core](https://img.shields.io/badge/.Net_Core-Full_Compatibility-green.svg?style=flat-square)

![Solr 4+](https://img.shields.io/badge/Solr_4.+-Full_Compatibility-green.svg?style=flat-square)
![Solr 5.3+](https://img.shields.io/badge/Solr_5.3+-Full_Compatibility-green.svg?style=flat-square)
![Solr 6+](https://img.shields.io/badge/Solr_6.+-Features_created_in_Solr_5.3_works_well-orange.svg?style=flat-square)

## Overview

A simple and lightweight query .NET library for Solr, in a controlled, buildable and fail fast way.

## Providers

The source for Solr4 and Solr5 providers are included in this project.

| Provider | Package name        | Stable (`master` branch)                                                                                                                          |
| -------- | ------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| Solr4    | `SolrExpress.Solr4` | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.Solr4.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.Solr4/) |
| Solr5    | `SolrExpress.Solr5` | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.Solr5.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.Solr5/) |

## Dependency Injectors Containers

The source for Autofac, CoreClr, Ninject and Simple Injector containers are included in this project.

| Provider         | Package name                    | Stable (`master` branch)                                                                                                                                                  |
| ---------------- | ------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Autofac          | `SolrExpress.DI.Autofac`        | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.DI.Autofac.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.DI.Autofac/)               |
| Native .Net Core | `SolrExpress.DI.CoreClr`        | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.DI.CoreClr.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.DI.CoreClr/)               |
| Ninject          | `SolrExpress.DI.Ninject`        | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.DI.Ninject.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.DI.Ninject/)               |
| Simple Injector  | `SolrExpress.DI.SimpleInjector` | [![NuGet](https://img.shields.io/nuget/v/SolrExpress.DI.SimpleInjector.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SolrExpress.DI.SimpleInjector/) |
