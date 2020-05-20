using System.Collections.Generic;

namespace FeatuR
{
    public interface IFeatureContext
    {
        Dictionary<string, string> Parameters { get; set; }
    }
}
