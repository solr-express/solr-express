using Newtonsoft.Json;
using SolrExpress.Builder;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Solr5.Search.Result;
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
		            ""count"": 37,
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

            var facet1 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet1.FieldExpression = (field) => field.Field1;

            var facet2 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet2.FieldExpression = (field) => field.Field2;

            var facet3 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet3.FieldExpression = (field) => field.Field3;

            var searchParameters = new List<ISearchParameter>
            {
                facet1,
                facet2,
                facet3
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
		            ""count"": 37,
		            ""query1"": {
			            ""count"": 10
		            },
		            ""query2"": {
			            ""count"": 20
		            },
	            }
            }";

            var result = (IFacetsResult<TestFacetDocument>)new FacetsResult<TestFacetDocument>();

            var facet1 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facet1.AliasName = "query1";

            var facet2 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facet2.AliasName = "query2";

            var searchParameters = new List<ISearchParameter>
            {
                facet1,
                facet2
            };

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
		            ""count"": 37,
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

            var facet1 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facet1.FieldExpression = (field) => field.Range1;
            facet1.Start = "100";
            facet1.End = "850";
            facet1.Gap = "250";

            var facet2 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facet2.FieldExpression = (field) => field.Range2;
            facet2.Start = "4";
            facet2.End = "7";
            facet2.Gap = "1";

            var facet3 = (IFacetRangeParameter<TestFacetDocument>)new FacetRangeParameter<TestFacetDocument>(expressionBuilder);
            facet3.FieldExpression = (field) => field.Range4;
            facet3.Start = "NOW-30YEARS";
            facet3.End = "NOW+1DAY";
            facet3.Gap = "+5YEARS";

            var searchParameters = new List<ISearchParameter>
            {
                facet1,
                facet2,
                facet3
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
            Assert.Equal(1, ((FacetItemRangeValue<decimal>)range1Values[5]).Quantity);

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

            Assert.Equal(DateTime.Parse("1987-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[0]).MinimumValue);
            Assert.Equal(DateTime.Parse("1992-08-08T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[0]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[0]).Quantity);

            Assert.Equal(DateTime.Parse("1992-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[1]).MinimumValue);
            Assert.Equal(DateTime.Parse("1997-08-09T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[1]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[1]).Quantity);

            Assert.Equal(DateTime.Parse("1997-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[2]).MinimumValue);
            Assert.Equal(DateTime.Parse("2002-08-09T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[2]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[2]).Quantity);

            Assert.Equal(DateTime.Parse("2002-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[3]).MinimumValue);
            Assert.Equal(DateTime.Parse("2007-08-09T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[3]).MaximumValue);
            Assert.Equal(11, ((FacetItemRangeValue<DateTime>)range4Values[3]).Quantity);

            Assert.Equal(DateTime.Parse("2007-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[4]).MinimumValue);
            Assert.Equal(DateTime.Parse("2012-08-08T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[4]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[4]).Quantity);

            Assert.Equal(DateTime.Parse("2012-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[5]).MinimumValue);
            Assert.Equal(DateTime.Parse("2017-08-09T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[5]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[5]).Quantity);

            Assert.Equal(DateTime.Parse("2017-08-10T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[6]).MinimumValue);
            Assert.Equal(DateTime.Parse("2022-08-09T11:56:40.803Z").ToUniversalTime(), ((FacetItemRangeValue<DateTime>)range4Values[6]).MaximumValue);
            Assert.Equal(0, ((FacetItemRangeValue<DateTime>)range4Values[6]).Quantity);
        }

        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet field and subfacet field data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult004()
        {
            // Arrange
            var jsonPlainText = @"
            {
                ""facets"": {
                    ""count"": 51,
                    ""field1"": {
                        ""buckets"": [
                            {
                                ""val"": ""ROOT.VALUE001"",
                                ""count"": 10,
                                ""field2"": {
                                    ""buckets"": [
                                        {
                                            ""val"": ""CHILD001.VALUE001"",
                                            ""count"": 5
                                        },
                                        {
                                            ""val"": ""CHILD001.VALUE002"",
                                            ""count"": 5
                                        }
                                    ]
                                }
                            },
                            {
                                ""val"": ""ROOT.VALUE002"",
                                ""count"": 20,
                                ""field2"": {
                                    ""buckets"": [
                                        {
                                            ""val"": ""CHILD002.VALUE001"",
                                            ""count"": 20,
                                            ""field3"": {
                                                ""buckets"": [
                                                    {
                                                        ""val"": ""GRANCHILD002.VALUE001"",
                                                        ""count"": 15
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
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

            var facet3 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet3.FieldExpression = (field) => field.Field3;

            var facet2 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet2.FieldExpression = (field) => field.Field2;
            facet2.Facets = new List<IFacetParameter> { facet3 };

            var facet1 = (IFacetFieldParameter<TestFacetDocument>)new FacetFieldParameter<TestFacetDocument>(expressionBuilder);
            facet1.FieldExpression = (field) => field.Field1;
            facet1.Facets = new List<IFacetParameter> { facet2 };

            var searchParameters = new List<ISearchParameter>
            {
                facet1
            };

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("field1", data[0].Name);
            Assert.Equal(FacetType.Field, data[0].FacetType);

            var facetValues1 = ((FacetItemField)data[0]).Values;

            Assert.Equal(2, facetValues1.Count());

            var root1 = facetValues1.First(q => q.Key.Equals("ROOT.VALUE001"));
            var root2 = facetValues1.First(q => q.Key.Equals("ROOT.VALUE002"));
            Assert.NotNull(root1);
            Assert.NotNull(root2);
            Assert.Equal(10, root1.Quantity);
            Assert.Equal(20, root2.Quantity);
            Assert.NotNull(root1.Facets);
            Assert.NotNull(root2.Facets);
            Assert.Equal(1, root1.Facets.Count());
            Assert.Equal(1, root2.Facets.Count());

            Assert.Equal("field2", root1.Facets.ToList()[0].Name);
            Assert.Equal(FacetType.Field, root1.Facets.ToList()[0].FacetType);
            Assert.Equal("field2", root2.Facets.ToList()[0].Name);
            Assert.Equal(FacetType.Field, root2.Facets.ToList()[0].FacetType);
            var facetRootValues1 = ((FacetItemField)root1.Facets.ToList()[0]).Values;
            var facetRootValues2 = ((FacetItemField)root2.Facets.ToList()[0]).Values;
            Assert.Equal(2, facetRootValues1.Count());
            Assert.Equal(1, facetRootValues2.Count());

            var child1_1 = facetRootValues1.FirstOrDefault(q => q.Key.Equals("CHILD001.VALUE001"));
            var child1_2 = facetRootValues1.FirstOrDefault(q => q.Key.Equals("CHILD001.VALUE001"));
            Assert.NotNull(child1_1);
            Assert.NotNull(child1_2);
            Assert.Equal(5, child1_1.Quantity);
            Assert.Equal(5, child1_2.Quantity);

            var child2_1 = facetRootValues2.FirstOrDefault(q => q.Key.Equals("CHILD002.VALUE001"));
            Assert.NotNull(child2_1);
            Assert.Equal(20, child2_1.Quantity);

            var granchild2_1 = (FacetItemField)child2_1.Facets.FirstOrDefault();
            Assert.NotNull(granchild2_1);
            Assert.Equal(FacetType.Field, granchild2_1.FacetType);
            Assert.Equal("field3", granchild2_1.Name);
            Assert.NotEmpty(granchild2_1.Values);
            Assert.Equal(1, granchild2_1.Values.Count());
            Assert.Equal("GRANCHILD002.VALUE001", granchild2_1.Values.ToList()[0].Key);
            Assert.Equal(15, granchild2_1.Values.ToList()[0].Quantity);
        }

        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet query and subfacet field data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult005()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facets"": {
		            ""count"": 37,
		            ""queryA"": {
			            ""count"": 20,
		                ""queryA1"": {
			                ""count"": 10
		                },
		                ""queryA2"": {
			                ""count"": 10,
		                    ""queryA21"": {
			                    ""count"": 5
		                    }
		                }
		            },
		            ""queryB"": {
			            ""count"": 30,
		                ""queryB1"": {
			                ""count"": 5
		                }
		            }
	            }
            }";

            var result = (IFacetsResult<TestFacetDocument>)new FacetsResult<TestFacetDocument>();

            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestFacetDocument>();
            var expressionBuilder = new ExpressionBuilder<TestFacetDocument>(solrExpressOptions, solrConnection);
            expressionBuilder.LoadDocument();

            var facetA1 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetA1.AliasName = "queryA1";

            var facetA21 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetA21.AliasName = "queryA21";

            var facetA2 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetA2.AliasName = "queryA2";
            facetA2.Facets = new List<IFacetParameter> { facetA21 };

            var facetA = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetA.AliasName = "queryA";
            facetA.Facets = new List<IFacetParameter> { facetA1, facetA2 };

            var facetB1 = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetB1.AliasName = "queryB1";

            var facetB = (IFacetQueryParameter<TestFacetDocument>)new FacetQueryParameter<TestFacetDocument>();
            facetB.AliasName = "queryB";
            facetB.Facets = new List<IFacetParameter> { facetB1 };

            var searchParameters = new List<ISearchParameter>
            {
                facetA,
                facetB
            };

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(2, data.Count);
            Assert.Equal("queryA", data[0].Name);
            Assert.Equal("queryB", data[1].Name);
            Assert.Equal(FacetType.Query, data[0].FacetType);
            Assert.Equal(FacetType.Query, data[1].FacetType);
            Assert.Equal(20, ((FacetItemQuery)data[0]).Quantity);
            Assert.Equal(30, ((FacetItemQuery)data[1]).Quantity);

            Assert.NotNull(((FacetItemQuery)data[0]).Facets);
            Assert.NotNull(((FacetItemQuery)data[1]).Facets);
            Assert.Equal(2, ((FacetItemQuery)data[0]).Facets.Count());
            Assert.Equal(1, ((FacetItemQuery)data[1]).Facets.Count());

            var child1_1 = (FacetItemQuery)((FacetItemQuery)data[0]).Facets.ToList()[0];
            Assert.Equal("queryA1", child1_1.Name);
            Assert.Equal(FacetType.Query, child1_1.FacetType);
            Assert.Equal(10, child1_1.Quantity);

            var child1_2 = (FacetItemQuery)((FacetItemQuery)data[0]).Facets.ToList()[1];
            Assert.Equal("queryA2", child1_2.Name);
            Assert.Equal(FacetType.Query, child1_2.FacetType);
            Assert.Equal(10, child1_2.Quantity);
            Assert.NotNull(child1_2.Facets);
            Assert.Equal(1, child1_2.Facets.Count());

            var granchild1_2_2 = (FacetItemQuery)child1_2.Facets.ToList()[0];
            Assert.Equal("queryA21", granchild1_2_2.Name);
            Assert.Equal(FacetType.Query, granchild1_2_2.FacetType);
            Assert.Equal(5, granchild1_2_2.Quantity);

            var child2_1 = (FacetItemQuery)((FacetItemQuery)data[1]).Facets.ToList()[0];
            Assert.Equal("queryB1", child2_1.Name);
            Assert.Equal(FacetType.Query, child2_1.FacetType);
            Assert.Equal(5, child2_1.Quantity);
        }
    }
}