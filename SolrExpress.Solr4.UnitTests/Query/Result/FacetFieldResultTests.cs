using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr4.Query.Result;

namespace SolrExpress.Solr4.UnitTests.Query.Result
{
    public class FacetFieldResultTests
    {
        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetFieldResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facet_counts"":{
                ""facet_fields"":{
                    ""facetField"":[
                    ""VALUE001"",10,
                    ""VALUE002"",20]}}
            }");

            var parameter = new FacetFieldResult<TestDocument>();
            
            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.Equal(1, parameter.Data.Count);
            Assert.Equal("facetField", parameter.Data[0].Name);
            Assert.Equal(2, parameter.Data[0].Data.Count);
            Assert.True(parameter.Data[0].Data.ContainsKey("VALUE001"));
            Assert.Equal(10, parameter.Data[0].Data["VALUE001"]);
            Assert.True(parameter.Data[0].Data.ContainsKey("VALUE002"));
            Assert.Equal(20, parameter.Data[0].Data["VALUE002"]);
        }
    }
}
