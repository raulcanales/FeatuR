using FeatuR;
using Microsoft.AspNetCore.Mvc;

namespace Samples.featuR.WebApp.Controllers
{
    [ApiController]    
    public class DefaultController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public DefaultController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("feature1")]
        public IActionResult Feature1()
        {
            if (_featureService.IsFeatureEnabled("feature1"))
                return Ok();

            return BadRequest();
        }

        [HttpGet("feature2")]
        public IActionResult Feature2()
        {
            if (_featureService.IsFeatureEnabled("feature2"))
                return Ok();

            return BadRequest();
        }
    }
}
