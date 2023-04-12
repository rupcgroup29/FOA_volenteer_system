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
        [HttpGet("ExposureParameters")]
        public Recommendation GetTopExposureParameters()
        {
            return new Recommendation(Recommendation.ReadExposureKeyWordsAndHashtages(), Recommendation.ReadExposurePlatform(), Recommendation.ReadExposureLanguage());
        }


    }
}
