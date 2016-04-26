using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace SolrExpress.Core.Tests
{
    [TestClass]
    public class DocumentCollectionTests
    {
        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null in provider parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DocumentCollection001()
        {
            // Arrange / Act / Assert
            new DocumentCollection<TestDocument>(null, new Mock<IResolver>().Object, new Configuration());
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null in resolver parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DocumentCollection002()
        {
            // Arrange / Act / Assert
            new DocumentCollection<TestDocument>(new Mock<IProvider>().Object, null, new Configuration());
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DocumentCollection003()
        {
            // Arrange / Act / Assert
            new DocumentCollection<TestDocument>(new Mock<IProvider>().Object, new Mock<IResolver>().Object, null);
        }
    }
}
