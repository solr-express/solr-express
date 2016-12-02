using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Solr4.Extension;
using System;
using System.Collections.Generic;
using Xunit;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Search;
using System.Linq;
using SolrExpress.Core.Search.Parameter;
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
        private IDocumentCollection<TechProductDocument> GetDocumentCollection()
        {
#if NETCOREAPP1_0
            return this._serviceProvider.GetRequiredService<IDocumentCollection<TechProductDocument>>();
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<TechProductDocument> data;

            // Act
            documentCollection.Select().Query("*:*");

            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Execute();

            result.Document(out data);

            // Assert
            Assert.Equal(10, data.Count());
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Filter(q => q.InStock, "true")
                .Filter(q => q.ManufacturerId, "corsair")
                .Execute();

            result.Document(out data);

            // Assert
            Assert.Equal(3, data.Count());
            Assert.Equal("TWINX2048-3200PRO", data.ToList()[0].Id);
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<FacetKeyValue<string>> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .FacetField(q => q.ManufacturerId)
                .FacetField(q => q.InStock)
                .Execute();

            result.FacetField(out data);

            // Assert
            Assert.Equal(2, data.Count());
            Assert.Equal("ManufacturerId", data.ToList()[0].Name);
            Assert.Equal("InStock", data.ToList()[1].Name);
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
            ISearchResult<TechProductDocument> result;
            IDictionary<string, long> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .FacetQuery("Facet1", new Range<TechProductDocument, decimal>(q => q.Popularity, from: 10))
                .FacetQuery("Facet2", new Range<TechProductDocument, decimal>(q => q.Popularity, to: 10))
                .Execute();

            result.FacetQuery(out data);

            // Assert
            Assert.Equal(2, data.Count());
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<FacetKeyValue<FacetRange>> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .FacetRange("Facet1", q => q.Popularity, "1", "1", "10")
                .FacetRange("Facet2", q => q.Price, "10", "10", "1000")
                .FacetRange("Facet3", q => q.ManufacturedateIn, "+10DAYS", "NOW-30YEARS", "NOW+1DAY")
                .Execute();

            result.FacetRange(out data);

            // Assert
            Assert.Equal(3, data.Count());
            Assert.Equal("Facet1", data.ToList()[0].Name);
            Assert.Equal("Facet2", data.ToList()[1].Name);
            Assert.Equal("Facet3", data.ToList()[2].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Query" and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest007()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            ISearchResult<TechProductDocument> result;
            Information data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Execute();

            result.Information(out data);

            // Assert
            Assert.True(data.DocumentCount > 0);
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<FacetKeyValue<string>> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .FacetField(q => q.ManufacturerId, limit: 1)
                .Execute();
            result.FacetField(out data);

            // Assert
            Assert.Equal(1, data.Count());
            Assert.Equal("ManufacturerId", data.ToList()[0].Name);
            Assert.Equal(1, data.ToList()[0].Data.Count());
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
            IEnumerable<TechProductDocument> fetchedDocuments;
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

            Assert.Equal(1, fetchedDocuments.Count());
            Assert.Equal(documentId, fetchedDocuments.ToList()[0].Id);
            Assert.Equal("IntegrationTest009", fetchedDocuments.ToList()[0].Name);
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
            IEnumerable<TechProductDocument> fetchedDocuments;
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

            Assert.Equal(0, fetchedDocuments.Count());
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Sort(q => q.Id, true)
                .Execute();
            result.Document(out data);

            // Assert
            Assert.Equal(10, data.Count());
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Sort(q => q.Id, false)
                .Sort(q => q.Name, true)
                .Execute();
            result.Document(out data);

            // Assert
            Assert.Equal(10, data.Count());
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
            IEnumerable<TechProductDocument> fetchedDocuments;
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

            Assert.Equal(2, fetchedDocuments.Count());
            Assert.Equal(documentId1, fetchedDocuments.ToList()[0].Id);
            Assert.Equal(documentId2, fetchedDocuments.ToList()[1].Id);
            Assert.Equal("IntegrationTest013", fetchedDocuments.ToList()[0].Name);
            Assert.Equal("IntegrationTest013", fetchedDocuments.ToList()[1].Name);
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
            ISearchResult<TechProductDocument> result;
            IEnumerable<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Sort(q => q.Id, false)
                .Sort(q => q.Name, true)
                .Execute();
            result.Document(out data);

            // Assert
            Assert.Equal(10, data.Count());
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
            IEnumerable<TechProductDocument> fetchedDocuments;
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

            Assert.Equal(2, fetchedDocuments.Count());
            Assert.Equal(documentId1, fetchedDocuments.ToList()[0].Id);
            Assert.Equal(documentId2, fetchedDocuments.ToList()[1].Id);
            Assert.Equal("IntegrationTest013", fetchedDocuments.ToList()[0].Name);
            Assert.Equal("IntegrationTest013", fetchedDocuments.ToList()[1].Name);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type boost) and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest016()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            ISearchResult<TechProductDocument> result;
            InformationResult<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Boost(new Any("inStock"), BoostFunctionType.Boost)
                .Execute();
            data = result.Get(new InformationResult<TechProductDocument>());

            // Assert
            Assert.True(data.Data.DocumentCount > 1);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type bf) and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest017()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            ISearchResult<TechProductDocument> result;
            InformationResult<TechProductDocument> data;

            // Act
            result = documentCollection
                .Select()
                .Query(new QueryAll())
                .Boost(new Any("inStock"), BoostFunctionType.Bf)
                .Execute();
            data = result.Get(new InformationResult<TechProductDocument>());

            // Assert
            Assert.True(data.Data.DocumentCount > 1);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Offset" and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a correct pagination
        /// </summary>
        [Fact]
        public void IntegrationTest018()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();
            IEnumerable<TechProductDocument> allDocuments;
            IEnumerable<TechProductDocument> documentsPage1;
            IEnumerable<TechProductDocument> documentsPage2;
            IEnumerable<TechProductDocument> documentsPage3;
            documentCollection
                .Select()
                .Query(new QueryAll())
                .Limit(20)
                .Execute()
                .Document(out allDocuments);

            // Act
            documentCollection
                .Select()
                .Query(new QueryAll())
                .Page(5, 1)
                .Execute()
                .Document(out documentsPage1);

            documentCollection
                .Select()
                .Query(new QueryAll())
                .Page(5, 2)
                .Execute()
                .Document(out documentsPage2);

            documentCollection
                .Select()
                .Query(new QueryAll())
                .Page(5, 3)
                .Execute()
                .Document(out documentsPage3);

            // Assert
            var listAllDocuments = allDocuments.ToList();
            Assert.Equal(listAllDocuments[0].Id, documentsPage1.ToList()[0].Id);
            Assert.Equal(listAllDocuments[5].Id, documentsPage2.ToList()[0].Id);
            Assert.Equal(listAllDocuments[10].Id, documentsPage3.ToList()[0].Id);
        }
    }
}
