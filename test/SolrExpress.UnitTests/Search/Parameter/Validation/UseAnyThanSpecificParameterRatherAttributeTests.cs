using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using System;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter.Validation
{
    public sealed class IsAnyParameter : IAnyParameter, ISearchParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(ISearchParameter other)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class IsNotAnyParameter : ISearchParameter
    {
        public bool Equals(ISearchParameter other)
        {
            throw new NotImplementedException();
        }
    }

    public class UseAnyThanSpecificParameterRatherAttributeTests
    {
        /// <summary>
        /// Where   Using an instance of a class that not implements IAnyParameter
        /// When    Validate this instance in method DocumentSearch<T>.ValidateSearchItem using SolrExpressOptions.CheckAnyParameter=true
        /// What    Returns true
        /// </summary>
        [Fact]
        public void UseAnyThanSpecificParameterRatherAttributeFact001()
        {
            // Arrange
            var attribute = new UseAnyThanSpecificParameterRatherAttribute();
            var parameter = new IsNotAnyParameter();

            // Act
            var result = attribute.IsValid<TestDocument>(parameter, out string errorMessage);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using an instance of a class that implements IAnyParameter, and value property Name is a GUID
        /// When    Validate this instance in method DocumentSearch<T>.ValidateSearchItem using SolrExpressOptions.CheckAnyParameter=true
        /// What    Returns true
        /// </summary>
        [Fact]
        public void UseAnyThanSpecificParameterRatherAttributeFact002()
        {
            // Arrange
            var attribute = new UseAnyThanSpecificParameterRatherAttribute();
            var parameter = new IsAnyParameter
            {
                Name = Guid.NewGuid().ToString("N")
            };

            // Act
            var result = attribute.IsValid<TestDocument>(parameter, out string errorMessage);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using an instance of a class that implements IAnyParameter, and value property Name is a predefined parameter
        /// When    Validate this instance in method DocumentSearch<T>.ValidateSearchItem using SolrExpressOptions.CheckAnyParameter=true
        /// What    Returns false
        /// </summary>
        [Theory]
        [InlineData("facet.field")]
        [InlineData("facet.limit")]
        [InlineData("facet.query")]
        [InlineData("facet.range")]
        [InlineData("fl")]
        [InlineData("fq")]
        [InlineData("mm")]
        [InlineData("q")]
        [InlineData("qf")]
        [InlineData("rows")]
        [InlineData("sort")]
        [InlineData("start")]
        public void UseAnyThanSpecificParameterRatherAttributeTheory001(string name)
        {
            // Arrange
            var attribute = new UseAnyThanSpecificParameterRatherAttribute();
            var parameter = new IsAnyParameter
            {
                Name = name
            };

            // Act
            var result = attribute.IsValid<TestDocument>(parameter, out string errorMessage);

            // Assert
            Assert.False(result);
        }
    }
}
