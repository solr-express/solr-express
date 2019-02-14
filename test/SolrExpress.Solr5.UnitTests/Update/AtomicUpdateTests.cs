using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Solr5.Update;
using SolrExpress.Update;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Update
{
    public class AtomicUpdateTests
    {
        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate001()
        {
            // Arrange
            var expected = JObject.Parse(@"
                {
                ""update"": [{""id"" : ""123456"",
                    ""dummy"" : {""set"":""aaaa""},
                    ""dummy2"" : {""set"":""bbbb""},
                    ""number_value"" : {""inc"":10},
                    ""number_value2"" : {""dec"":20},
                    ""array_value"" : {""add"":[1]},
                    ""array_value2"" : {""remove"":[2]},
                    ""dummy3"" : {""removeregex"":""/a/""}
                }],
                 ""commit"": {}
                }");
            var document = new DocumentUpdate<TestUpdateDocument>
            {
                Id = "123456"
            };
            document.Set(q => q.Dummy, "aaaa");
            document.Set(q => q.Dummy2, "bbbb");
            document.Increment(q => q.NumberValue, 10);
            document.Decrement(q => q.NumberValue2, 20);
            document.Add(q => q.ArrayValue, 1);
            document.Remove(q => q.ArrayValue2, 2);
            document.RemoveRegex(q => q.Dummy3, "/a/");

            var configuration = new SolrDocumentConfiguration<TestUpdateDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicUpdate<TestUpdateDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            actual = JObject.Parse(actual.ToString());
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""update"": [{
                        ""id"": ""123456"",
			            ""dummy"": {
                            ""set"": ""aaaa""

                        },
			            ""dummy2"": {
                            ""set"": ""bbbb""

                        }
                    },
		            {
			            ""id"": ""654321"",
			            ""dummy"": {
				            ""set"": ""cccc""
			            },
			            ""dummy2"": {
				            ""set"": ""dddd""
			            }
		            }
	            ],
	            ""commit"": {}
            }");
            var document1 = new DocumentUpdate<TestUpdateDocument>
            {
                Id = "123456"
            };
            document1.Set(q => q.Dummy, "aaaa");
            document1.Set(q => q.Dummy2, "bbbb");
            var document2 = new DocumentUpdate<TestUpdateDocument>
            {
                Id = "654321",
            };
            document2.Set(q => q.Dummy, "cccc");
            document2.Set(q => q.Dummy2, "dddd");

            var configuration = new SolrDocumentConfiguration<TestUpdateDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicUpdate<TestUpdateDocument>(configuration);

            // Act
            var actual = atomic.Execute(document1, document2);

            // Assert
            actual = JObject.Parse(actual.ToString());
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute" without any document
        /// What    Create a string.empty
        /// </summary>
        [Fact]
        public void AtomicUpdate003()
        {
            // Arrange
            var configuration = new SolrDocumentConfiguration<TestUpdateDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicUpdate<TestUpdateDocument>(configuration);

            // Act
            var actual = atomic.Execute();

            // Assert
            Assert.Null(actual);
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate004()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""update"": [{
                        ""id"": ""123456"",
			            ""date"": {
                            ""set"": ""2018-06-08T00:00:00Z""

                        },
			            ""dateNullable"": {
                            ""set"": ""2018-06-08T00:00:00Z""
                        },
			            ""coord"": {
                            ""set"": ""1.2,2.3""
                        },
			            ""coordNullable"": {
                            ""set"": ""1.2,2.3""
                        }
                    }
	            ],
	            ""commit"": {}
            }");
            var document = new DocumentUpdate<SimpleNullableTestDocument>
            {
                Id = "123456"
            };

            document.Set(q => q.Date, new DateTime(2018, 06, 08));
            document.Set(q => q.DateNullable, new DateTime(2018, 06, 08));
            document.Set(q => q.GeoCoordinate, new GeoCoordinate(1.2M, 2.3M));
            document.Set(q => q.GeoCoordinateNullable, new GeoCoordinate(1.2M, 2.3M));
            var configuration = new SolrDocumentConfiguration<SimpleNullableTestDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicUpdate<SimpleNullableTestDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            actual = JObject.Parse(actual.ToString());
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
