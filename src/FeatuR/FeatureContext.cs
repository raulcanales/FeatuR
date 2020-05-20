using System.Collections.Generic;

namespace FeatuR
{
    public class FeatureContext : IFeatureContext
    {
        private Dictionary<string, string> _parameters;
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
