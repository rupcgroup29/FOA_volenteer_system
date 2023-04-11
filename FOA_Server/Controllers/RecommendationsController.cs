using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        // GET api/<ReadPostsController>/5
        [HttpGet("ExposureKeyWordsAndHashtages")]
        public string GetExposureKeyWordsAndHashtages()
        {
           return Recommendation.ReadExposureKeyWordsAndHashtages();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("ExposurePlatform")]
        public string GetExposurePlatform()
        {
            return Recommendation.ReadExposurePlatform();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("ExposureLanguage")]
        public string GetExposureLanguage()
        {
            return Recommendation.ReadExposureLanguage();

        }

    }
}
