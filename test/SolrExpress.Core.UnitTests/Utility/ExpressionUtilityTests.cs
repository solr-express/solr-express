using SolrExpress.Core.Utility;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests.Utility
{
    public class ExpressionUtilityTests
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

            [SolrField("PropString")]
            public string PropertyStringWithAttr { get; set; }

            [SolrField("PropInt")]
            public int PropertyIntWithAttr { get; set; }

            [SolrField("PropLong")]
            public long PropertyLongWithAttr { get; set; }

            [SolrField("PropDateTime")]
            public DateTime PropertyDateTimeWithAttr { get; set; }

            [SolrField("PropDateTimeOffset")]
            public DateTimeOffset PropertyDateTimeOffsetWithAttr { get; set; }

            [SolrField("PropBool")]
            public bool PropertyBoolWithAttr { get; set; }
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a string property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility001()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyString);

            // Assert
            Assert.Equal("PropertyString", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a int property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility002()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyInt);

            // Assert
            Assert.Equal("PropertyInt", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a long property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility003()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyLong);

            // Assert
            Assert.Equal("PropertyLong", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTime property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility004()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTime);

            // Assert
            Assert.Equal("PropertyDateTime", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTimeOffset property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility005()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTimeOffset);

            // Assert
            Assert.Equal("PropertyDateTimeOffset", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a bool property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility006()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyBool);

            // Assert
            Assert.Equal("PropertyBool", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable int property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility007()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyIntNullable);

            // Assert
            Assert.Equal("PropertyIntNullable", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable long property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility008()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyLongNullable);

            // Assert
            Assert.Equal("PropertyLongNullable", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable DateTime property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility009()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTimeNullable);

            // Assert
            Assert.Equal("PropertyDateTimeNullable", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable DateTimeOffset property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility010()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTimeOffsetNullable);

            // Assert
            Assert.Equal("PropertyDateTimeOffsetNullable", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a nullable bool property
        /// What    Return the name of the property
        /// </summary>
        [Fact]
        public void ExpressionUtility011()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyBoolNullable);

            // Assert
            Assert.Equal("PropertyBoolNullable", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a string property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility012()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyStringWithAttr);

            // Assert
            Assert.Equal("PropString", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a int property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility013()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyIntWithAttr);

            // Assert
            Assert.Equal("PropInt", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a long property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility014()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyLongWithAttr);

            // Assert
            Assert.Equal("PropLong", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTime property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility015()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTimeWithAttr);

            // Assert
            Assert.Equal("PropDateTime", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a DateTimeOffset property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility016()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyDateTimeOffsetWithAttr);

            // Assert
            Assert.Equal("PropDateTimeOffset", name);
        }

        /// <summary>
        /// Where   Using ExpressionUtility class
        /// When    Invoking the method "GetPropertyNameFromExpression" using a lambda with a bool property with SolrFieldAttribute
        /// What    Return the name of the SolrFieldAttribute associeted with the property
        /// </summary>
        [Fact]
        public void ExpressionUtility017()
        {
            // Arrange
            string name;

            // Act
            name = ExpressionUtility.GetFieldNameFromExpression<Document>(q => q.PropertyBoolWithAttr);

            // Assert
            Assert.Equal("PropBool", name);
        }
    }
}
