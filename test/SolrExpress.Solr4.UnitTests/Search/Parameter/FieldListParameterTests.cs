//using Xunit;
//using SolrExpress.Solr4.Search.Parameter;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using SolrExpress.Core;
//using SolrExpress.Core.Utility;
//using Moq;

//namespace SolrExpress.Solr4.UnitTests.Search.Parameter
//{
//    public class FieldListParameterTests
//    {
//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Invoking method "Execute" using 2 instances
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void FieldListParameter001()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter1 = new FieldListParameter<TestDocument>(expressionBuilder);
//            var parameter2 = new FieldListParameter<TestDocument>(expressionBuilder);
//            parameter1.Configure(q => q.Id);
//            parameter2.Configure(q => q.Score);

//            // Act
//            parameter1.Execute(container);
//            parameter2.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("fl=_id_,_score_", container[0]);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
//        /// What    Returns valid=false
//        /// </summary>
//        [Fact]
//        public void FieldListParameter002()
//        {
//            // Arrange
//            bool actual;
//            string dummy;
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new FieldListParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.NotStored);

//            // Act
//            parameter.Validate(out actual, out dummy);

//            // Assert
//            Assert.False(actual);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
//        /// What    Returns valid=true
//        /// </summary>
//        [Fact]
//        public void FieldListParameter003()
//        {
//            // Arrange
//            bool actual;
//            string dummy;
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new FieldListParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.Stored);

//            // Act
//            parameter.Validate(out actual, out dummy);

//            // Assert
//            Assert.True(actual);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Invoking method "Execute" using 1 instance and 2 expressions
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void FieldListParameter004()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FieldListParameter<TestDocument>(expressionBuilder);
//            parameter.Configure(q => q.Id, q => q.Score);

//            // Act
//            parameter.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("fl=_id_,_score_", container[0]);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
//        /// What    Returns valid=false
//        /// </summary>
//        [Fact]
//        public void FieldListParameter005()
//        {
//            // Arrange
//            bool actual;
//            string dummy;
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new FieldListParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.Stored, q => q.NotStored);

//            // Act
//            parameter.Validate(out actual, out dummy);

//            // Assert
//            Assert.False(actual);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Create the instance with null
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void FieldListParameter006()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FieldListParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Create the instance with empty collection
//        /// What    Throws ArgumentException
//        /// </summary>
//        [Fact]
//        public void FieldListParameter007()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new FieldListParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentException>(() => parameter.Configure(new Expression<Func<TestDocument, object>>[] { }));
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Invoking method "Validate" using field Stored=true
//        /// What    Valid is true
//        /// </summary>
//        [Fact]
//        public void FieldListParameter008()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new FieldListParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.Stored);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//        }

//        /// <summary>
//        /// Where   Using a FieldListParameter instance
//        /// When    Invoking method "Validate" using field Stored=false
//        /// What    Valid is true
//        /// </summary>
//        [Fact]
//        public void FieldListParameter009()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new FieldListParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.NotStored);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.False(isValid);
//            Assert.Equal(Resource.FieldMustBeStoredTrueToBeUsedInFieldsException, errorMessage);
//        }
//    }
//}
