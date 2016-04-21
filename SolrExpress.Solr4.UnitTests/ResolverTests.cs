using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Result;
using SolrExpress.Solr4.Result;

namespace SolrExpress.Solr4.UnitTests
{
    [TestClass]
    public class ResolverTests
    {
        /// <summary>
        /// Where   Using a Resolver instance
        /// When    Invoking the method "Get" using interface IDocumentResult
        /// What    Returns concrete class than implements IDocumentResult interface
        /// </summary>
        [TestMethod]
        public void Resolver001()
        {
            // Arrange
            var resolver = new Resolver();

            // Act
            var result = resolver.GetInstance<IDocumentResult<TestDocument>>();

            // Assert
            Assert.IsInstanceOfType(result, typeof(DocumentResult<TestDocument>));
        }
    }
}
