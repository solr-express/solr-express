using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Core.Query.Result;
using SolrExpress.Solr5.Query.Parameter;
using SolrExpress.Solr5.Query.Result;
using System;
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
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);

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
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            result = solrQuery.Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

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
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FilterParameter<TechProductDocument>().Configure(new Single<TechProductDocument>(q => q.InStock, "true")));
            solrQuery.Parameter(new FilterParameter<TechProductDocument>().Configure(new Single<TechProductDocument>(q => q.ManufacturerId, "corsair")));
            result = solrQuery.Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(3, data.Count);
            Assert.AreEqual("TWINX2048-3200PRO", data[0].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Field" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest004()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<string>> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.ManufacturerId));
            solrQuery.Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.InStock));
            result = solrQuery.Execute();
            data = result.Get(new FacetFieldResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual("ManufacturerId", data[0].Name);
            Assert.AreEqual("InStock", data[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Query" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest005()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            Dictionary<string, long> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FacetQueryParameter<TechProductDocument>().Configure("Facet1", new Range<TechProductDocument, decimal>(q => q.Popularity, from: 10)));
            solrQuery.Parameter(new FacetQueryParameter<TechProductDocument>().Configure("Facet2", new Range<TechProductDocument, decimal>(q => q.Popularity, to: 10)));
            result = solrQuery.Execute();
            data = result.Get(new FacetQueryResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(2, data["Facet1"]);
            Assert.AreEqual(15, data["Facet2"]);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Range" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest006()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<FacetRange>> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet1", q => q.Popularity, "1", "1", "10"));
            solrQuery.Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet2", q => q.Price, "10", "10", "1000"));
            result = solrQuery.Execute();
            data = result.Get(new FacetRangeResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual("Facet1", data[0].Name);
            Assert.AreEqual("Facet2", data[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and result Statistic
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest007()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            StatisticResult<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            result = solrQuery.Execute();
            data = result.Get(new StatisticResult<TechProductDocument>());

            // Assert
            Assert.AreEqual(32, data.Data.DocumentCount);
            Assert.IsFalse(data.Data.IsEmpty);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Facet.Range" and "Facet.Limit"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest008()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<FacetRange>> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet1", q => q.Popularity, "1", "1", "10"));
            solrQuery.Parameter(new FacetLimitParameter().Configure(1));
            result = solrQuery.Execute();
            data = result.Get(new FacetRangeResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual("Facet1", data[0].Name);
            Assert.AreEqual(11, data[0].Data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Facet.Field"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest009()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<string>> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.ManufacturerId, limit: 10));
            result = solrQuery.Execute();
            data = result.Get(new FacetFieldResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual("ManufacturerId", data[0].Name);
            Assert.AreEqual(10, data[0].Data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter multivalues
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest010()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new Multi(SolrQueryConditionType.Or, new Single<TechProductDocument>(c => c.Id, "S*"), new Single<TechProductDocument>(c => c.Id, "*TEST"))));
            result = solrQuery.Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(4, data.Count);
            Assert.AreEqual("GB18030TEST", data[0].Id);
            Assert.AreEqual("SP2514N", data[1].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding a new document into collection
        /// What    Create a communication between software and SOLR and add document in collection
        /// </summary>
        [TestMethod]
        public void IntegrationTest011()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/collection1");
            var config = new Configuration { FailFast = false };
            var documentCollection = new DocumentCollection<TechProductDocument>(provider, new SimpleResolver(), config);
            List<TechProductDocument> fetchedDocuments;
            var documentId = Guid.NewGuid().ToString("N");
            var documentToAdd = new TechProductDocument
            {
                Id = documentId,
                Name = "IntegrationTest009"
            };
            var update = documentCollection.Update;

            // Act
            update.Add(documentToAdd);
            update.Commit();

            // Assert
            documentCollection
                .Select
                .Query(q => q.Id, documentId)
                .Execute()
                .Document(out fetchedDocuments);

            Assert.AreEqual(1, fetchedDocuments.Count);
            Assert.AreEqual(documentId, fetchedDocuments[0].Id);
            Assert.AreEqual("IntegrationTest009", fetchedDocuments[0].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding a new document into collection and delete this document
        /// What    Create a communication between software and SOLR and document is deleted from collection
        /// </summary>
        [TestMethod]
        public void IntegrationTest012()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/collection1");
            var config = new Configuration { FailFast = false };
            var documentCollection = new DocumentCollection<TechProductDocument>(provider, new SimpleResolver(), config);
            List<TechProductDocument> fetchedDocuments;
            var documentId = Guid.NewGuid().ToString("N");
            var documentToAdd = new TechProductDocument
            {
                Id = documentId,
                Name = "IntegrationTest009"
            };
            var update = documentCollection.Update;
            update.Add(documentToAdd);
            update.Commit();

            // Act
            update.Delete(documentId);
            update.Commit();

            // Assert
            documentCollection
                .Select
                .Query(q => q.Id, documentId)
                .Execute()
                .Document(out fetchedDocuments);

            Assert.AreEqual(0, fetchedDocuments.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Sort" (once)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest013()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Id, true));
            result = solrQuery.Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(10, data.Count);
            Assert.AreEqual("0579B002", data[0].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Sort" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [TestMethod]
        public void IntegrationTest014()
        {
            // Arrange
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var config = new Configuration { FailFast = false };
            var solrQuery = new SolrQueryable<TechProductDocument>(provider, new SimpleResolver(), config);
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            solrQuery.Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()));
            solrQuery.Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Id, false));
            solrQuery.Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Name, true));
            result = solrQuery.Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.AreEqual(10, data.Count);
            Assert.AreEqual("viewsonic", data[0].Id);
        }
    }
}
