using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FeatuR.RestClient.Samples.WebApp.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        const string FEATURE_ID = "test";

        public DefaultController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (_featureService.IsFeatureEnabled(FEATURE_ID))
                return Ok();

            return Forbid();
        }


        /// <summary>
        /// This endpoint could be implemented in another standalone service.
        /// </summary>
        [HttpGet("another-service/{featureId}")]
        public IActionResult IsFeatureEnabled(string featureId)
        {
            if (featureId == FEATURE_ID)
                return Ok(true);

            return BadRequest();
        }

        /// <summary>
        /// This endpoint could be implemented in another standalone service.
        /// </summary>
        [HttpGet("another-service/features")]
        public IActionResult GetFeatures()
        {
            return Ok(new[] { FEATURE_ID });
        }

        /// <summary>
        /// This endpoint could be implemented in another standalone service.
        /// </summary>
        [HttpPost("another-service/evaluate")]
        public IActionResult EvaluateFeatures(string[] featureIds)
        {
            var result = new Dictionary<string, bool>();
            foreach (var id in featureIds)
            {
                if (!string.IsNullOrWhiteSpace(id))
                    result.TryAdd(id.ToLower(), true);
            }

            return Ok(result);
        }
    }
}
