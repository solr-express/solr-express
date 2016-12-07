using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Utility;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.ParameterValue
{
    public class RangeTests
    {
        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range001()
        {
            // Arrange
            var expected = "_id_:[* TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, int>(q => q.Id, null, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range002()
        {
            // Arrange
            var expected = "_id_:[* TO 1]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, int>(q => q.Id, null, 1);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range003()
        {
            // Arrange
            var expected = "_id_:[1 TO 10]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, int>(q => q.Id, 1, 10);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with int type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range004()
        {
            // Arrange
            var expected = "_id_:[1 TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, int>(q => q.Id, 1, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range005()
        {
            // Arrange
            var expected = "_id_:[* TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, decimal>(q => q.Id, null, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range006()
        {
            // Arrange
            var expected = "_id_:[* TO 1.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, decimal>(q => q.Id, null, 1.5M);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range007()
        {
            // Arrange
            var expected = "_id_:[1.5 TO 10.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, decimal>(q => q.Id, 1.5M, 10.5M);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with decimal type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range008()
        {
            // Arrange
            var expected = "_id_:[1.5 TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, decimal>(q => q.Id, 1.5M, null);
            parameter.ExpressionBuilder = expressionBuilder;            

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range009()
        {
            // Arrange
            var expected = "_id_:[* TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, double>(q => q.Id, null, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range010()
        {
            // Arrange
            var expected = "_id_:[* TO 1.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, double>(q => q.Id, null, 1.5D);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range011()
        {
            // Arrange
            var expected = "_id_:[1.5 TO 10.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, double>(q => q.Id, 1.5D, 10.5D);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with double type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range012()
        {
            // Arrange
            var expected = "_id_:[1.5 TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, double>(q => q.Id, 1.5D, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range013()
        {
            // Arrange
            var expected = "_id_:[* TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, null, null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range014()
        {
            // Arrange
            var expected = "_id_:[* TO 2015-09-13T10:00:00Z]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, null, new DateTime(2015, 09, 13, 10, 0, 0));
            parameter.ExpressionBuilder = expressionBuilder;
            
            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range015()
        {
            // Arrange
            var expected = "_id_:[2000-09-13T10:00:00Z TO 2015-09-13T10:00:00Z]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), new DateTime(2015, 09, 13, 10, 0, 0));
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with DateTime type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range016()
        {
            // Arrange
            var expected = "_id_:[2000-09-13T10:00:00Z TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, DateTime>(q => q.Id, new DateTime(2000, 09, 13, 10, 0, 0), null);
            parameter.ExpressionBuilder = expressionBuilder;
            
            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range017()
        {
            // Arrange
            var expected = "_id_:[* TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, null, null);
            parameter.ExpressionBuilder = expressionBuilder;
            
            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing null in "from" value and not null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range018()
        {
            // Arrange
            var expected = "_id_:[* TO -10.5,10.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, null, new GeoCoordinate(-10.5M, 10.5M));
            parameter.ExpressionBuilder = expressionBuilder;
            
            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" and "to" values
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range019()
        {
            // Arrange
            var expected = "_id_:[-1.5,1.5 TO -10.5,10.5]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), new GeoCoordinate(-10.5M, 10.5M));
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with GeoCoordinate type in value, passing not null in "from" value and null in "to" value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Range020()
        {
            // Arrange
            var expected = "_id_:[-1.5,1.5 TO *]";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Range<TestDocument, GeoCoordinate>(q => q.Id, new GeoCoordinate(-1.5M, 1.5M), null);
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Range instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Range021()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Range<TestDocument, int>(null));
        }
    }
}
