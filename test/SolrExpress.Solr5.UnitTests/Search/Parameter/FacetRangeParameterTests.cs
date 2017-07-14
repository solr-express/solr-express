//using Moq;
//using Newtonsoft.Json.Linq;
//using SolrExpress.Core.Search.Parameter;
//using SolrExpress.Core.Utility;
//using SolrExpress.Solr5.Search.Parameter;
//using System;
//using Xunit;

//namespace SolrExpress.Solr5.UnitTests.Search.Parameter
//{
//    public class FacetRangeParameterTests
//    {
//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Execute" using the default arguments
//        /// What    Create a valid JSON
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter001()
//        {
//            // Arrange
//            var expected = JObject.Parse(@"
//            {
//              ""facet"": {
//                ""X"": {
//                  ""range"": {
//                    ""field"": ""_id_"",
//                    ""mincount"": 1,
//                    ""gap"": ""1"",
//                    ""start"": ""10"",
//                    ""end"": ""20"",
//                    ""other"": [
//                      ""before"",
//                      ""after""
//                    ]
//                  }
//                }
//              }
//            }");
//            string actual;
//            var jObject = new JObject();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);
//            parameter.Configure("X", q => q.Id, "1", "10", "20", true, true);

//            // Act
//            parameter.Execute(jObject);
//            actual = jObject.ToString();

//            // Assert
//            Assert.Equal(expected.ToString(), actual);
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Execute" using the sort type and direction parameters
//        /// What    Create a valid JSON
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter002()
//        {
//            // Arrange
//            var expected = JObject.Parse(@"
//            {
//              ""facet"": {
//                ""X"": {
//                  ""range"": {
//                    ""field"": ""_id_"",
//                    ""mincount"": 1,
//                    ""gap"": ""1"",
//                    ""start"": ""10"",
//                    ""end"": ""20"",
//                    ""other"": [
//                      ""before"",
//                      ""after""
//                    ],
//                    ""sort"": {
//                      ""count"": ""desc""
//                    }
//                  }
//                }
//              }
//            }");
//            string actual;
//            var jObject = new JObject();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);
//            parameter.Configure("X", q => q.Id, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Execute(jObject);
//            actual = jObject.ToString();

//            // Assert
//            Assert.Equal(expected.ToString(), actual);
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type integer and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter003()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropInteger, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type long and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter004()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropLong, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type float and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter005()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropFloat, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type double and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter006()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropDouble, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type decimal and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter007()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropDecimal, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type DateTime and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter008()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropDateTime, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Validate" using a field type string and with fail fast actived
//        /// What    Is valid should be true
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter009()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder);
//            parameter.Configure("X", q => q.PropString, "1", "10", "20", true, true, FacetSortType.CountDesc);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.False(isValid);
//            Assert.False(string.IsNullOrWhiteSpace(errorMessage));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParamete instance
//        /// When    Create the instance with null in alias name
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter010()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, q => q.Id, "", "", ""));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParamete instance
//        /// When    Create the instance with null in alias name
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter011()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", null, "", "", ""));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParamete instance
//        /// When    Create the instance with null in alias name
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter012()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, null, "", ""));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParamete instance
//        /// When    Create the instance with null in alias name
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter013()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", null, ""));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParamete instance
//        /// When    Create the instance with null in alias name
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter014()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", "", null));
//        }

//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Execute" using the default arguments and an excluding list
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter015()
//        {
//            // Arrange
//            var expected = JObject.Parse(@"
//            {
//              ""facet"": {
//                ""X"": {
//                  ""range"": {
//                    ""field"": ""{!ex=tag1,tag2}_id_"",
//                    ""mincount"": 1,
//                    ""gap"": ""1"",
//                    ""start"": ""10"",
//                    ""end"": ""20"",
//                    ""other"": [
//                      ""before"",
//                      ""after""
//                    ]
//                  }
//                }
//              }
//            }").ToString();
//            var jObject = new JObject();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);
//            parameter.Configure("X", q => q.Id, "1", "10", "20", true, true, excludes: new[] { "tag1", "tag2" });

//            // Act
//            parameter.Execute(jObject);
//            var actual = jObject.ToString();

//            // Assert
//            Assert.Equal(expected, actual);
//        }

//        // <summary>
//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Execute" not calculating before range
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter016()
//        {
//            // Arrange
//            var expected = JObject.Parse(@"
//            {
//              ""facet"": {
//                ""X"": {
//                  ""range"": {
//                    ""field"": ""{!ex=tag1,tag2}_id_"",
//                    ""mincount"": 1,
//                    ""gap"": ""1"",
//                    ""start"": ""10"",
//                    ""end"": ""20"",
//                    ""other"": [
//                      ""after""
//                    ]
//                  }
//                }
//              }
//            }").ToString();
//            var jObject = new JObject();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);
//            parameter.Configure("X", q => q.Id, "1", "10", "20", false, true, excludes: new[] { "tag1", "tag2" });

//            // Act
//            parameter.Execute(jObject);
//            var actual = jObject.ToString();

//            // Assert
//            Assert.Equal(expected, actual);
//        }

//        // <summary>
//        /// <summary>
//        /// Where   Using a FacetRangeParameter instance
//        /// When    Invoking the method "Execute" not calculating after range
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void FacetRangeParameter017()
//        {
//            // Arrange
//            var expected = JObject.Parse(@"
//            {
//              ""facet"": {
//                ""X"": {
//                  ""range"": {
//                    ""field"": ""{!ex=tag1,tag2}_id_"",
//                    ""mincount"": 1,
//                    ""gap"": ""1"",
//                    ""start"": ""10"",
//                    ""end"": ""20"",
//                    ""other"": [
//                      ""before""
//                    ]
//                  }
//                }
//              }
//            }").ToString();
//            var jObject = new JObject();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FacetRangeParameter<TestDocument>(expressionBuilder);
//            parameter.Configure("X", q => q.Id, "1", "10", "20", true, false, excludes: new[] { "tag1", "tag2" });

//            // Act
//            parameter.Execute(jObject);
//            var actual = jObject.ToString();

//            // Assert
//            Assert.Equal(expected, actual);
//        }
//    }
//}
