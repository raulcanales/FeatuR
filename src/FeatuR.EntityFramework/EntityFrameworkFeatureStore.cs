using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FeatuR.EntityFramework
{
    public class EntityFrameworkFeatureStore : IFeatureStore
    {
        private readonly FeatuRDbContext _context;

        public EntityFrameworkFeatureStore(FeatuRDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feature> GetEnabledFeatures()
            => _context.Features.Where(f => f.Enabled);

        public async Task<IEnumerable<Feature>> GetEnabledFeaturesAsync(CancellationToken token = default)
            => await _context.Features.Where(f => f.Enabled).ToListAsync(token);

        public Feature GetFeatureById(string featureId)
        {
            if (string.IsNullOrWhiteSpace(featureId)) throw new ArgumentNullException(nameof(featureId));
            return _context.Features.SingleOrDefault(f => f.Id == featureId);
        }

        public async Task<Feature> GetFeatureByIdAsync(string featureId, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(featureId)) throw new ArgumentNullException(nameof(featureId));
            return await _context.Features.SingleOrDefaultAsync(f => f.Id == featureId, token);
        }
    }
}
