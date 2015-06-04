using System;

namespace SolrExpress.Core.Attribute
{
    /// <summary>
    /// Attribute used to indicate field configurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SolrFieldAtribute : System.Attribute
    {
        public SolrFieldAtribute(string label)
        {
            this.Label = label;
        }

        public string Label { get; set; }

        public bool Index { get; set; }

        public bool Stored { get; set; }
    }
}
