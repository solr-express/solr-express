using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseAnyParameter : IAnyParameter, IEquatable<BaseAnyParameter>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IAnyParameter parameter)
            {
                return
                    (this.Name?.Equals(parameter.Name) ?? false) &&
                    (this.Value?.Equals(parameter.Value) ?? false);
            }

            return false;
        }

        public bool Equals(BaseAnyParameter other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return Tuple
                .Create(this.Name, this.Value)
                .GetHashCode();
        }
    }
}
