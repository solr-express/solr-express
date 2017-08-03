using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Search.Query.Extension;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FilterParameterTests
    {
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""id:\""X\"""",
                ""score:\""Y\""""
              ]
            }");
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter1 = (IFilterParameter<TestDocument>)new FilterParameter<TestDocument>();
            parameter1.Query = new SearchQuery<TestDocument>(expressionBuilder).Field(q => q.Id).AddValue("X");
            var parameter2 = (IFilterParameter<TestDocument>)new FilterParameter<TestDocument>();
            parameter2.Query = new SearchQuery<TestDocument>(expressionBuilder).Field(q => q.Score).AddValue("Y");

            // Act
            ((ISearchItemExecution<JObject>)parameter1).Execute();
            ((ISearchItemExecution<JObject>)parameter1).AddResultInContainer(container);
            ((ISearchItemExecution<JObject>)parameter2).Execute();
            ((ISearchItemExecution<JObject>)parameter2).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking method "Execute" using tag name
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""{!tag=tag1}id:\""X\""""
              ]
            }");
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = (IFilterParameter<TestDocument>)new FilterParameter<TestDocument>();
            parameter.Query = searchQuery.Field(q => q.Id).AddValue("X");
            parameter.TagName = "tag1";

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
