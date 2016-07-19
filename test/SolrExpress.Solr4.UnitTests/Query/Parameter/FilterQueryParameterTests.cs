using Xunit;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class FilterQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter1 = new FilterQueryParameter<TestDocument>();
            var parameter2 = new FilterQueryParameter<TestDocument>();
            parameter1.Configure(new Single<TestDocument>(q => q.Id, "X"));
            parameter2.Configure(new Single<TestDocument>(q => q.Score, "Y"));

            // Act
            parameter1.Execute(container);
            parameter2.Execute(container);

            // Assert
            Assert.Equal(2, container.Count);
            Assert.Equal("fq=_id_:X", container[0]);
            Assert.Equal("fq=_score_:Y", container[1]);
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FilterQueryParameter002()
        {
            // Arrange
            var parameter = new FilterQueryParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using tag name
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter003()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FilterQueryParameter<TestDocument>();
            parameter.Configure(new Single<TestDocument>(q => q.Id, "X"), "tag1");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fq={!tag=tag1}_id_:X", container[0]);
        }
    }
}
