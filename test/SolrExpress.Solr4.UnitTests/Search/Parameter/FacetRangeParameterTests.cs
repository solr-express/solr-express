using Xunit;
using SolrExpress.Core;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FacetRangeParameterTests
    {
        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetRangeParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(8, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.range={!key=X}_id_", container[1]);
            Assert.Equal("f._id_.facet.range.gap=1", container[2]);
            Assert.Equal("f._id_.facet.range.start=10", container[3]);
            Assert.Equal("f._id_.facet.range.end=20", container[4]);
            Assert.Equal("f._id_.facet.range.other=before", container[5]);
            Assert.Equal("f._id_.facet.range.other=after", container[6]);
            Assert.Equal("f._id_.facet.mincount=1", container[7]);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetRangeParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", FacetSortType.CountAsc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(9, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.range={!key=X}_id_", container[1]);
            Assert.Equal("f._id_.facet.range.gap=1", container[2]);
            Assert.Equal("f._id_.facet.range.start=10", container[3]);
            Assert.Equal("f._id_.facet.range.end=20", container[4]);
            Assert.Equal("f._id_.facet.range.other=before", container[5]);
            Assert.Equal("f._id_.facet.range.other=after", container[6]);
            Assert.Equal("f._id_.facet.range.sort=count", container[7]);
            Assert.Equal("f._id_.facet.mincount=1", container[8]);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type integer and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter003()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropInteger, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type long and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter004()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropLong, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type float and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter005()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropFloat, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type double and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter006()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDouble, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type decimal and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter007()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDecimal, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type DateTime and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter008()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDateTime, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type string and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter009()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropString, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.False(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetRangeParamete010()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", FacetSortType.CountDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact]
        public void FacetRangeParamete011()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", FacetSortType.IndexDesc);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => parameter.Execute(container));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter012()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, q => q.Id, null, null, null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in expression value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter013()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", null, null, null, null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in gap value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter014()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, null, null, null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in start value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter015()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", null, null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in end value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter016()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", "", null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetRangeParameter017()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(8, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.range={!ex=tag1,tag2 key=X}_id_", container[1]);
            Assert.Equal("f._id_.facet.range.gap=1", container[2]);
            Assert.Equal("f._id_.facet.range.start=10", container[3]);
            Assert.Equal("f._id_.facet.range.end=20", container[4]);
            Assert.Equal("f._id_.facet.range.other=before", container[5]);
            Assert.Equal("f._id_.facet.range.other=after", container[6]);
            Assert.Equal("f._id_.facet.mincount=1", container[7]);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetRangeParameter018()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "+7DAY", "NOW-21DAYS", "NOW+1DAY");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(8, container.Count);
            Assert.Equal("facet=true", container[0]);
            Assert.Equal("facet.range={!key=X}_id_", container[1]);
            Assert.Equal("f._id_.facet.range.gap=%2B7DAY", container[2]);
            Assert.Equal("f._id_.facet.range.start=NOW-21DAYS", container[3]);
            Assert.Equal("f._id_.facet.range.end=NOW%2B1DAY", container[4]);
            Assert.Equal("f._id_.facet.range.other=before", container[5]);
            Assert.Equal("f._id_.facet.range.other=after", container[6]);
            Assert.Equal("f._id_.facet.mincount=1", container[7]);
        }
    }
}
