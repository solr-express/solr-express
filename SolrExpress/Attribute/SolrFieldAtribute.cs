using System;

namespace SolrExpress.Attribute
{
    /// <summary>
    /// Attribute used to indicate field configurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SolrFieldAtribute : System.Attribute
    {
        public SolrFieldAtribute(string label, bool index, bool stored)
        {
            this.Index = index;
            this.Label = label;
            this.Stored = stored;
        }

        public string Label { get; set; }

        public bool Index { get; set; }

        public bool Stored { get; set; }
    }
}
