using Moq;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using Xunit;

namespace SolrExpress.UnitTests.Search
{
    [UseAnyThanSpecificParameterRather]
    public sealed class FakeParameter : IAnyParameter, ISearchParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class DocumentSearchTests
    {
        /// <summary>
        /// Where   Using an instance of a class that implements IAnyParameter
        /// When    Validate this instance in method DocumentSearch<T>.ValidateSearchItem using SolrExpressOptions.CheckAnyParameter=true
        /// What    Throws SearchParameterIsInvalidException
        /// </summary>
        [Fact]
        public void DocumentSearchFact001()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions
            {
                CheckAnyParameter = true
            };
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            var searchItemCollection = new Mock<ISearchItemCollection<TestDocument>>();

            var documentSearch = new DocumentCollectionSearch<TestDocument>(
                solrExpressOptions,
                serviceProvider.Object,
                searchItemCollection.Object);

            var parameter = new FakeParameter
            {
                Name = "q"
            };

            // Act / Assert
            Assert.Throws<SearchParameterIsInvalidException>(() => documentSearch.ValidateSearchItem(parameter));
        }

        /// <summary>
        /// Where   Using an instance of a class that implements IAnyParameter
        /// When    Validate this instance in method DocumentSearch<T>.ValidateSearchItem using SolrExpressOptions.CheckAnyParameter=false
        /// What    Don´t throws SearchParameterIsInvalidException
        /// </summary>
        [Fact]
        public void DocumentSearchFact002()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions
            {
                CheckAnyParameter = false
            };
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            var searchItemCollection = new Mock<ISearchItemCollection<TestDocument>>();

            var documentSearch = new DocumentCollectionSearch<TestDocument>(
                solrExpressOptions,
                serviceProvider.Object,
                searchItemCollection.Object);

            var parameter = new FakeParameter
            {
                Name = "q"
            };

            // Act / Assert
            documentSearch.ValidateSearchItem(parameter);
        }
    }
}
