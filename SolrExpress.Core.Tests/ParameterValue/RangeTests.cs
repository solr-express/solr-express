using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class RangeTests
    {
        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range001()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new Range<TestDocument, int>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range002()
        {
            // Arrange
            var expected = "Id:[* TO 1]";
            string actual;
            var parameter = new Range<TestDocument, int>(q => q.Id, null, 1);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range003()
        {
            // Arrange
            var expected = "Id:[1 TO 10]";
            string actual;
            var parameter = new Range<TestDocument, int>(q => q.Id, 1, 10);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range004()
        {
            // Arrange
            var expected = "Id:[1 TO *]";
            string actual;
            var parameter = new Range<TestDocument, int>(q => q.Id, 1, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range005()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new Range<TestDocument, decimal>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range006()
        {
            // Arrange
            var expected = "Id:[* TO 1.5]";
            string actual;
            var parameter = new Range<TestDocument, decimal>(q => q.Id, null, 1.5M);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range007()
        {
            // Arrange
            var expected = "Id:[1.5 TO 10.5]";
            string actual;
            var parameter = new Range<TestDocument, decimal>(q => q.Id, 1.5M, 10.5M);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range008()
        {
            // Arrange
            var expected = "Id:[1.5 TO *]";
            string actual;
            var parameter = new Range<TestDocument, decimal>(q => q.Id, 1.5M, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range009()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new Range<TestDocument, double>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range010()
        {
            // Arrange
            var expected = "Id:[* TO 1.5]";
            string actual;
            var parameter = new Range<TestDocument, double>(q => q.Id, null, 1.5D);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range011()
        {
            // Arrange
            var expected = "Id:[1.5 TO 10.5]";
            string actual;
            var parameter = new Range<TestDocument, double>(q => q.Id, 1.5D, 10.5D);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range012()
        {
            // Arrange
            var expected = "Id:[1.5 TO *]";
            string actual;
            var parameter = new Range<TestDocument, double>(q => q.Id, 1.5D, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range013()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range014()
        {
            // Arrange
            var expected = "Id:[* TO 2015-09-13T10:00:00Z]";
            string actual;
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, null, new DateTime(2015, 09, 13, 10, 0, 0));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range015()
        {
            // Arrange
            var expected = "Id:[2000-09-13T10:00:00Z TO 2015-09-13T10:00:00Z]";
            string actual;
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), new DateTime(2015, 09, 13, 10, 0, 0));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range016()
        {
            // Arrange
            var expected = "Id:[2000-09-13T10:00:00Z TO *]";
            string actual;
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range017()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range018()
        {
            // Arrange
            var expected = "Id:[* TO -10.5,10.5]";
            string actual;
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, null, new GeoCoordinate(-10.5M, 10.5M));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range019()
        {
            // Arrange
            var expected = "Id:[-1.5,1.5 TO -10.5,10.5]";
            string actual;
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), new GeoCoordinate(-10.5M, 10.5M));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Range020()
        {
            // Arrange
            var expected = "Id:[-1.5,1.5 TO *]";
            string actual;
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Range021()
        {
            // Arrange / Act / Assert
            new Range<TestDocument, int>(null);
        }
    }
}
