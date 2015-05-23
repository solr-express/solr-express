using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Exception;
using SolrExpress.QueryBuilder;
using SolrExpress.QueryBuilder.Parameter.Solr5;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolrExpress.Tests.QueryBuilder
{
    /// <summary>
    /// Unit tests to SolrQueryable
    /// </summary>
    [TestClass]
    public class SolrQueryableTests
    {
        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of TestParameter twice
        /// What    Throws error
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AllowMultipleInstanceOfParameterType))]
        public void SolrQueryable001()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var resultDataresultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            var mockParameter = new Mock<IQueryParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstance).Returns(false);
            mockParameter.Setup(q => q.ParameterName).Returns("mock");
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataresultDataBuilderMock.Object);
            queryable.Add(mockParameter.Object);

            // Act / Assert
            queryable.Add(mockParameter.Object);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of QueryParameter
        /// What    JsonQuery created with query:"Id:ITEM001" argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable002()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable002.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new QueryParameter<TestDocument>(q => q.Id, "ITEM01"));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of FieldsParameter
        /// What    JsonQuery created with fields:["Id","Score"] argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable003()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable003.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new FieldsParameter<TestDocument>(q => q.Id));
            queryable.Add(new FieldsParameter<TestDocument>(q => q.Score));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of FilterParameter
        /// What    JsonQuery created with filter:["Id:X", "Score:Y"] argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable004()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable004.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new FilterParameter<TestDocument>(q => q.Id, "X"));
            queryable.Add(new FilterParameter<TestDocument>(q => q.Score, "Y"));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of QueryParameter
        /// What    JsonQuery created with facet={Id:{type:terms,field:Id}} argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable005()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable005.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new FacetFieldParameter<TestDocument>(q => q.Id));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of QueryParameter
        /// What    JsonQuery created with facet={X:{type:terms,field:X,sort:{index:desc}}} argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable006()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable006.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.Quantity, false));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of QueryParameter
        /// What    JsonQuery created with facet={X:{type:terms,field:X}} argument
        /// </summary>
        [TestMethod]
        public void SolrQueryable007()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable007.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var providerMock = new Mock<IProvider>();
            var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
            string jsonExpression;
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
            queryable.Add(new FacetQueryParameter("X", "avg('Y')"));

            // Act
            queryable.GetJson();
            jsonExpression = queryable._expression;

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        ///// <summary>
        ///// Where   Using an instance of the class SolrQueryable
        ///// When    Invoke the "Add" method with a instance of QueryParameter
        ///// What    JsonQuery created with facet={X:{type:terms,field:X,sort:{index:desc}}} argument
        ///// </summary>
        //[TestMethod]
        //public void SolrQueryable008()
        //{
        //    // Arrange
        //    var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable07.txt");
        //    var jsonStr = File.ReadAllText(jsonFilePath);
        //    var providerMock = new Mock<IProvider>();
        //    var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
        //    string jsonExpression;
        //    var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
        //    queryable.FacetQuery("X", "avg('Y')", SolrFacetSortType.Quantity, false);

        //    // Act
        //    queryable.GetJson();
        //    jsonExpression = queryable._expression;

        //    // Assert
        //    Assert.AreEqual(jsonStr, jsonExpression);
        //}

        ///// <summary>
        ///// Where   Using an instance of the class SolrQueryable
        ///// When    Invoke the "Add" method with a instance of QueryParameter
        ///// What    JsonQuery created with facet={X:{type:terms,field:X}} argument
        ///// </summary>
        //[TestMethod]
        //public void SolrQueryable009()
        //{
        //    // Arrange
        //    var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable08.txt");
        //    var jsonStr = File.ReadAllText(jsonFilePath);
        //    var providerMock = new Mock<IProvider>();
        //    var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
        //    string jsonExpression;
        //    var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
        //    queryable.FacetRange("X", "X", "1", "10", "20");

        //    // Act
        //    queryable.GetJson();
        //    jsonExpression = queryable._expression;

        //    // Assert
        //    Assert.AreEqual(jsonStr, jsonExpression);
        //}

        ///// <summary>
        ///// Where   Using an instance of the class SolrQueryable
        ///// When    Invoke the "Add" method with a instance of QueryParameter
        ///// What    JsonQuery created with facet={X:{type:terms,field:X,sort:{index:desc}}} argument
        ///// </summary>
        //[TestMethod]
        //public void SolrQueryable010()
        //{
        //    // Arrange
        //    var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable09.txt");
        //    var jsonStr = File.ReadAllText(jsonFilePath);
        //    var providerMock = new Mock<IProvider>();
        //    var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
        //    string jsonExpression;
        //    var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
        //    queryable.FacetRange("X", "X", "1", "10", "20", SolrFacetSortType.Quantity, false);

        //    // Act
        //    queryable.GetJson();
        //    jsonExpression = queryable._expression;

        //    // Assert
        //    Assert.AreEqual(jsonStr, jsonExpression);
        //}

        ///// <summary>
        ///// Where   Using an instance of the class SolrQueryable
        ///// When    Invoke the "Add" method with a instance of QueryParameter
        ///// What    JsonQuery created with offset:0 argument
        ///// </summary>
        //[TestMethod]
        //public void SolrQueryable011()
        //{
        //    // Arrange
        //    var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable10.txt");
        //    var jsonStr = File.ReadAllText(jsonFilePath);
        //    var providerMock = new Mock<IProvider>();
        //    var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
        //    string jsonExpression;
        //    var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
        //    queryable.Add(new OffsetParameter(10));

        //    // Act
        //    queryable.GetJson();
        //    jsonExpression = queryable._expression;

        //    // Assert
        //    Assert.AreEqual(jsonStr, jsonExpression);
        //}

        ///// <summary>
        ///// Where   Using an instance of the class SolrQueryable
        ///// When    Invoke the "Add" method with a instance of QueryParameter
        ///// What    JsonQuery created with limit:0 argument
        ///// </summary>
        //[TestMethod]
        //public void SolrQueryable012()
        //{
        //    // Arrange
        //    var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "SolrQueryable11.txt");
        //    var jsonStr = File.ReadAllText(jsonFilePath);
        //    var providerMock = new Mock<IProvider>();
        //    var resultDataBuilderMock = new Mock<IResultDataBuilder<TestDocument>>();
        //    string jsonExpression;
        //    var queryable = new SolrQueryable<TestDocument>(providerMock.Object, resultDataBuilderMock.Object);
        //    queryable.Add(new LimitParameter(10));

        //    // Act
        //    queryable.GetJson();
        //    jsonExpression = queryable._expression;

        //    // Assert
        //    Assert.AreEqual(jsonStr, jsonExpression);
        //}
    }
}
