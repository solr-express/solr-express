using Moq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.Result;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.Result
{
    public class InformationBuilderTests
    {
        /// <summary>
        /// Where   Using InformationBuilder class
        /// When    Invoking the method "Calculate" using offset=0, limit=100, documents=200
        /// What    Configure statistic instance with correct values
        /// </summary>
        [Fact]
        public void InformationBuilder001()
        {
            // Arrange
            var offsetParameterMock = new Mock<IOffsetParameter<TestDocument>>();
            var limitParameterMock = new Mock<ILimitParameter<TestDocument>>();

            offsetParameterMock.SetupGet(q => q.Value).Returns(0);
            limitParameterMock.SetupGet(q => q.Value).Returns(100);

            var list = new List<ISearchParameter>
            {
                offsetParameterMock.Object,
                limitParameterMock.Object
            };

            // Act
            var statistic = InformationBuilder<TestDocument>.Create(list, 1, 200);

            // Assert
            Assert.Equal(200, statistic.DocumentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), statistic.ElapsedTime);
            Assert.Equal(true, statistic.HasNextPage);
            Assert.Equal(false, statistic.HasPreviousPage);
            Assert.Equal(true, statistic.IsFirstPage);
            Assert.Equal(false, statistic.IsLastPage);
            Assert.Equal(2, statistic.PageCount);
            Assert.Equal(1, statistic.PageNumber);
            Assert.Equal(100, statistic.PageSize);
        }

        /// <summary>
        /// Where   Using InformationBuilder class
        /// When    Invoking the method "Calculate" using offset=0, limit=100, documents=201
        /// What    Configure statistic instance with correct values
        /// </summary>
        [Fact]
        public void InformationBuilder002()
        {
            // Arrange
            var offsetParameterMock = new Mock<IOffsetParameter<TestDocument>>();
            var limitParameterMock = new Mock<ILimitParameter<TestDocument>>();

            offsetParameterMock.SetupGet(q => q.Value).Returns(0);
            limitParameterMock.SetupGet(q => q.Value).Returns(100);

            var list = new List<ISearchParameter>
            {
                offsetParameterMock.Object,
                limitParameterMock.Object
            };

            // Act
            var statistic = InformationBuilder<TestDocument>.Create(list, 1, 201);

            // Assert
            Assert.Equal(201, statistic.DocumentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), statistic.ElapsedTime);
            Assert.Equal(true, statistic.HasNextPage);
            Assert.Equal(false, statistic.HasPreviousPage);
            Assert.Equal(true, statistic.IsFirstPage);
            Assert.Equal(false, statistic.IsLastPage);
            Assert.Equal(3, statistic.PageCount);
            Assert.Equal(1, statistic.PageNumber);
            Assert.Equal(100, statistic.PageSize);
        }

        /// <summary>
        /// Where   Using InformationBuilder class
        /// When    Invoking the method "Calculate" using offset=0, limit=100, documents=0
        /// What    Configure statistic instance with correct values
        /// </summary>
        [Fact]
        public void InformationBuilder003()
        {
            // Arrange
            var offsetParameterMock = new Mock<IOffsetParameter<TestDocument>>();
            var limitParameterMock = new Mock<ILimitParameter<TestDocument>>();

            offsetParameterMock.SetupGet(q => q.Value).Returns(0);
            limitParameterMock.SetupGet(q => q.Value).Returns(100);

            var list = new List<ISearchParameter>
            {
                offsetParameterMock.Object,
                limitParameterMock.Object
            };

            // Act
            var statistic = InformationBuilder<TestDocument>.Create(list, 1, 0);

            // Assert
            Assert.Equal(0, statistic.DocumentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), statistic.ElapsedTime);
            Assert.Equal(false, statistic.HasNextPage);
            Assert.Equal(false, statistic.HasPreviousPage);
            Assert.Equal(true, statistic.IsFirstPage);
            Assert.Equal(true, statistic.IsLastPage);
            Assert.Equal(0, statistic.PageCount);
            Assert.Equal(1, statistic.PageNumber);
            Assert.Equal(100, statistic.PageSize);
        }

        /// <summary>
        /// Where   Using InformationBuilder class
        /// When    Invoking the method "Calculate" using offset=100, limit=100, documents=200
        /// What    Configure statistic instance with correct values
        /// </summary>
        [Fact]
        public void InformationBuilder004()
        {
            // Arrange
            var offsetParameterMock = new Mock<IOffsetParameter<TestDocument>>();
            var limitParameterMock = new Mock<ILimitParameter<TestDocument>>();

            offsetParameterMock.SetupGet(q => q.Value).Returns(100);
            limitParameterMock.SetupGet(q => q.Value).Returns(100);

            var list = new List<ISearchParameter>
            {
                offsetParameterMock.Object,
                limitParameterMock.Object
            };

            // Act
            var statistic = InformationBuilder<TestDocument>.Create(list, 1, 200);

            // Assert
            Assert.Equal(200, statistic.DocumentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), statistic.ElapsedTime);
            Assert.Equal(false, statistic.HasNextPage);
            Assert.Equal(true, statistic.HasPreviousPage);
            Assert.Equal(false, statistic.IsFirstPage);
            Assert.Equal(true, statistic.IsLastPage);
            Assert.Equal(2, statistic.PageCount);
            Assert.Equal(2, statistic.PageNumber);
            Assert.Equal(100, statistic.PageSize);
        }

        /// <summary>
        /// Where   Using InformationBuilder class
        /// When    Invoking the method "Calculate" using offset=200, limit=100, documents=300
        /// What    Configure statistic instance with correct values
        /// </summary>
        [Fact]
        public void InformationBuilder005()
        {
            // Arrange
            var offsetParameterMock = new Mock<IOffsetParameter<TestDocument>>();
            var limitParameterMock = new Mock<ILimitParameter<TestDocument>>();

            offsetParameterMock.SetupGet(q => q.Value).Returns(200);
            limitParameterMock.SetupGet(q => q.Value).Returns(100);

            var list = new List<ISearchParameter>
            {
                offsetParameterMock.Object,
                limitParameterMock.Object
            };

            // Act
            var statistic = InformationBuilder<TestDocument>.Create(list, 1, 300);

            // Assert
            Assert.Equal(300, statistic.DocumentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), statistic.ElapsedTime);
            Assert.Equal(false, statistic.HasNextPage);
            Assert.Equal(true, statistic.HasPreviousPage);
            Assert.Equal(false, statistic.IsFirstPage);
            Assert.Equal(true, statistic.IsLastPage);
            Assert.Equal(3, statistic.PageCount);
            Assert.Equal(3, statistic.PageNumber);
            Assert.Equal(100, statistic.PageSize);
        }
    }
}
