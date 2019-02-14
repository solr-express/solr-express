using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Update
{
    public sealed class DocumentUpdate<TDocument>
        where TDocument : Document
    {
        private readonly Dictionary<Expression, object> _jornal = new Dictionary<Expression, object>();

        /// <summary>
        /// Returns current jornal to update document
        /// </summary>
        /// <returns>Current jornal</returns>
        internal Dictionary<Expression, object> GetJornal()
        {
            return this._jornal;
        }

        /// <summary>
        /// Set or replace a particular value, or remove the value if null is specified as the new value
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="value">Value to set</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> Set<UValue>(Expression<Func<TDocument, UValue>> fieldExpression, UValue value)
        {
            var itemToJornal = new
            {
                set = value
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        /// <summary>
        /// Adds an additional value to a list
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Value to add</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> Add<UArrayValue, UValue>(Expression<Func<TDocument, UArrayValue>> fieldExpression, params UValue[] values)
            where UArrayValue : IEnumerable<UValue>
        {
            var itemToJornal = new
            {
                add = values
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        /// <summary>
        /// Removes a value (or a list of values) from a list
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Value to remove</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> Remove<UArrayValue, UValue>(Expression<Func<TDocument, UArrayValue>> fieldExpression, params UValue[] values)
            where UArrayValue : IEnumerable<UValue>
        {
            var itemToJornal = new
            {
                remove = values
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        /// <summary>
        /// Removes from a list that match the given Java regular expression
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="regexPattern">Regex used to remove values</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> RemoveRegex(Expression<Func<TDocument, object>> fieldExpression, string regexPattern)
        {
            var itemToJornal = new
            {
                removeregex = regexPattern
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        /// <summary>
        /// Increments a numeric value by a specific amount(use a negative value to decrement)
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="value">Value to increment</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> Increment<UValue>(Expression<Func<TDocument, UValue>> fieldExpression, UValue value)
            where UValue : struct,
                  IComparable,
                  IComparable<UValue>,
                  IConvertible,
                  IEquatable<UValue>,
                  IFormattable
        {
            var itemToJornal = new
            {
                inc = value
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        /// <summary>
        /// Decrements a numeric value by a specific amount(use a negative value to decrement)
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="value">Value to increment</param>
        /// <returns>It self</returns>
        public DocumentUpdate<TDocument> Decrement<UValue>(Expression<Func<TDocument, UValue>> fieldExpression, UValue value)
            where UValue : struct,
                  IComparable,
                  IComparable<UValue>,
                  IConvertible,
                  IEquatable<UValue>,
                  IFormattable
        {
            var itemToJornal = new
            {
                dec = value
            };

            this._jornal.Add(fieldExpression, itemToJornal);

            return this;
        }

        public string Id { get; set; }
    }
}
