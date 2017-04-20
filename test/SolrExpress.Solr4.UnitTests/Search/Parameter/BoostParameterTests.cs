using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class BoostParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [InlineData(BoostFunctionType.Bf, "bf=id")]
        [InlineData(BoostFunctionType.Boost, "boost=id")]
        public void BoostParameterTheory001(BoostFunctionType boostFunctionType, string expected)
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IBoostParameter<TestDocument>)new BoostParameter<TestDocument>();
            parameter.BoostFunctionType = boostFunctionType;
            //TODO: Implement Query
            //parameter.Query = new Any<TestDocument>("id");

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal(expected, container[0]);
        }
    }
}
