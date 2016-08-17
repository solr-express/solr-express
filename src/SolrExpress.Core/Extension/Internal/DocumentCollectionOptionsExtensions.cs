namespace SolrExpress.Core.Extension.Internal
{
    public static class DocumentCollectionOptionsExtensions
    {
        /// <summary>
        /// Copy DocumentCollectionOptions instance based in generic form to non-generic form
        /// </summary>
        /// <param name="original">Original instance to be copied</param>
        /// <param name="copy">Copy instance to receive data</param>
        internal static void CopyOptionsTo<TDocument>(this DocumentCollectionOptions<TDocument> original, out DocumentCollectionOptions copy)
            where TDocument : IDocument
        {
            copy = new DocumentCollectionOptions
            {
                CheckAnyParameter = original.CheckAnyParameter,
                FailFast = original.FailFast
            };

            copy.GlobalParameters.AddRange(original.GlobalParameters);
            copy.GlobalQueryInterceptors.AddRange(original.GlobalQueryInterceptors);
            copy.GlobalResultInterceptors.AddRange(original.GlobalResultInterceptors);
        }

        /// <summary>
        /// Copy DocumentCollectionOptions instance based in nongeneric form to generic form
        /// </summary>
        /// <param name="original">Original instance to be copied</param>
        /// <param name="copy">Copy instance to receive data</param>
        internal static void CopyOptionsTo<TDocument>(this DocumentCollectionOptions original, out DocumentCollectionOptions<TDocument> copy)
            where TDocument : IDocument
        {
            copy = new DocumentCollectionOptions<TDocument>
            {
                CheckAnyParameter = original.CheckAnyParameter,
                FailFast = original.FailFast
            };

            copy.GlobalParameters.AddRange(original.GlobalParameters);
            copy.GlobalQueryInterceptors.AddRange(original.GlobalQueryInterceptors);
            copy.GlobalResultInterceptors.AddRange(original.GlobalResultInterceptors);
        }
    }
}
