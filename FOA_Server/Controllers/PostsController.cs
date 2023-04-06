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
        // GET: api/<PostsController>/6
        [HttpGet("noStatusPosts")]
        public List<Post> GetNoneStatusPosts()
        {
            Post post = new Post();
            return post.ReadPostsWithoutStatus();
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
                new Platform(post.PlatformID, post.PlatformName).InsertPlatform();
                Platform newID = new Platform();
                int platformID = newID.getPlatformByName(post.PlatformName);
                post.PlatformID = platformID;
            }
            Post affected = post.InsertPost();
            return affected;
        }


        // PUT api/<PostsController>/5
        [HttpPut("{postId}")]
        public int Put(int postId, [FromBody] PostChangeStatus postStatusUpdate)
        {
            postId = postStatusUpdate.PostID;
            int postStatus = postStatusUpdate.PostStatus;
            int removalStatus = postStatusUpdate.RemovalStatus;
            int postStatusManager = postStatusUpdate.PostStatusManager;
            int removalStatusManager = postStatusUpdate.RemovalStatusManager;

            Post post = new Post();
            return post.UpdatePost(postId, postStatus, removalStatus, postStatusManager, removalStatusManager);

        }


    }
}
