using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FeatuR.EntityFramework.MySQL
{
    public class MySQLFeatureStore : IFeatureStore
    {
        private readonly FeatuRDbContext _context;

        public MySQLFeatureStore(FeatuRDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feature> GetEnabledFeatures()
            => _context.Features.Where(f => f.Enabled);

        public Feature GetFeatureById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            return _context.Features.SingleOrDefault(f => f.Id == id);
        }

        public async Task<Feature> GetFeatureByIdAsync(string id, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            return await _context.Features.SingleOrDefaultAsync(f => f.Id == id, token);
        }
    }
}
