using Xunit;
using SolrExpress.Core;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class FacetQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"));

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(3, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.query={!key=X}avg('Y')", container[1]);
            Assert.Equal("f.X.facet.mincount=1", container[2]);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetQueryParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"), FacetSortType.CountAsc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(4, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.query={!key=X}avg('Y')", container[1]);
            Assert.Equal("f.X.facet.sort=count", container[2]);
            Assert.Equal("f.X.facet.mincount=1", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetQueryParameter003()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new QueryAll(), FacetSortType.CountDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetQueryParameter004()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new QueryAll(), FacetSortType.IndexDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetQueryParameter005()
        {
            // Arrange
            var parameter = new FacetQueryParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, new Any("x")));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in expression
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetQueryParameter006()
        {
            // Arrange / Act / Assert
            var parameter = new FacetQueryParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", null));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetQueryParameter007()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"), excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(3, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.query={!ex=tag1,tag2 key=X}avg('Y')", container[1]);
            Assert.Equal("f.X.facet.mincount=1", container[2]);
        }
    }
}
