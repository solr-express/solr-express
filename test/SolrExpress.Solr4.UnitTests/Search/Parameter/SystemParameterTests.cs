using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class SystemParameterTests
    {
        /// <summary>
        /// Where   Using a SystemParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SystemParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SystemParameter<TestDocument>();
            parameter.Configure();

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal("echoParams=none", container[0]);
            Assert.Equal("wt=json", container[1]);
            Assert.Equal("indent=off", container[2]);
            Assert.Equal("defType=edismax", container[3]);
            Assert.Equal("q.alt=*:*", container[4]);
            Assert.Equal("df=id", container[5]);
            Assert.Equal("fl=*,score", container[6]);
            Assert.Equal("sort=score desc", container[7]);
            Assert.Equal("q=*:*", container[8]);
        }
    }
}
