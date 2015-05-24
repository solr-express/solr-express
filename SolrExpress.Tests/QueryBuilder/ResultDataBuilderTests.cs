using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Exception;
using SolrExpress.Solr5.Builder;
using System.Collections.Generic;

namespace SolrExpress.Tests.QueryBuilder
{
    [TestClass]
    public class ResultDataBuilderTests
    {
        [TestMethod]
        public void WhenExecuteTheParseToConcretClassesUsingAValidJson_ParseCorrect()
        {
            // Arrange
            var jsonStr = @"
            {
              ""response"":{""numFound"":7722,""start"":0,""maxScore"":1.0,""docs"":[
                  {
                    ""id"":""ITEM01"",
                    ""score"":1.5}]
              }
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new DocsResultBuilder<TestDocument>();
            List<TestDocument> lst;

            // Act
            builder.Execute(jsonObject);
            lst = builder.Documents;

            // Assert
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual("ITEM01", lst[0].Id);
            Assert.AreEqual(1.5M, lst[0].Score);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void WhenExecuteTheParseToConcretClassesUsingAnInvalidJson_ThrowsException()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseX"":{""numFound"":7722,""start"":0,""maxScore"":1.0,""docs"":[
                  {
                    ""id"":""ITEM01"",
                    ""score"":1.5}]
              }
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new DocsResultBuilder<TestDocument>();

            // Act / Assert
            builder.Execute(jsonObject);
        }
    }
}
