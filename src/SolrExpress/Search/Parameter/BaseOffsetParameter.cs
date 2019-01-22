namespace SolrExpress.Search.Parameter
{
    public abstract class BaseOffsetParameter<TDocument> : IOffsetParameter<TDocument>
        where TDocument : Document
    {
        public long Value { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IOffsetParameter<TDocument> parameter)
            {
                return this.Value.Equals(parameter.Value);
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
