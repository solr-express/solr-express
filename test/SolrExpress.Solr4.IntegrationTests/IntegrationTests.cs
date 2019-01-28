using Microsoft.Extensions.DependencyInjection;
using SolrExpress.DI.CoreClr;
using SolrExpress.Search.Extension;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Result.Extension;
using SolrExpress.Solr4.Extension;
using System;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        public IntegrationTests()
        {
            var services = new ServiceCollection()
                .AddSolrExpress<TechProductDocument>(builder => builder
                    .UseHostAddress("http://localhost:8983/solr/collection1")
                    .UseSolr4());

            this._serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Get a flesh instance of DocumentCollection<TechProductDocument> 
        /// </summary>
        /// <returns>Instance of DocumentCollection<TechProductDocument></returns>
        private DocumentCollection<TechProductDocument> GetDocumentCollection()
        {
            return this._serviceProvider.GetRequiredService<DocumentCollection<TechProductDocument>>();
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Execute()
                .Document(out var data);

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

            // Act
            documentCollection
                .Select()
                .Filter(q => q.InStock, "true")
                .Filter(q => q.ManufacturerId, "corsair")
                .Execute()
                .Document(out var data);

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

            // Act
            documentCollection
                .Select()
                .FacetField(q => q.ManufacturerId)
                .FacetField(q => q.InStock)
                .Execute()
                .Facets(out var data);

            // Assert
            Assert.Equal(2, data.Count());
            Assert.Contains(data, q => q.Name.Equals("ManufacturerId"));
            Assert.Contains(data, q => q.Name.Equals("InStock"));
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetQuery("Facet1", query => query.Field(q => q.Popularity).GreaterThan(10))
                .FacetQuery("Facet2", query => query.Field(q => q.Popularity).LessThan(10))
                .Execute()
                .Facets(out var data);

            // Assert
            Assert.Equal(2, data.Count());
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetRange("Facet1", q => q.Popularity, "1", "1", "10")
                .FacetRange("Facet2", q => q.Price, "10", "10", "1000")
                .FacetRange("Facet3", q => q.ManufacturedateIn, "+10DAYS", "NOW-30YEARS", "NOW+1DAY")
                .Execute()
                .Facets(out var data);

            // Assert
            Assert.Equal(3, data.Count());
            Assert.Contains(data, q => q.Name.Equals("Facet1"));
            Assert.Contains(data, q => q.Name.Equals("Facet2"));
            Assert.Contains(data, q => q.Name.Equals("Facet3"));
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Execute()
                .Information(out var data);

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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetField(q => q.ManufacturerId, facet => facet.Limit(1))
                .Execute()
                .Facets(out var data);

            // Assert
            Assert.Single(data);
            Assert.Equal("ManufacturerId", data.ToList()[0].Name);
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

            var documentId = Guid.NewGuid().ToString("N");
            var documentToAdd = new TechProductDocument
            {
                Id = documentId,
                Name = "IntegrationTest009"
            };

            // Act
            documentCollection
                .Update()
                .Add(documentToAdd)
                .Execute();

            // Assert
            documentCollection
                .Select()
                .Filter(q => q.Id, documentId)
                .Execute()
                .Document(out var data);

            Assert.Single(data);
            Assert.Equal(documentId, data.ToList()[0].Id);
            Assert.Equal("IntegrationTest009", data.ToList()[0].Name);
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

            var documentId1 = Guid.NewGuid().ToString("N");
            var documentId2 = Guid.NewGuid().ToString("N");
            var documentToAdd1 = new TechProductDocument
            {
                Id = documentId1,
                Name = "Some value1"
            };
            var documentToAdd2 = new TechProductDocument
            {
                Id = documentId2,
                Name = "Some value2"
            };
            documentCollection
                .Update()
                .Add(documentToAdd1)
                .Execute();

            documentCollection
                .Update()
                .Add(documentToAdd2)
                .Execute();

            // Act
            documentCollection
                .Update()
                .Delete(documentId1)
                .Execute();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, q => q.Any(documentId1, documentId2))
                .Execute()
                .Document(out var fetchedDocuments);

            Assert.Single(fetchedDocuments);
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Sort(q => q.Id, true)
                .Execute()
                .Document(out var data);

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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Sort(q => q.Id, false)
                .Sort(q => q.Name, true)
                .Execute()
                .Document(out var data);

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

            var documentId1 = Guid.NewGuid().ToString("N");
            var documentId2 = Guid.NewGuid().ToString("N");
            var documentToAdd1 = new TechProductDocument
            {
                Id = documentId1,
                Name = "Some value1"
            };
            var documentToAdd2 = new TechProductDocument
            {
                Id = documentId2,
                Name = "Some value2"
            };
            // Act
            documentCollection
                .Update()
                .Add(documentToAdd1)
                .Execute();

            documentCollection
                .Update()
                .Add(documentToAdd2)
                .Execute();

            // Assert
            documentCollection
                .Select()
                .Query(q => q.Id, q => q.Any(documentId1, documentId2))
                .Execute()
                .Document(out var fetchedDocuments);

            Assert.Equal(2, fetchedDocuments.Count());
            Assert.Contains(fetchedDocuments, q => q.Id.Equals(documentId1));
            Assert.Contains(fetchedDocuments, q => q.Id.Equals(documentId2));
            Assert.Contains(fetchedDocuments, q => q.Name.Equals(documentToAdd1.Name));
            Assert.Contains(fetchedDocuments, q => q.Name.Equals(documentToAdd2.Name));
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

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Sort(q => q.Id, false)
                .Sort(q => q.Name, true)
                .Execute()
                .Document(out var data);

            // Assert
            Assert.Equal(10, data.Count());
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type boost) and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest015()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Boost(query => query.Field(q => q.InStock))
                .Execute()
                .Information(out var data);

            // Assert
            Assert.True(data.DocumentCount > 1);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Boost" (type bf) and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest016()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Boost(query => query.Field(q => q.InStock), boost => boost.BoostFunctionType(BoostFunctionType.Bf))
                .Execute()
                .Information(out var data);

            // Assert
            Assert.True(data.DocumentCount > 1);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Offset" and result Information
        /// When    Invoking the method "Execute"
        /// What    Create a correct pagination
        /// </summary>
        [Fact]
        public void IntegrationTest017()
        {
            // Arrange
            this.GetDocumentCollection()
                .Select()
                .QueryAll()
                .Limit(20)
                .Execute()
                .Document(out var allDocuments);

            // Act
            this.GetDocumentCollection()
                .Select()
                .QueryAll()
                .Page(5, 1)
                .Execute()
                .Document(out var documentsPage1);

            this.GetDocumentCollection()
                .Select()
                .QueryAll()
                .Page(5, 2)
                .Execute()
                .Document(out var documentsPage2);

            this.GetDocumentCollection()
                .Select()
                .QueryAll()
                .Page(5, 3)
                .Execute()
                .Document(out var documentsPage3);

            // Assert
            var listAllDocuments = allDocuments.ToList();
            Assert.Equal(listAllDocuments[0].Id, documentsPage1.ToList()[0].Id);
            Assert.Equal(listAllDocuments[5].Id, documentsPage2.ToList()[0].Id);
            Assert.Equal(listAllDocuments[10].Id, documentsPage3.ToList()[0].Id);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Fields"
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR and populate only requested fields
        /// </summary>
        [Fact]
        public void IntegrationTest018()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Fields(q => q.Id, q => q.Name)
                .Sort(q => q.Id, false)
                .Limit(1)
                .Execute()
                .Document(out var documents);

            // Assert
            var documentList = documents.ToList();
            Assert.Single(documentList);
            Assert.NotEmpty(documentList[0].Id);
            Assert.Null(documentList[0].Name);
            Assert.Null(documentList[0].Manufacturer);
            Assert.Null(documentList[0].ManufacturerId);
            Assert.Null(documentList[0].Categories);
            Assert.Null(documentList[0].Features);
            Assert.Equal(default(GeoCoordinate), documentList[0].StoredAt);
            Assert.Equal(0, documentList[0].Price);
            Assert.Equal(0, documentList[0].Popularity);
            Assert.False(documentList[0].InStock);
            Assert.Equal(default(DateTime), documentList[0].ManufacturedateIn);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "FacetField" with excludes
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR and populate only requested fields
        /// </summary>
        [Fact]
        public void IntegrationTest019()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .Filter(q => q.Id, "TWINX2048-3200PRO", "sometag1")
                .Filter(q => q.ManufacturerId, "corsair", "sometag2")
                .FacetField(q => q.Categories, facet => facet.Excludes("sometag1", "sometag2"))
                .Execute()
                .Document(out var documents);

            // Assert
            var documentList = documents.ToList();
            Assert.Single(documentList);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "FacetField" with facet method
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR and populate only requested fields
        /// </summary>
        [Fact]
        public void IntegrationTest020()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetField(q => q.Categories, facet => facet.MethodType(FacetMethodType.DocValues))
                .FacetField(q => q.Features, facet => facet.MethodType(FacetMethodType.Stream))
                .FacetField(q => q.Manufacturer, facet => facet.MethodType(FacetMethodType.UninvertedField))
                .Execute()
                .Facets(out var facets);

            // Assert
            Assert.Equal(3, facets.Count());
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "FacetField" with facet prefix
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR and populate only requested fields
        /// </summary>
        [Fact]
        public void IntegrationTest021()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetField(q => q.Categories, facet => facet.Prefix("c"))
                .Execute()
                .Facets(out var facets);

            // Assert
            Assert.Single(facets);
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Facet.Range" (with HardEnd)
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest022()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .FacetRange("Facet1", q => q.Popularity, "1", "1", "10", facet => facet.HardEnd(true))
                .Execute()
                .Facets(out var data);

            // Assert
            Assert.Single(data);
            Assert.Contains(data, q => q.Name.Equals("Facet1"));
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter LocalParameter
        /// When    Invoking the method "Execute"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest023()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .QueryAll()
                .LocalParameter("parameter1", "my parameter")
                .Execute()
                .Document(out var data);

            // Assert
            Assert.Equal(10, data.Count());
        }

        /// <summary>
        /// Where   Creating a SOLR context, using parameter "Filter"
        /// When    Invoking the method "StartsWith"
        /// What    Create a communication between software and SOLR
        /// </summary>
        [Fact]
        public void IntegrationTest024()
        {
            // Arrange
            var documentCollection = this.GetDocumentCollection();

            // Act
            documentCollection
                .Select()
                .Filter(x => x.Categories, query => query.StartsWith("e"))
                .Execute()
                .Document(out var data);

            // Assert
            Assert.Equal(10, data.Count());
        }
    }
}
