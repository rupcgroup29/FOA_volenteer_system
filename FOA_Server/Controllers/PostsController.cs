using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET: api/<PostsController>/6
        [HttpGet("noStatusPosts")]
        public List<Post> GetNoneStatusPosts()
        {
            return Models.Post.ReadPostsWitoutStatusByMenagerName();
        }

        // GET: api/<PostsController>/6
        [HttpGet("numberOfNoneStatusPosts")]
        public int GetNumberOfNoneStatusPosts()
        {
            return Models.Post.NumberOfPostdWithoutStatus();
        }

        // POST api/<PostsController>
        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            try
            {
                if (post.CountryID == 999)
                {
                    int newCountryID = new Country(post.CountryName, post.CountryID).InsertCountry();
                    post.CountryID = newCountryID;
                }
                if (post.LanguageID == 999)
                {
                    int newLangID = new Language(post.LanguageName, post.LanguageID).InsertLanguage();
                    post.LanguageID = newLangID;
                }
                if (post.PlatformID == 999)
                {
                    int newPlatromID = new Platform(post.PlatformID, post.PlatformName).InsertPlatform();
                    post.PlatformID = newPlatromID;
                }
                Post affected = post.InsertPost();
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }




    }
}
