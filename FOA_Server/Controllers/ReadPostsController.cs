using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadPostsController : ControllerBase
    {
        // GET: api/<ReadPostsController>
        [HttpGet]
        public List<ReadPost> Get()
        {
            ReadPost p = new ReadPost();
            return p.ReadPostWithHIRAandKeyworks();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("{postId}")]
        public ReadPost Get(int postId)
        {
            ReadPost p = new ReadPost();
            return p.ReadPostByIdWithHIRAandKeyworks(postId);
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("ExposureKeyWordsAndHashtages")]
        public string GetExposureKeyWordsAndHashtages()
        {
            ReadPost p = new ReadPost();
            return p.ReadExposureKeyWordsAndHashtages();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("ExposurePlatform")]
        public string GetExposurePlatform()
        {
            ReadPost p = new ReadPost();
            return p.ReadExposurePlatform();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("ExposureLanguage")]
        public string GetExposureLanguage()
        {
            ReadPost p = new ReadPost();
            return p.ReadExposureLanguage();
        }


        // PUT api/<ReadPostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
