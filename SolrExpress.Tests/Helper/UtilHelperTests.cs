using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Attribute;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Tests.Helper
{
    [TestClass]
    public class UtilHelperTests
    {
        private class Document : IDocument
        {
            public string PropertyString { get; set; }

            public int PropertyInt { get; set; }

            public long PropertyLong { get; set; }

            public DateTime PropertyDateTime { get; set; }

            public DateTimeOffset PropertyDateTimeOffset { get; set; }

            public bool PropertyBool { get; set; }

            public int? PropertyIntNullable { get; set; }

            public long? PropertyLongNullable { get; set; }

            public DateTime? PropertyDateTimeNullable { get; set; }

            public DateTimeOffset? PropertyDateTimeOffsetNullable { get; set; }

            public bool? PropertyBoolNullable { get; set; }

            [SolrFieldAtribute("PropString")]
            public string PropertyStringWithAttr { get; set; }

            [SolrFieldAtribute("PropInt")]
            public int PropertyIntWithAttr { get; set; }

            [SolrFieldAtribute("PropLong")]
            public long PropertyLongWithAttr { get; set; }

            [SolrFieldAtribute("PropDateTime")]
            public DateTime PropertyDateTimeWithAttr { get; set; }

            [SolrFieldAtribute("PropDateTimeOffset")]
            public DateTimeOffset PropertyDateTimeOffsetWithAttr { get; set; }

            [SolrFieldAtribute("PropBool")]
            public bool PropertyBoolWithAttr { get; set; }
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a string property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper001()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyString);

            // Assert
            Assert.AreEqual("PropertyString", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a int property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper002()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyInt);

            // Assert
            Assert.AreEqual("PropertyInt", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a long property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper003()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyLong);

            // Assert
            Assert.AreEqual("PropertyLong", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTime property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper004()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTime);

            // Assert
            Assert.AreEqual("PropertyDateTime", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTimeOffset property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper005()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTimeOffset);

            // Assert
            Assert.AreEqual("PropertyDateTimeOffset", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a bool property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper006()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyBool);

            // Assert
            Assert.AreEqual("PropertyBool", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable int property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper007()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyIntNullable);

            // Assert
            Assert.AreEqual("PropertyIntNullable", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable long property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper008()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyLongNullable);

            // Assert
            Assert.AreEqual("PropertyLongNullable", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable DateTime property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper009()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTimeNullable);

            // Assert
            Assert.AreEqual("PropertyDateTimeNullable", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable DateTimeOffset property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper010()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTimeOffsetNullable);

            // Assert
            Assert.AreEqual("PropertyDateTimeOffsetNullable", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable bool property
        /// What    Return the name of the property
        /// </summary>
        [TestMethod]
        public void UtilHelper011()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyBoolNullable);

            // Assert
            Assert.AreEqual("PropertyBoolNullable", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a string property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper012()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyStringWithAttr);

            // Assert
            Assert.AreEqual("PropString", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a int property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper013()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyIntWithAttr);

            // Assert
            Assert.AreEqual("PropInt", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a long property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper014()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyLongWithAttr);

            // Assert
            Assert.AreEqual("PropLong", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTime property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper015()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTimeWithAttr);

            // Assert
            Assert.AreEqual("PropDateTime", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTimeOffset property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper016()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyDateTimeOffsetWithAttr);

            // Assert
            Assert.AreEqual("PropDateTimeOffset", name);
        }

        /// <summary>
        /// Where   Using UtilHelper class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a bool property with SolrFieldAtribute
        /// What    Return the name of the SolrFieldAtribute associeted with the property
        /// </summary>
        [TestMethod]
        public void UtilHelper017()
        {
            // Arrange
            string name;

            // Act
            name = UtilHelper.GetPropertyNameFromExpression<Document>(q => q.PropertyBoolWithAttr);

            // Assert
            Assert.AreEqual("PropBool", name);
        }
    }
}
