using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr4.Search.Result;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Result
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
            var parameters = new List<ISearchParameter>();
            var result = (IConvertJsonObject)new FacetFieldResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetFieldResult<TestDocument>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetField", data[0].Name);
            Assert.Equal(2, data[0].Data.Count);
            Assert.True(data[0].Data.ContainsKey("VALUE001"));
            Assert.Equal(10, data[0].Data["VALUE001"]);
            Assert.True(data[0].Data.ContainsKey("VALUE002"));
            Assert.Equal(20, data[0].Data["VALUE002"]);
        }
    }
}
