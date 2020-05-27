using Microsoft.AspNetCore.Mvc;

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
    }
}
