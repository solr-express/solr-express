using Newtonsoft.Json;
using SolrExpress.Builder;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Result
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
	            ""facet_counts"": {
		            ""facet_fields"": {
			            ""field1"": [
				            ""VALUE001"", 10,
				            ""VALUE002"", 20
			            ],
			            ""field2"": [
				            ""VALUE001"", 10,
				            ""VALUE002"", 20
			            ],
			            ""field3"": [
			            ]
		            }
	            }
            }";

            var result = (IFacetsResult<TestDocument>)new FacetsResult<TestDocument>();

            var searchParameters = new List<ISearchParameter>();

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
	            ""facet_counts"": {
		            ""facet_queries"": {
			            ""query1"": 10,
			            ""query2"": 20
		            }
	            }
            }";

            var result = (IFacetsResult<TestDocument>)new FacetsResult<TestDocument>();

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
	            ""facet_counts"": {
		            ""facet_ranges"": {
			            ""range1"": {
				            ""counts"": [
					            ""200.0"",
					            4,
					            ""450.0"",
					            2,
					            ""700.0"",
					            0,
					            ""950.0"",
					            0
				            ],
				            ""gap"": 250,
				            ""start"": 200,
				            ""end"": 1200,
				            ""before"": 55,
				            ""after"": 1
			            },
			            ""range2"": {
				            ""counts"": [
					            ""2"",
					            0,
					            ""3"",
					            0,
					            ""4"",
					            0,
					            ""5"",
					            1,
					            ""6"",
					            5,
					            ""7"",
					            4,
					            ""8"",
					            0,
					            ""9"",
					            0
				            ],
				            ""gap"": 1,
				            ""start"": 2,
				            ""end"": 10,
				            ""before"": 49,
				            ""after"": 2
			            },
			            ""range3"": {
				            ""counts"": [
					            ""2"",
					            0,
					            ""3"",
					            0
				            ],
				            ""gap"": 1,
				            ""start"": 2,
				            ""end"": 10
			            },
			            ""range4"": {
				            ""counts"": [
					            ""1987-08-09T20:55:04.076Z"",
					            0,
					            ""1992-08-09T20:55:04.076Z"",
					            0,
					            ""1997-08-09T20:55:04.076Z"",
					            0,
					            ""2002-08-09T20:55:04.076Z"",
					            11,
					            ""2007-08-09T20:55:04.076Z"",
					            0,
					            ""2012-08-09T20:55:04.076Z"",
					            0,
					            ""2017-08-09T20:55:04.076Z"",
					            0
				            ],
				            ""gap"": ""+5YEARS"",
				            ""start"": ""1987-08-09T20:55:04.076Z"",
				            ""end"": ""2022-08-09T20:55:04.076Z""
			            }
		            }
	            }
            }";

            var result = (IFacetsResult<TestRangeDocument>)new FacetsResult<TestRangeDocument>();

            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestRangeDocument>();
            var expressionBuilder = new ExpressionBuilder<TestRangeDocument>(solrExpressOptions, solrConnection);
            expressionBuilder.LoadDocument();

            var facetRange1 = (IFacetRangeParameter<TestRangeDocument>)new FacetRangeParameter<TestRangeDocument>(expressionBuilder);
            facetRange1.FieldExpression = (field) => field.Range1;

            var facetRange2 = (IFacetRangeParameter<TestRangeDocument>)new FacetRangeParameter<TestRangeDocument>(expressionBuilder);
            facetRange2.FieldExpression = (field) => field.Range2;

            var facetRange3 = (IFacetRangeParameter<TestRangeDocument>)new FacetRangeParameter<TestRangeDocument>(expressionBuilder);
            facetRange3.FieldExpression = (field) => field.Range3;

            var facetRange4 = (IFacetRangeParameter<TestRangeDocument>)new FacetRangeParameter<TestRangeDocument>(expressionBuilder);
            facetRange4.FieldExpression = (field) => field.Range4;

            var searchParameters = new List<ISearchParameter>
            {
                facetRange1,
                facetRange2,
                facetRange3,
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
            Assert.Equal("range3", data[2].Name);
            Assert.Equal("range4", data[3].Name);

            Assert.Equal(FacetType.Range, data[0].FacetType);
            Assert.Equal(FacetType.Range, data[1].FacetType);
            Assert.Equal(FacetType.Range, data[2].FacetType);
            Assert.Equal(FacetType.Range, data[3].FacetType);

            var range1Values = ((FacetItemRange)data[0]).Values.ToList();

            Assert.Null(((FacetItemRangeValue<decimal>)range1Values[0]).MinimumValue);
            Assert.Equal(200, ((FacetItemRangeValue<decimal>)range1Values[0]).MaximumValue);
            Assert.Equal(55, ((FacetItemRangeValue<decimal>)range1Values[0]).Quantity);

            Assert.Equal(200, ((FacetItemRangeValue<decimal>)range1Values[1]).MinimumValue);
            Assert.Equal(450, ((FacetItemRangeValue<decimal>)range1Values[1]).MaximumValue);
            Assert.Equal(4, ((FacetItemRangeValue<decimal>)range1Values[1]).Quantity);

            Assert.Equal(450, ((FacetItemRangeValue<decimal>)range1Values[2]).MinimumValue);
            Assert.Equal(700, ((FacetItemRangeValue<decimal>)range1Values[2]).MaximumValue);
            Assert.Equal(2, ((FacetItemRangeValue<decimal>)range1Values[2]).Quantity);

            Assert.Equal(700, ((FacetItemRangeValue<decimal>)range1Values[3]).MinimumValue);
            Assert.Equal(950, ((FacetItemRangeValue<decimal>)range1Values[3]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<decimal>)range1Values[3]).Quantity);

            Assert.Equal(950, ((FacetItemRangeValue<decimal>)range1Values[4]).MinimumValue);
            Assert.Equal(1200, ((FacetItemRangeValue<decimal>)range1Values[4]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<decimal>)range1Values[4]).Quantity);

            Assert.Equal(1200, ((FacetItemRangeValue<decimal>)range1Values[5]).MinimumValue);
            Assert.Null(((FacetItemRangeValue<decimal>)range1Values[5]).MaximumValue);
            Assert.Equal(1, ((FacetItemRangeValue<decimal>)range1Values[5]).Quantity);

            var range2Values = ((FacetItemRange)data[1]).Values.ToList();

            Assert.Null(((FacetItemRangeValue<int>)range2Values[0]).MinimumValue);
            Assert.Equal(2, ((FacetItemRangeValue<int>)range2Values[0]).MaximumValue);
            Assert.Equal(49, ((FacetItemRangeValue<int>)range2Values[0]).Quantity);

            Assert.Equal(2, ((FacetItemRangeValue<int>)range2Values[1]).MinimumValue);
            Assert.Equal(3, ((FacetItemRangeValue<int>)range2Values[1]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[1]).Quantity);

            Assert.Equal(3, ((FacetItemRangeValue<int>)range2Values[2]).MinimumValue);
            Assert.Equal(4, ((FacetItemRangeValue<int>)range2Values[2]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[2]).Quantity);

            Assert.Equal(4, ((FacetItemRangeValue<int>)range2Values[3]).MinimumValue);
            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[3]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[3]).Quantity);

            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[4]).MinimumValue);
            Assert.Equal(6, ((FacetItemRangeValue<int>)range2Values[4]).MaximumValue);
            Assert.Equal(1, ((FacetItemRangeValue<int>)range2Values[4]).Quantity);

            Assert.Equal(6, ((FacetItemRangeValue<int>)range2Values[5]).MinimumValue);
            Assert.Equal(7, ((FacetItemRangeValue<int>)range2Values[5]).MaximumValue);
            Assert.Equal(5, ((FacetItemRangeValue<int>)range2Values[5]).Quantity);

            Assert.Equal(7, ((FacetItemRangeValue<int>)range2Values[6]).MinimumValue);
            Assert.Equal(8, ((FacetItemRangeValue<int>)range2Values[6]).MaximumValue);
            Assert.Equal(4, ((FacetItemRangeValue<int>)range2Values[6]).Quantity);

            Assert.Equal(8, ((FacetItemRangeValue<int>)range2Values[7]).MinimumValue);
            Assert.Equal(9, ((FacetItemRangeValue<int>)range2Values[7]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[7]).Quantity);

            Assert.Equal(9, ((FacetItemRangeValue<int>)range2Values[8]).MinimumValue);
            Assert.Equal(10, ((FacetItemRangeValue<int>)range2Values[8]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range2Values[8]).Quantity);

            Assert.Equal(10, ((FacetItemRangeValue<int>)range2Values[9]).MinimumValue);
            Assert.Null(((FacetItemRangeValue<int>)range2Values[9]).MaximumValue);
            Assert.Equal(2, ((FacetItemRangeValue<int>)range2Values[9]).Quantity);

            var range3Values = ((FacetItemRange)data[2]).Values.ToList();

            Assert.Equal(2, ((FacetItemRangeValue<int>)range3Values[0]).MinimumValue);
            Assert.Equal(3, ((FacetItemRangeValue<int>)range3Values[0]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range3Values[0]).Quantity);

            Assert.Equal(3, ((FacetItemRangeValue<int>)range3Values[1]).MinimumValue);
            Assert.Equal(4, ((FacetItemRangeValue<int>)range3Values[1]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<int>)range3Values[1]).Quantity);

            var range4Values = ((FacetItemRange)data[3]).Values.ToList();

            Assert.Equal(DateTime.Parse("1987-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[0]).MinimumValue);
            Assert.Equal(DateTime.Parse("1987-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[0]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[0]).Quantity);

            Assert.Equal(DateTime.Parse("1992-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[1]).MinimumValue);
            Assert.Equal(DateTime.Parse("1992-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[1]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[1]).Quantity);

            Assert.Equal(DateTime.Parse("1997-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[2]).MinimumValue);
            Assert.Equal(DateTime.Parse("1997-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[2]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[2]).Quantity);

            Assert.Equal(DateTime.Parse("2002-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[3]).MinimumValue);
            Assert.Equal(DateTime.Parse("2002-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[3]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[3]).Quantity);

            Assert.Equal(DateTime.Parse("2007-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[4]).MinimumValue);
            Assert.Equal(DateTime.Parse("2007-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[4]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[4]).Quantity);

            Assert.Equal(DateTime.Parse("2012-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[5]).MinimumValue);
            Assert.Equal(DateTime.Parse("2012-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[5]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[5]).Quantity);

            Assert.Equal(DateTime.Parse("2017-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[6]).MinimumValue);
            Assert.Equal(DateTime.Parse("2017-08-09T20:55:04.076Z"), ((FacetItemRangeValue<DateTime>)range4Values[6]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[6]).Quantity);
        }
    }
}