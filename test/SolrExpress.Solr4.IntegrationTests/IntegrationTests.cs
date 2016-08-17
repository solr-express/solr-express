using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Core.Query.Result;
using SolrExpress.Solr4.Extension;
using SolrExpress.Solr4.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System;
using System.Collections.Generic;
using Xunit;
#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
#endif

namespace SolrExpress.Solr4.IntegrationTests
{
    public class IntegrationTests
    {
#if NETCOREAPP1_0
        private IServiceProvider _serviceProvider;
#else
        private DocumentCollectionBuilder<TechProductDocument> _documentCollectionBuilder;
#endif
        /// <summary>
        /// Default constructor of class
        /// </summary>
        public IntegrationTests()
        {
            var options = new DocumentCollectionOptions<TechProductDocument>
            {
                FailFast = false
            };

#if NETCOREAPP1_0
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSolrExpress<TechProductDocument>(builder => builder
                .UseOptions(options)
                .UseHostAddress("http://localhost:8983/solr/collection1")
                .UseSolr4());

            this._serviceProvider = serviceCollection.BuildServiceProvider();
#else
            this._documentCollectionBuilder = new DocumentCollectionBuilder<TechProductDocument>()
                .AddSolrExpress()
                .UseOptions(options)
                .UseHostAddress("http://localhost:8983/solr/collection1")
                .UseSolr4();
#endif
        }

        /// <summary>
        /// Get a flesh instance of DocumentCollection<TechProductDocument> 
        /// </summary>
        /// <returns>Instance of DocumentCollection<TechProductDocument></returns>
        private DocumentCollection<TechProductDocument> GetDocumentCollection()
        {
#if NETCOREAPP1_0
            return this._serviceProvider.GetRequiredService<DocumentCollection<TechProductDocument>>();
#else
            return this._documentCollectionBuilder.Create();
#endif
        }

