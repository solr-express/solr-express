using Moq;
using SolrExpress.Core.Update;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests
{
    public class DocumentCollectionTests
    {
        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Create the instance with null in options
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection001()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new DocumentCollection<TestDocument>(null, null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection002()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => documentCollection.Update().Add(null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection003()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => documentCollection.Update().Delete(null));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Add with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection004()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => documentCollection.Update().Add(new TestDocument[] { }));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Remove with a empty collection
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void DocumentCollection005()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => documentCollection.Update().Delete(new string[] { }));
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes property Select twice
        /// What    Each invokes returns a different instance
        /// </summary>
        [Fact]
        public void DocumentCollection006()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

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
        public void DocumentCollection007()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act
            documentCollection.Update().Commit();

            // Assert
            engine.Verify(q => q.GetService<IAtomicUpdate<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicUpdate
        /// </summary>
        [Fact]
        public void DocumentCollection008()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);
            var update = documentCollection.Update().Add(new TestDocument());

            // Act
            update.Commit();

            // Assert
            engine.Verify(q => q.GetService<IAtomicUpdate<TestDocument>>(), Times.Once);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit without invokes method Add
        /// What    Don't invoke class than implements IAtomicDelete
        /// </summary>
        [Fact]
        public void DocumentCollection009()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);

            // Act
            documentCollection.Update().Commit();

            // Assert
            engine.Verify(q => q.GetService<IAtomicDelete<TestDocument>>(), Times.Never);
        }

        /// <summary>
        /// Where   Using a DocumentCollection instance
        /// When    Invokes method Commit invoking method Add before
        /// What    Invoke class than implements IAtomicDelete
        /// </summary>
        [Fact]
        public void DocumentCollection010()
        {
            // Arrange
            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
            engine.Setup(q => q.GetService<IAtomicDelete<TestDocument>>()).Returns(new Mock<IAtomicDelete<TestDocument>>().Object);
            engine.Setup(q => q.GetService<IAtomicUpdate<TestDocument>>()).Returns(new Mock<IAtomicUpdate<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var documentCollection = new DocumentCollection<TestDocument>(options, engine.Object);
            var update = documentCollection.Update().Delete("");

            // Act
            update.Commit();

            // Assert
            engine.Verify(q => q.GetService<IAtomicDelete<TestDocument>>(), Times.Once);
        }
    }
}
