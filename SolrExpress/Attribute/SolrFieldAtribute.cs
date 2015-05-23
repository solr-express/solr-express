using System;

namespace SolrExpress.Attribute
{
    /// <summary>
    /// Attribute used to indicate field configurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class SolrFieldAtribute : System.Attribute
    {
        private string _label;
        private bool _index;
        private bool _stored;

        public SolrFieldAtribute(string label, bool index, bool stored)
        {
            this._index = index;
            this._label = label;
            this._stored = stored;
        }
    }
}
