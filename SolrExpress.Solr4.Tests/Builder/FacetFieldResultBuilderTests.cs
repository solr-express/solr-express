using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr4.Builder;

namespace SolrExpress.Solr4.Tests.Builder
{
    [TestClass]
    public class FacetFieldResultBuilderTests
    {
        /// <summary>
        /// Where   Using a FacetFieldResultBuilder instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetFieldResultBuilder001()
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

            var parameter = new FacetFieldResultBuilder();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetField", parameter.Data[0].Name);
            Assert.AreEqual(2, parameter.Data[0].Data.Count);
            Assert.IsTrue(parameter.Data[0].Data.ContainsKey("VALUE001"));
            Assert.AreEqual(10, parameter.Data[0].Data["VALUE001"]);
            Assert.IsTrue(parameter.Data[0].Data.ContainsKey("VALUE002"));
            Assert.AreEqual(20, parameter.Data[0].Data["VALUE002"]);
        }
    }
}
