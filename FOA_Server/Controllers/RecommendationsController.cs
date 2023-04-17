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
        [HttpGet]     //get all 3 parameters for the recommendation algoritem
        public Recommendation GetTopExposureParameters()
        {
            return new Recommendation(Recommendation.ReadExposureLanguage(), Recommendation.ReadExposurePlatform(), Recommendation.ReadExposureKeyWordsAndHashtages());
        }


    }
}
