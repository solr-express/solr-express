using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Builder;
using SolrExpress.Solr5.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr5.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        /// <summary>
        /// Where   Creating a SOLR context, only creting provider and solr query classes
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest001()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var solrQuery = new SolrQueryable<TechProductDocument>(provider);

            // Act / Assert
            solrQuery.Execute();
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest002()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var solrQuery = new SolrQueryable<TechProductDocument>(provider);
            SolrQueryResult result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter(new FreeValue("*:*")));
            result = solrQuery.Execute();
            data = result.Get(new DocumentBuilder<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(10, data.Count);
            Assert.AreEqual("GB18030TEST", data[0].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Filter" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest003()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var solrQuery = new SolrQueryable<TechProductDocument>(provider);
            SolrQueryResult result;
            List<TechProductDocument> data;
            
            // Act
            solrQuery.Parameter(new QueryParameter(new FreeValue("*:*")));
            solrQuery.Parameter(new FilterParameter(new SingleValue<TechProductDocument>(q => q.InStock, "true")));
            solrQuery.Parameter(new FilterParameter(new SingleValue<TechProductDocument>(q => q.ManufacturerId, "corsair")));
            result = solrQuery.Execute();
            data = result.Get(new DocumentBuilder<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(3, data.Count);
            Assert.AreEqual("TWINX2048-3200PRO", data[0].Id);
        }
    }
}
