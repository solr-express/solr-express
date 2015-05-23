using System.Text;

namespace SolrExpress.Exception
{
    public class UnexpectedJsonFormatException : System.Exception
    {
        /// <summary>
        /// Get the exception message
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        /// <returns>Exception message</returns>
        private static string GetExceptionMessage(string json)
        {
            var sb = new StringBuilder();
            sb.AppendLine("The informed JSON string is unexpected in the use of the parse");
            sb.AppendLine("The informed JSON string was:");
            sb.AppendLine(json);

            return sb.ToString();
        }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonFormatException(string json)
            : base(UnexpectedJsonFormatException.GetExceptionMessage(json))
        {
        }
    }
}
