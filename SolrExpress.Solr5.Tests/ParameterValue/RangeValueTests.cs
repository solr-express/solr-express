using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.ParameterValue;
using System;

namespace SolrExpress.Solr5.Tests.ParameterValue
{
    [TestClass]
    public class RangeValueTests
    {
        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with int type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue001()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, int>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with int type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue002()
        {
            // Arrange
            var expected = "Id:[* TO 1]";
            string actual;
            var parameter = new RangeValue<TestDocument, int>(q => q.Id, null, 1);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with int type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue003()
        {
            // Arrange
            var expected = "Id:[1 TO 10]";
            string actual;
            var parameter = new RangeValue<TestDocument, int>(q => q.Id, 1, 10);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with int type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue004()
        {
            // Arrange
            var expected = "Id:[1 TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, int>(q => q.Id, 1, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with decimal type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue005()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, decimal>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with decimal type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue006()
        {
            // Arrange
            var expected = "Id:[* TO 1.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, decimal>(q => q.Id, null, 1.5M);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with decimal type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue007()
        {
            // Arrange
            var expected = "Id:[1.5 TO 10.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, decimal>(q => q.Id, 1.5M, 10.5M);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with decimal type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue008()
        {
            // Arrange
            var expected = "Id:[1.5 TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, decimal>(q => q.Id, 1.5M, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with double type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue009()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, double>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with double type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue010()
        {
            // Arrange
            var expected = "Id:[* TO 1.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, double>(q => q.Id, null, 1.5D);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with double type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue011()
        {
            // Arrange
            var expected = "Id:[1.5 TO 10.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, double>(q => q.Id, 1.5D, 10.5D);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with double type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue012()
        {
            // Arrange
            var expected = "Id:[1.5 TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, double>(q => q.Id, 1.5D, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with DateTime type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue013()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, DateTime>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with DateTime type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue014()
        {
            // Arrange
            var expected = "Id:[* TO 2015-09-13T10:00:00Z]";
            string actual;
            var parameter = new RangeValue<TestDocument, DateTime>(q => q.Id, null, new DateTime(2015, 09, 13, 10, 0, 0));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue015()
        {
            // Arrange
            var expected = "Id:[2000-09-13T10:00:00Z TO 2015-09-13T10:00:00Z]";
            string actual;
            var parameter = new RangeValue<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), new DateTime(2015, 09, 13, 10, 0, 0));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue016()
        {
            // Arrange
            var expected = "Id:[2000-09-13T10:00:00Z TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue017()
        {
            // Arrange
            var expected = "Id:[* TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, GeoCoordinate>(q => q.Id, null, null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue018()
        {
            // Arrange
            var expected = "Id:[* TO -10.5,10.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, GeoCoordinate>(q => q.Id, null, new GeoCoordinate(-10.5M, 10.5M));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue019()
        {
            // Arrange
            var expected = "Id:[-1.5,1.5 TO -10.5,10.5]";
            string actual;
            var parameter = new RangeValue<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), new GeoCoordinate(-10.5M, 10.5M));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a RangeValue instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RangeValue020()
        {
            // Arrange
            var expected = "Id:[-1.5,1.5 TO *]";
            string actual;
            var parameter = new RangeValue<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), null);

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
