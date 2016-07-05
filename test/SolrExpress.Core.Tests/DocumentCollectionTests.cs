using Moq;
using SolrExpress.Core.Update;
using System;
using Xunit;

namespace SolrExpress.Core.Tests
{
    public class DocumentCollectionTests
    {
        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null in provider parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection001()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new DocumentCollection<TestDocument>(null, new Mock<IResolver>().Object, new Configuration()));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null in resolver parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection002()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new DocumentCollection<TestDocument>(new Mock<IProvider>().Object, null, new Configuration()));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection003()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new DocumentCollection<TestDocument>(new Mock<IProvider>().Object, new Mock<IResolver>().Object, null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection004()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => documentCollection.Update().Add(null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection005()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => documentCollection.Update().Delete(null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection006()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => documentCollection.Update().Add(new TestDocument[] { }));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection007()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => documentCollection.Update().Delete(new string[] { }));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes property Select twice
        /// What    Each invokes returns a different instance
        /// </summary>
        [Fact]
        public void DocumentCollection008()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>().Object;
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver, configuration);

            // Act
            var select1 = documentCollection.Select();
            var select2 = documentCollection.Select();

            // Assert
            Assert.NotSame(select1, select2);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit without invokes method Add
        /// What    Don't invoke class than implements IAtomicUpdate
        /// </summary>
        [Fact]
        public void DocumentCollection009()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);

            // Act
            documentCollection.Update().Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicUpdate<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicUpdate
        /// </summary>
        [Fact]
        public void DocumentCollection010()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);
            var update = documentCollection.Update().Add(new TestDocument());

            // Act
            update.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicUpdate<TestDocument>>(), Times.Once);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit without invokes method Add
        /// What    Don't invoke class than implements IAtomicDelete
        /// </summary>
        [Fact]
        public void DocumentCollection011()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);

            // Act
            documentCollection.Update().Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicDelete<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicDelete
        /// </summary>
        [Fact]
        public void DocumentCollection012()
        {
            // Arrange
            var provider = new Mock<IProvider>().Object;
            var resolver = new Mock<IResolver>();
            resolver.Setup(q => q.GetInstance<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            var configuration = new Configuration();
            var documentCollection = new DocumentCollection<TestDocument>(provider, resolver.Object, configuration);
            var update = documentCollection.Update().Delete("");

            // Act
            update.Commit();

            // Assert
            resolver.Verify(q => q.GetInstance<IAtomicDelete<TestDocument>>(), Times.Once);
        }
    }
}
