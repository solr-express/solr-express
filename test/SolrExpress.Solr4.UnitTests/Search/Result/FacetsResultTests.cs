using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr4.Search.Result;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Result
{
    public class FacetsResultTests
    {
        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet field data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult001()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facet_counts"": {
		            ""facet_fields"": {
			            ""field1"": [
				            ""VALUE001"", 10,
				            ""VALUE002"", 20
			            ],
			            ""field2"": [
				            ""VALUE001"", 10,
				            ""VALUE002"", 20
			            ],
			            ""field3"": [
			            ]
		            }
	            }
            }";

            var result = (IFacetsResult<TestDocument>)new FacetsResult<TestDocument>();

            var searchParameters = new List<ISearchParameter>();

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(3, data.Count);
            Assert.Equal("field1", data[0].Name);
            Assert.Equal("field2", data[1].Name);
            Assert.Equal("field3", data[2].Name);

            Assert.Equal(FacetType.Field, data[0].FacetType);
            Assert.Equal(FacetType.Field, data[1].FacetType);
            Assert.Equal(FacetType.Field, data[2].FacetType);

            var facetData1 = ((FacetItemMultiValues<string>)data[0]).Data;
            Assert.Equal(2, facetData1.Count());
            Assert.True(facetData1.Any(q => q.Key.Equals("VALUE001")));
            Assert.Equal(10, facetData1.First(q => q.Key.Equals("VALUE001")).Quantity);
            Assert.True(facetData1.Any(q => q.Key.Equals("VALUE002")));
            Assert.Equal(20, facetData1.First(q => q.Key.Equals("VALUE002")).Quantity);

            var facetData2 = ((FacetItemMultiValues<string>)data[1]).Data;
            Assert.Equal(2, facetData2.Count());
            Assert.True(facetData2.Any(q => q.Key.Equals("VALUE001")));
            Assert.Equal(10, facetData2.First(q => q.Key.Equals("VALUE001")).Quantity);
            Assert.True(facetData2.Any(q => q.Key.Equals("VALUE002")));
            Assert.Equal(20, facetData2.First(q => q.Key.Equals("VALUE002")).Quantity);

            var facetData3 = ((FacetItemMultiValues<string>)data[2]).Data;
            Assert.Equal(0, facetData3.Count());
        }

        /// <summary>
        /// Where   Using a FacetsResult instance
        /// When    Invoking the method "Execute" using a JSON with a facet query data
        /// What    Parse result
        /// </summary>
        [Fact]
        public void FacetFieldResult002()
        {
            // Arrange
            var jsonPlainText = @"
            {
	            ""facet_counts"": {
		            ""facet_queries"": {
			            ""query1"": 10,
			            ""query2"": 20
		            }
	            }
            }";

            var result = (IFacetsResult<TestDocument>)new FacetsResult<TestDocument>();

            var searchParameters = new List<ISearchParameter>();

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(2, data.Count);
            Assert.Equal("query1", data[0].Name);
            Assert.Equal("query2", data[1].Name);

            Assert.Equal(FacetType.Query, data[0].FacetType);
            Assert.Equal(FacetType.Query, data[1].FacetType);

            Assert.Equal(10, ((FacetItemSingleValue)data[0]).Quantity);
            Assert.Equal(20, ((FacetItemSingleValue)data[1]).Quantity);
        }
    }
}
