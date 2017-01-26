using System.Text.RegularExpressions;

namespace SolrExpress.Search.Result.Utility
{
    internal static class SearchResultRegex
    {
        internal static Regex InformationResultElapsedTimeragment = new Regex(@"responseHeader\42\:(.|\t|\n)+QTime(.|\t|\n)+?(\d+)", RegexOptions.Compiled);
        internal static Regex InformationResultDocumentCountFragment = new Regex(@"response\42\:(.|\t|\n)+numFound(.|\t|\n)+?(\d+)", RegexOptions.Compiled);
    }
}