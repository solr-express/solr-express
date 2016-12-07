using Moq;
using SolrExpress.Core;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Search.Parameter;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FacetFieldParameterTests
    {
        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter001()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(3, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.field={!key=Id}_id_", container[1]);
            Assert.Equal("f._id_.facet.mincount=1", container[2]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter002()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();            
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id, FacetSortType.CountAsc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(4, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.field={!key=Id}_id_", container[1]);
            Assert.Equal("f._id_.facet.sort=count", container[2]);
            Assert.Equal("f._id_.facet.mincount=1", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetFieldParameter003()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id, FacetSortType.CountDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetFieldParameter004()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id, FacetSortType.IndexDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetFieldParameter005()
        {
            // Arrange
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the limit parameter
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter006()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id, limit: 10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(4, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.field={!key=Id}_id_", container[1]);
            Assert.Equal("f._id_.facet.mincount=1", container[2]);
            Assert.Equal("f._id_.facet.limit=10", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter007()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.Configure(q => q.Id, excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(3, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.field={!ex=tag1,tag2 key=Id}_id_", container[1]);
            Assert.Equal("f._id_.facet.mincount=1", container[2]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=true
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FacetFieldParameter008()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocumentWithAttribute>(expressionBuilder);
            parameter.Configure(q => q.Indexed);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=false
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FacetFieldParameter009()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            var parameter = new FacetFieldParameter<TestDocumentWithAttribute>(expressionBuilder);
            parameter.Configure(q => q.NotIndexed);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException, errorMessage);
        }
    }
}
