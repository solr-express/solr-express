using Newtonsoft.Json;
using SolrExpress.Builder;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Solr5.Search.Result;
using SolrExpress.Solr5.UnitTests;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Result
{
    public class FacetsResultTests
    {
        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet field data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult001()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facets"": {
		            ""field1"": {
			            ""buckets"": [{
					            ""val"": ""VALUE001"",
					            ""count"": 10
				            },
				            {
					            ""val"": ""VALUE002"",
					            ""count"": 20
				            }
			            ]
		            },
		            ""field2"": {
			            ""buckets"": [{
					            ""val"": ""VALUE001"",
					            ""count"": 10
				            },
				            {
					            ""val"": ""VALUE002"",
					            ""count"": 20
				            }
			            ]
		            },
		            ""field3"": {
			            ""buckets"": []
		            }
	            }
            }";

            var result = (IFacetsResult<TestFacetDocument>)new FacetsResult<TestFacetDocument>();

            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestFacetDocument>();
            var expressionBuilder = new ExpressionBuilder<TestFacetDocument>(solrExpressOptions, solrConnection);
            expressionBuilder.LoadDocument();

            var facetRange1 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facetRange1.FieldExpression = (field) => field.Field1;

            var facetRange2 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facetRange2.FieldExpression = (field) => field.Field2;

            var facetRange4 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facetRange4.FieldExpression = (field) => field.Field3;

            var searchParameters = new List<ISearchParameter>
            {
                facetRange1,
                facetRange2,
                facetRange4
            };

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(3, data.Count);
            Assert.Equal("field1", data[0].Name);
            Assert.Equal("field2", data[1].Name);
            Assert.Equal("field3", data[2].Name);

            Assert.Equal(FacetType.Field, data[0].FacetType);
            Assert.Equal(FacetType.Field, data[1].FacetType);
            Assert.Equal(FacetType.Field, data[2].FacetType);

            var facetValues1 = ((FacetItemField)data[0]).Values;
            Assert.Equal(2, facetValues1.Count());
            Assert.True(facetValues1.Any(q => q.Key.Equals("VALUE001")));
            Assert.Equal(10, facetValues1.First(q => q.Key.Equals("VALUE001")).Quantity);
            Assert.True(facetValues1.Any(q => q.Key.Equals("VALUE002")));
            Assert.Equal(20, facetValues1.First(q => q.Key.Equals("VALUE002")).Quantity);

            var facetValues2 = ((FacetItemField)data[1]).Values;
            Assert.Equal(2, facetValues2.Count());
            Assert.True(facetValues2.Any(q => q.Key.Equals("VALUE001")));
            Assert.Equal(10, facetValues2.First(q => q.Key.Equals("VALUE001")).Quantity);
            Assert.True(facetValues2.Any(q => q.Key.Equals("VALUE002")));
            Assert.Equal(20, facetValues2.First(q => q.Key.Equals("VALUE002")).Quantity);

            var facetValues3 = ((FacetItemField)data[2]).Values;
            Assert.Equal(0, facetValues3.Count());
        }

        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet query data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult002()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facets"": {
		            ""query1"": {
			            ""count"": 10
		            },
		            ""query2"": {
			            ""count"": 20
		            },
	            }
            }";

            var result = (IFacetsResult<TestFacetDocument>)new FacetsResult<TestFacetDocument>();

            var searchParameters = new List<ISearchParameter>();

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(2, data.Count);
            Assert.Equal("query1", data[0].Name);
            Assert.Equal("query2", data[1].Name);

            Assert.Equal(FacetType.Query, data[0].FacetType);
            Assert.Equal(FacetType.Query, data[1].FacetType);

            Assert.Equal(10, ((FacetItemQuery)data[0]).Quantity);
            Assert.Equal(20, ((FacetItemQuery)data[1]).Quantity);
        }

        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet range data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult003()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facets"": {
		            ""range1"": {
			            ""buckets"": [{
					            ""val"": 100,
					            ""count"": 4
				            },
				            {
					            ""val"": 350,
					            ""count"": 3
				            },
				            {
					            ""val"": 600,
					            ""count"": 1
				            },
				            {
					            ""val"": 850,
					            ""count"": 0
				            }
			            ],
			            ""before"": {
				            ""count"": 7
			            },
			            ""after"": {
				            ""count"": 1
			            }
		            },
		            ""range2"": {
			            ""buckets"": [{
					            ""val"": 4,
					            ""count"": 0
				            },
				            {
					            ""val"": 5,
					            ""count"": 1
				            },
				            {
					            ""val"": 6,
					            ""count"": 5
				            },
				            {
					            ""val"": 7,
					            ""count"": 4
				            }
			            ]
		            },
		            ""range4"": {
			            ""buckets"": [{
					            ""val"": ""1987-08-10T11:56:40.803Z"",
					            ""count"": 0
				            },
				            {
					            ""val"": ""1992-08-10T11:56:40.803Z"",
					            ""count"": 0
				            },
				            {
					            ""val"": ""1997-08-10T11:56:40.803Z"",
					            ""count"": 0
				            },
				            {
					            ""val"": ""2002-08-10T11:56:40.803Z"",
					            ""count"": 11
				            },
				            {
					            ""val"": ""2007-08-10T11:56:40.803Z"",
					            ""count"": 0
				            },
				            {
					            ""val"": ""2012-08-10T11:56:40.803Z"",
					            ""count"": 0
				            },
				            {
					            ""val"": ""2017-08-10T11:56:40.803Z"",
					            ""count"": 0
				            }
			            ]
		            }
	            }
            }";

            var result = (IFacetsResult<TestFacetDocument>)new FacetsResult<TestFacetDocument>();

            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestFacetDocument>();
            var expressionBuilder = new ExpressionBuilder<TestFacetDocument>(solrExpressOptions, solrConnection);
            expressionBuilder.LoadDocument();

            var facetRange1 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facetRange1.FieldExpression = (field) => field.Range1;
            facetRange1.Start = "100";
            facetRange1.End = "850";
            facetRange1.Gap = "250";

            var facetRange2 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facetRange2.FieldExpression = (field) => field.Range2;
            facetRange2.Start = "4";
            facetRange2.End = "7";
            facetRange2.Gap = "1";

            var facetRange4 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facetRange4.FieldExpression = (field) => field.Range4;
            facetRange4.Start = "NOW-30YEARS";
            facetRange4.End = "NOW+1DAY";
            facetRange4.Gap = "+5YEARS";

            var searchParameters = new List<ISearchParameter>
            {
                facetRange1,
                facetRange2,
                facetRange4
            };

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(4, data.Count);
            Assert.Equal("range1", data[0].Name);
            Assert.Equal("range2", data[1].Name);
            Assert.Equal("range4", data[2].Name);

            Assert.Equal(FacetType.Range, data[0].FacetType);
            Assert.Equal(FacetType.Range, data[1].FacetType);
            Assert.Equal(FacetType.Range, data[2].FacetType);

            var range1Values = ((FacetItemRange)data[0]).Values.ToList();

            Assert.Null(((FacetItemRangeValue<decimal>)range1Values[0]).MinimumValue);
            Assert.Equal(100, ((FacetItemRangeValue<decimal>)range1Values[0]).MaximumValue);
            Assert.Equal(7, ((FacetItemRangeValue<decimal>)range1Values[0]).Quantity);

            Assert.Equal(100, ((FacetItemRangeValue<decimal>)range1Values[1]).MinimumValue);
            Assert.Equal(350, ((FacetItemRangeValue<decimal>)range1Values[1]).MaximumValue);
            Assert.Equal(4, ((FacetItemRangeValue<decimal>)range1Values[1]).Quantity);

            Assert.Equal(350, ((FacetItemRangeValue<decimal>)range1Values[2]).MinimumValue);
            Assert.Equal(600, ((FacetItemRangeValue<decimal>)range1Values[2]).MaximumValue);
            Assert.Equal(3, ((FacetItemRangeValue<decimal>)range1Values[2]).Quantity);

            Assert.Equal(600, ((FacetItemRangeValue<decimal>)range1Values[3]).MinimumValue);
            Assert.Equal(850, ((FacetItemRangeValue<decimal>)range1Values[3]).MaximumValue);
            Assert.Equal(1, ((FacetItemRangeValue<decimal>)range1Values[3]).Quantity);

            Assert.Equal(850, ((FacetItemRangeValue<decimal>)range1Values[4]).MinimumValue);
            Assert.Equal(1100, ((FacetItemRangeValue<decimal>)range1Values[4]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<decimal>)range1Values[4]).Quantity);

            Assert.Equal(1100, ((FacetItemRangeValue<decimal>)range1Values[5]).MinimumValue);
            Assert.Null(((FacetItemRangeValue<decimal>)range1Values[5]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<decimal>)range1Values[5]).Quantity);

            var range2Values = ((FacetItemRange)data[1]).Values.ToList();

            Assert.Equal(4, ((FacetItemRangeValue<int>)range2Values[0]).MinimumValue);
            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[0]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[0]).Quantity);

            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[1]).MinimumValue);
            Assert.Equal(6, ((FacetItemRangeValue<int>)range2Values[1]).MaximumValue);
            Assert.Equal(1, ((FacetItemRangeValue<int>)range2Values[1]).Quantity);

            Assert.Equal(6, ((FacetItemRangeValue<int>)range2Values[2]).MinimumValue);
            Assert.Equal(7, ((FacetItemRangeValue<int>)range2Values[2]).MaximumValue);
            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[2]).Quantity);

            Assert.Equal(7, ((FacetItemRangeValue<int>)range2Values[3]).MinimumValue);
            Assert.Equal(8, ((FacetItemRangeValue<int>)range2Values[3]).MaximumValue);
            Assert.Equal(4, ((FacetItemRangeValue<int>)range2Values[3]).Quantity);
            
            var range4Values = ((FacetItemRange)data[2]).Values.ToList();

            Assert.Equal(DateTimeOffset.Parse("1992-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[0]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("1997-08-09T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[0]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[0]).Quantity);

            Assert.Equal(DateTimeOffset.Parse("1997-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[1]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("2002-08-09T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[1]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[1]).Quantity);

            Assert.Equal(DateTimeOffset.Parse("2002-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[2]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("2007-08-09T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[2]).MaximumValue);
            Assert.Equal(11, ((FacetItemRangeValue<DateTime>)range4Values[2]).Quantity);

            Assert.Equal(DateTimeOffset.Parse("2007-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[3]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("2012-08-08T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[3]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[3]).Quantity);

            Assert.Equal(DateTimeOffset.Parse("2012-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[4]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("2017-08-09T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[4]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[4]).Quantity);

            Assert.Equal(DateTimeOffset.Parse("2017-08-10T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[5]).MinimumValue);
            Assert.Equal(DateTimeOffset.Parse("2022-08-09T11:56:40.803Z").DateTime, ((FacetItemRangeValue<DateTime>)range4Values[5]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[5]).Quantity);
        }
    }
}