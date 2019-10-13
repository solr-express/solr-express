using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Solr5.Update;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Update
{
    public class AtomicAddTests
    {
        /// <summary>
        /// Where   Using a AtomicAdd instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicAdd001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{

                        ""dummy"": ""ymmud"",
			            ""id"": ""123456""

                    }
	            ],
	            ""commit"": {}
            }");
            var document = new TestAddDocument
            {
                Id = "123456",
                Dummy = "ymmud"
            };
            var configuration = new SolrDocumentConfiguration<TestAddDocument>();
            configuration.Field(q => q.Id).HasName("id");
            configuration.Field(q => q.Dummy).HasName("dummy");
            var atomic = new AtomicAdd<TestAddDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicAdd instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicAdd002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{
                        ""dummy"": ""ymmud"",
			            ""id"": ""123456""
                    },
		            {
                        ""dummy"": ""ymmud2"",
			            ""id"": ""654321""
		            }
	            ],
	            ""commit"": {}
            }");
            var document1 = new TestAddDocument
            {
                Id = "123456",
                Dummy = "ymmud"
            };
            var document2 = new TestAddDocument
            {
                Id = "654321",
                Dummy = "ymmud2"
            };
            var configuration = new SolrDocumentConfiguration<TestAddDocument>();
            configuration.Field(q => q.Id).HasName("id");
            configuration.Field(q => q.Dummy).HasName("dummy");
            var atomic = new AtomicAdd<TestAddDocument>(configuration);

            // Act
            var actual = atomic.Execute(document1, document2);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicAdd instance
        /// When    Invoking method "Execute" without any document
        /// What    Create a string.empty
        /// </summary>
        [Fact]
        public void AtomicAdd003()
        {
            // Arrange
            var configuration = new SolrDocumentConfiguration<TestAddDocument>();
            configuration.Field(q => q.Id).HasName("id");
            configuration.Field(q => q.Dummy).HasName("dummy");
            var atomic = new AtomicAdd<TestAddDocument>(configuration);

            // Act
            var actual = atomic.Execute();

            // Assert
            Assert.Null(actual);
        }

        /// <summary>
        /// Where   Using a AtomicAdd instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicAdd004()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{
                        ""date"": ""2018-06-08T00:00:00Z"",
                        ""dateNullable"": ""2018-06-08T00:00:00Z"",
                        ""coord"": ""1.2,2.3"",
                        ""coordNullable"": ""1.2,2.3"",
			            ""id"": ""123456""
                    }
	            ],
	            ""commit"": {}
            }");
            var document = new SimpleNullableTestDocument
            {
                Id = "123456",
                Date = new DateTime(2018, 06, 08),
                DateNullable = new DateTime(2018, 06, 08),
                GeoCoordinate = new GeoCoordinate(1.2M, 2.3M),
                GeoCoordinateNullable = new GeoCoordinate(1.2M, 2.3M),
            };
            var configuration = new SolrDocumentConfiguration<SimpleNullableTestDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicAdd<SimpleNullableTestDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicAdd instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicAdd005()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{
                        ""date"": ""2018-06-08T00:00:00Z"",
                        ""coord"": ""1.2,2.3"",
			            ""id"": ""123456""
                    }
	            ],
	            ""commit"": {}
            }");
            var document = new SimpleNullableTestDocument
            {
                Id = "123456",
                Date = new DateTime(2018, 06, 08),
                DateNullable = null,
                GeoCoordinate = new GeoCoordinate(1.2M, 2.3M),
                GeoCoordinateNullable = null,
            };
            var configuration = new SolrDocumentConfiguration<SimpleNullableTestDocument>();
            configuration.Field(q => q.Id).HasName("id");
            var atomic = new AtomicAdd<SimpleNullableTestDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
