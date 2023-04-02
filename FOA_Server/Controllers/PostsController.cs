using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET: api/<PostsController>
        [HttpGet]
        public List<Post> Get()
        {
            Post p = new Post();
            return p.ReadAllPosts();
        }

        // GET: api/<PostsController>/6
        [HttpGet("noStatusPosts")]
        public List<Post> GetNoneStatusPosts()
        {
            Post post = new Post();
            return post.ReadPostsWithoutStatus();
        }

        // GET: api/<PostsController>/6
        [HttpGet("approvalPosts")]
        public List<Post> GetAllApprovedPosts()
        {
            Post post = new Post();
            return post.ApprovalPosts();
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostsController>
        [HttpPost]
        public Post Post([FromBody] Post post)
        {
            // ענת: הוספת פונקציות הבודקות אם המשתמש הקליד אחר באחד השדות והפעלת פונקציה מתאימה
            if (post.CountryID == 999)
            {
                new Country(post.CountryName, post.CountryID).InsertCountry();
                Country newID = new Country();
                int countryID = newID.getCountryByName(post.CountryName);
                post.CountryID = countryID;
            }
            if (post.LanguageID == 999)
            {
                new Language(post.LanguageName, post.LanguageID).InsertLanguage();
                Language newID = new Language();
                int languageID = newID.getLanguageByName(post.CountryName);
                post.LanguageID = languageID;
            }
            if (post.PlatformID == 999)
            {
                new Platform(post.PlatformID,post.PlatformName).InsertPlatform();
                Platform newID = new Platform();
                int platformID = newID.getPlatformByName(post.PlatformName);
                post.PlatformID = platformID;
            }
            Post affected = post.InsertPost();
            return affected;
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
