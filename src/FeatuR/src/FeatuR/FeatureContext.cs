using System.Collections.Generic;

namespace FeatuR
{
    /// <inheritdoc />
    public class FeatureContext : IFeatureContext
    {
        private Dictionary<string, string> _parameters;

        /// <inheritdoc />
        public Dictionary<string, string> Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new Dictionary<string, string>();

                return _parameters;
            }
            set { _parameters = value; }
        }
    }
}
