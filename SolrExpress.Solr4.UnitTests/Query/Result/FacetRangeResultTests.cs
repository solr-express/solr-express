using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Result;
using SolrExpress.Solr4.Query.Result;
using System;
using System.Linq;

namespace SolrExpress.Solr4.UnitTests.Query.Result
{
    [TestClass]
    public class FacetRangeResultTests
    {
        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with integer values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                          ""1"",2,
                          ""2"",0,
                          ""3"",0,
                          ""4"",0,
                          ""5"",0,
                          ""6"",0,
                          ""7"",1,
                          ""8"",0,
                          ""9"",1],
                        ""gap"":10.0,
                        ""start"":10.0,
                        ""end"":100.0,
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(11, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<int>));
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with date time values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult002()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                            ""2014-06-22T20:33:00.741Z"",10,
                            ""2014-06-28T20:33:00.741Z"",0,
                            ""2014-07-04T20:33:00.741Z"",10],
                        ""gap"":""+6DAYS"",
                        ""start"":""2014-06-22T20:33:00.741Z"",
                        ""end"":""2015-06-23T20:33:00.741Z"",
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(5, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<DateTime>));
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with float values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult003()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                          ""10.0"",2,
                          ""20.0"",0,
                          ""30.0"",0,
                          ""40.0"",0,
                          ""50.0"",0,
                          ""60.0"",0,
                          ""70.0"",1,
                          ""80.0"",0,
                          ""90.0"",1],
                        ""gap"":10.5,
                        ""start"":10.0,
                        ""end"":100.0,
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(11, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<float>));
        }
    }
}
