# [3.1.2] - 2016-07-30
## Enhancement
* Add default parameters (#125)
* Create unit test to test validations methods (#129)
* Organize changelogs in CHANGELOG.md file (#133)
* Change projects dependencies (#136)

# [3.1.1] - 2016-07-19

## Bug fix
* Create mincount using solr field name rather than POCO property name (#128)
* In sort validation, must use "index" property rather than "stored" property (#127)
* Need encode parameters in FacetRange (#123)

## Enhancement
* Change all properties in SolrExpress.Core.Query.ParameterValue.* classes to public {get; private set;} (#130)
* Use List< IParameter > parameters to calculate gaps and discovery facet types (#46)
* Recode Core.Query.ParameterValue.Range<> (#126)
* Use min.count = 1 (#50)

# [3.1.0] - 2016-07-12

## Bug fix
* Wrong default namespace in xprojs (#121)
* Create wrong parameter when use BoostType.Boost (#117)

## Enhancement
* Ignore list (#118, #120)
* Change base version to 4.0 and add support to 4.5 rather than 4.5.1 (#119)
* Rename class Statistic to Info (#116)
* Implements random sort (#110)

# [3.0.0] - 2016-07-07

## Enhancement
* Support to .Net Core 1.0 (#109)
* Interceptors (Query and Result) and Parameters in global form (#114)
* Create handler parameter to avoid string in Execute method (#115)

## Notes
* All projects are signed by default (no more *.Signed packages)

# [2.1.0] 2016-05-13

## Bug fix
1. In Solr 5.5, when 2 sorts parameters are added, a bad format is created and Solr don't process the request (#101)
2. Description of exception InvalidParameterTypeException is bad formatted (#100)

## Enhancement
1. Extensions (#99, #103)
2. Atomic update (#106)
3. Simple interceptions (#104, #105)
3. Boost parameter (#102)
4. Exception description (#98)

# [2.0.0] - 2016-05-05

## Bug fix
* Fix hyperlink to samples in readme.md (#93)

## Enhancement
* Code cleanup and reorganization
* Improves in DI
* Atomic update (#94)

# [1.2.0.1] - 2015-01-06

## Bug fix
* NuGet mistakes (#86)
* Unit test fix (#89)

## Enhancement
* Rename ParameterValue classes removing "Value" suffix (#90)
* Create SpatialFilterParamaterValue (#91)
* Docs updat (#85)
* Create a better way to discovery field type in facet ranges (#53)
* Create FreeParameter (#83)
* ThrowHelper (#88)
* Create option to choose request handler (#82)
* Fluent language (#87)

# [1.1.0.2] - 2015-12-15

NuGet mistakes

# [1.1.0.1] - 2015-12-10

NuGet mistakes

# [1.1.0] - 2015-12-10

## Bug fix
- Wrong query when use MultiValue and SolrQueryConditionType (#76)

## Changes
- Change namespace of class IDocument (#78)
- Migrate Fluent Api to core project (#66)
- StatisticResultBuilder must result a class (#77)
- Update to C# 6.0 (#38)

## Enhancement
- Signed package enhancement (#65)
- Globalization (#42)

# [1.0.01] - 2015-08-27

All Is Said And Done :)

Read the documentation and if you have some question or find some error, please, contact me