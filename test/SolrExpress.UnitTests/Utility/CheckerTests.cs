using SolrExpress.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.UnitTests.Utility
{
    public class CheckerTests
    {
        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsTrue with true
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker001()
        {
            // Arrange
            var condition = true;

            // Act / Assert
            Assert.Throws<Exception>(() => Checker.IsTrue<Exception>(condition));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsTrue with false
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker002()
        {
            // Arrange
            var condition = false;

            // Act / Assert
            Checker.IsTrue<Exception>(condition);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNull with null
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker003()
        {
            // Arrange
            var value = (object)null;

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => Checker.IsNull(value));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNull with a instance of object
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker004()
        {
            // Arrange
            var value = new object();

            // Act / Assert
            Checker.IsNull(value);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNullOrWhiteSpace with null
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker005()
        {
            // Arrange
            var value = (string)null;

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => Checker.IsNullOrWhiteSpace(value));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNullOrWhiteSpace with a empty string
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker006()
        {
            // Arrange
            var value = "";

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => Checker.IsNullOrWhiteSpace(value));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNullOrWhiteSpace with a whitespace string
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker007()
        {
            // Arrange
            var value = " ";

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => Checker.IsNullOrWhiteSpace(value));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsNullOrWhiteSpace with a string with value
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker008()
        {
            // Arrange
            var value = ".";

            // Act / Assert
            Checker.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsOutOfRange with value = 10 and lower bound value = 11
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker009()
        {
            // Arrange
            decimal value = 10M;
            decimal minValue = 11M;
            decimal maxValue = 20M;
            string erroMessage = "test";

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Checker.IsOutOfRange(value, minValue, maxValue, erroMessage));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsOutOfRange with value = 10 and lower bound value = 10
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker010()
        {
            // Arrange
            decimal value = 10M;
            decimal minValue = 10M;
            decimal maxValue = 20M;
            string erroMessage = "test";

            // Act / Assert
            Checker.IsOutOfRange(value, minValue, maxValue, erroMessage);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsOutOfRange with value = 21 and upper bound value = 20
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker011()
        {
            // Arrange
            decimal value = 21M;
            decimal minValue = 10M;
            decimal maxValue = 20M;
            string erroMessage = "test";

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Checker.IsOutOfRange(value, minValue, maxValue, erroMessage));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsOutOfRange with value = 20 and upper bound value = 20
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker012()
        {
            // Arrange
            decimal value = 20M;
            decimal minValue = 10M;
            decimal maxValue = 20M;
            string erroMessage = "test";

            // Act / Assert
            Checker.IsOutOfRange(value, minValue, maxValue, erroMessage);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsLowerThan with value = 10 and min value = 11
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker013()
        {
            // Arrange
            decimal value = 10M;
            decimal minValue = 11M;

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Checker.IsLowerThan(value, minValue));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsOutOfRange with value = 10 and upper bound value = 10
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker014()
        {
            // Arrange
            decimal value = 10M;
            decimal minValue = 10M;

            // Act / Assert
            Checker.IsLowerThan(value, minValue);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsEmpty with a empty collection
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker015()
        {
            // Arrange
            ICollection collection = new List<int>();

            // Act / Assert
            Assert.Throws<ArgumentException>(() => Checker.IsEmpty(collection));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsEmpty with a collection with one element
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker016()
        {
            // Arrange
            ICollection collection = new List<int> { 1 };

            // Act / Assert
            Checker.IsEmpty(collection);
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsFalse with false
        /// What    Throws exception
        /// </summary>
        [Fact]
        public void Checker017()
        {
            // Arrange
            var condition = false;

            // Act / Assert
            Assert.Throws<Exception>(() => Checker.IsFalse<Exception>(condition));
        }

        /// <summary>
        /// Where   Using a Checker class
        /// When    Invoke IsFalse with true
        /// What    Not throws exception
        /// </summary>
        [Fact]
        public void Checker018()
        {
            // Arrange
            var condition = true;

            // Act / Assert
            Checker.IsFalse<Exception>(condition);
        }
    }
}
