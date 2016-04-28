using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Core.Update;
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

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DocumentCollection004()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            documentCollection.Add(null);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DocumentCollection005()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            documentCollection.Delete(null);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DocumentCollection006()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            documentCollection.Add(new TestDocument[] { });
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DocumentCollection007()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            documentCollection.Delete(new string[] { });
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes property Select twice
        /// What    Each invokes returns a different instance
        /// </summary>
        [TestMethod]
        public void DocumentCollection008()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act
            var select1 = documentCollection.Select;
            var select2 = documentCollection.Select;

            // Assert
            Assert.AreNotSame(select1, select2);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit without invokes method Add
        /// What    Don't invoke class than implements IAtomicUpdate
        /// </summary>
        [TestMethod]
        public void DocumentCollection009()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);

            // Act
            documentCollection.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicUpdate<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicUpdate
        /// </summary>
        [TestMethod]
        public void DocumentCollection010()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);
            documentCollection.Add(new TestDocument());

            // Act
            documentCollection.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicUpdate<TestDocument>>(), Times.Once);
        }






        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit without invokes method Add
        /// What    Don't invoke class than implements IAtomicDelete
        /// </summary>
        [TestMethod]
        public void DocumentCollection011()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);

            // Act
            documentCollection.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicDelete<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicDelete
        /// </summary>
        [TestMethod]
        public void DocumentCollection012()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);
            documentCollection.Delete("");

            // Act
            documentCollection.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicDelete<TestDocument>>(), Times.Once);
        }


    }
}
