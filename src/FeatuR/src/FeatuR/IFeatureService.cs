namespace FeatuR
{
    public interface IFeatureService
    {
        bool IsFeatureEnabled(string featureId);
        bool IsFeatureEnabled(string featureId, FeatureContext context);
    }
}