        /// <summary>
        /// Where   Creating a SOLR context, only creting provider and solr query classes
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest001()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act / Assert
            documentCollection.Select().Execute();
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest002()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(10, data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Filter" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest003()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new FilterQueryParameter<TechProductDocument>().Configure(new Single<TechProductDocument>(q => q.InStock, "true")))
                .Parameter(new FilterQueryParameter<TechProductDocument>().Configure(new Single<TechProductDocument>(q => q.ManufacturerId, "corsair")))
                .Execute();

            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(3, data.Count);
            Assert.Equal("TWINX2048-3200PRO", data[0].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Field" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest004()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<string>> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.ManufacturerId))
                .Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.InStock))
                .Execute();
            data = result.Get(new FacetFieldResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(2, data.Count);
            Assert.Equal("ManufacturerId", data[0].Name);
            Assert.Equal("InStock", data[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Query" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest005()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            Dictionary<string, long> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new FacetQueryParameter<TechProductDocument>().Configure("Facet1", new Range<TechProductDocument, decimal>(q => q.Popularity, from: 10)))
                .Parameter(new FacetQueryParameter<TechProductDocument>().Configure("Facet2", new Range<TechProductDocument, decimal>(q => q.Popularity, to: 10)))
                .Execute();
            data = result.Get(new FacetQueryResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(2, data.Count);
            Assert.True(data["Facet1"] > 0);
            Assert.True(data["Facet2"] > 0);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and "Facet.Range" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest006()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<FacetRange>> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet1", q => q.Popularity, "1", "1", "10"))
                .Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet2", q => q.Price, "10", "10", "1000"))
                .Parameter(new FacetRangeParameter<TechProductDocument>().Configure("Facet3", q => q.ManufacturedateIn, "+10DAYS", "NOW-30YEARS", "NOW+1DAY"))
                .Execute();
            data = result.Get(new FacetRangeResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(3, data.Count);
            Assert.Equal("Facet1", data[0].Name);
            Assert.Equal("Facet2", data[1].Name);
            Assert.Equal("Facet3", data[2].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and result Statistic
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest007()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            InformationResult<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Execute();
            data = result.Get(new InformationResult<TechProductDocument>());

            // Assert
            Assert.True(data.Data.DocumentCount > 0);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Facet.Field"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest008()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<FacetKeyValue<string>> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new FacetFieldParameter<TechProductDocument>().Configure(q => q.ManufacturerId, limit: 1))
                .Execute();
            data = result.Get(new FacetFieldResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(1, data.Count);
            Assert.Equal("ManufacturerId", data[0].Name);
            Assert.Equal(1, data[0].Data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding a new document into collection
        /// What    Create a communication between software and SOLR and add document in collection
        /// </summary>
        [Fact]
        public void IntegrationTest009()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            List<TechProductDocument> fetchedDocuments;
            var documentId = Guid.NewGuid().ToString("N");
            var documentToAdd = new TechProductDocument
            {
                Id = documentId,
                Name = "IntegrationTest009"
            };
            var update = documentCollection.Update();

            // Act
            update.Add(documentToAdd);
            update.Commit();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, documentId)
                .Execute()
                .Document(out fetchedDocuments);

            Assert.Equal(1, fetchedDocuments.Count);
            Assert.Equal(documentId, fetchedDocuments[0].Id);
            Assert.Equal("IntegrationTest009", fetchedDocuments[0].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding a new document into collection and delete this document
        /// What    Create a communication between software and SOLR and document is deleted from collection
        /// </summary>
        [Fact]
        public void IntegrationTest010()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            List<TechProductDocument> fetchedDocuments;
            var documentId = Guid.NewGuid().ToString("N");
            var documentToAdd = new TechProductDocument
            {
                Id = documentId,
                Name = "IntegrationTest009"
            };
            var update = documentCollection.Update();
            update.Add(documentToAdd);
            update.Commit();

            // Act
            update.Delete(documentId);
            update.Commit();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, documentId)
                .Execute()
                .Document(out fetchedDocuments);

            Assert.Equal(0, fetchedDocuments.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Sort" (once)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest011()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Id, true))
                .Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(10, data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Sort" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest012()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Id, false))
                .Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Name, true))
                .Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(10, data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding 2 new documents into collection
        /// What    Create a communication between software and SOLR and add document in collection
        /// </summary>
        [Fact]
        public void IntegrationTest013()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            List<TechProductDocument> fetchedDocuments;
            var documentId1 = Guid.NewGuid().ToString("N");
            var documentId2 = Guid.NewGuid().ToString("N");
            var documentToAdd1 = new TechProductDocument
            {
                Id = documentId1,
                Name = "IntegrationTest013"
            };
            var documentToAdd2 = new TechProductDocument
            {
                Id = documentId2,
                Name = "IntegrationTest013"
            };
            var update = documentCollection.Update();

            // Act
            update.Add(documentToAdd1, documentToAdd2);
            update.Commit();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, $"({documentId1} OR {documentId2})")
                .Execute()
                .Document(out fetchedDocuments);

            Assert.Equal(2, fetchedDocuments.Count);
            Assert.Equal(documentId1, fetchedDocuments[0].Id);
            Assert.Equal(documentId2, fetchedDocuments[1].Id);
            Assert.Equal("IntegrationTest013", fetchedDocuments[0].Name);
            Assert.Equal("IntegrationTest013", fetchedDocuments[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Sort" (twice)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest014()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            List<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Id, false))
                .Parameter(new SortParameter<TechProductDocument>().Configure(q => q.Name, true))
                .Execute();
            data = result.Get(new DocumentResult<TechProductDocument>()).Data;

            // Assert
            Assert.Equal(10, data.Count);
        }

        /// <summary>
        /// Where   Creating a SOLR context
        /// When    Adding 2 new documents into collection
        /// What    Create a communication between software and SOLR and add document in collection
        /// </summary>
        [Fact]
        public void IntegrationTest015()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            List<TechProductDocument> fetchedDocuments;
            var documentId1 = Guid.NewGuid().ToString("N");
            var documentId2 = Guid.NewGuid().ToString("N");
            var documentToAdd1 = new TechProductDocument
            {
                Id = documentId1,
                Name = "IntegrationTest013"
            };
            var documentToAdd2 = new TechProductDocument
            {
                Id = documentId2,
                Name = "IntegrationTest013"
            };
            var update = documentCollection.Update();

            // Act
            update.Add(documentToAdd1, documentToAdd2);
            update.Commit();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, $"({documentId1} OR {documentId2})")
                .Execute()
                .Document(out fetchedDocuments);

            Assert.Equal(2, fetchedDocuments.Count);
            Assert.Equal(documentId1, fetchedDocuments[0].Id);
            Assert.Equal(documentId2, fetchedDocuments[1].Id);
            Assert.Equal("IntegrationTest013", fetchedDocuments[0].Name);
            Assert.Equal("IntegrationTest013", fetchedDocuments[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type boost) and result Statistic
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest016()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            InformationResult<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new BoostParameter<TechProductDocument>().Configure(new Any("inStock"), BoostFunctionType.Boost))
                .Execute();
            data = result.Get(new InformationResult<TechProductDocument>());

            // Assert
            Assert.True(data.Data.DocumentCount > 1);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type bf) and result Statistic
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest017()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            QueryResult<TechProductDocument> result;
            InformationResult<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Parameter(new QueryParameter<TechProductDocument>().Configure(new QueryAll()))
                .Parameter(new BoostParameter<TechProductDocument>().Configure(new Any("inStock"), BoostFunctionType.Bf))
                .Execute();
            data = result.Get(new InformationResult<TechProductDocument>());

            // Assert
            Assert.True(data.Data.DocumentCount > 1);
        }
    }
}
