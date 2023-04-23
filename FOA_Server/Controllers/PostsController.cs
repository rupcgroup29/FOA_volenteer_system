using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET: api/<PostsController>/6
        [HttpGet("noStatusPosts")]    //get all new inserted posts (witout any status yet)
        public List<Post> GetNoneStatusPosts()
        {
            return Models.Post.ReadPostsWitoutStatusByMenagerName();
        }

        // GET: api/<PostsController>/6
        [HttpGet("numberOfNoneStatusPosts")]    //get the number of non-status posts
        public int GetNumberOfNoneStatusPosts()
        {
            return Models.Post.NumberOfPostdWithoutStatus();
        }

        // POST api/<PostsController>
        [HttpPost]      //insert new post to DB with the opption for insert new country & language & platform
        public IActionResult Post([FromBody] Post post)
        {
            try
            {
                if (post.CountryID == 999)  //insert new country to the database
                {
                    int newCountryID = new Country(post.CountryName, post.CountryID).InsertCountry();
                    post.CountryID = newCountryID;
                }
                if (post.LanguageID == 999)   //insert new language to the database
                {
                    int newLangID = new Language(post.LanguageName, post.LanguageID).InsertLanguage();
                    post.LanguageID = newLangID;
                }
                if (post.PlatformID == 999)    //insert new platform to the database
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

        // POST api/<PostsController>/6
        [HttpPost("screenshot")]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files)
        {
            List<string> imageLinks = new List<string>();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var filePath = Path.Combine(path, fileName);

                    // if the file name is allready exists in the folder - give it a new name
                    if (System.IO.File.Exists(filePath))
                    {
                        // If the file already exists, generate a new file name
                        var extension = Path.GetExtension(fileName);    //cat the .jpg part from the file name
                        var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                        int i = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            fileName = $"{nameWithoutExtension}_{i++}{extension}";
                            filePath = Path.Combine(path, fileName);
                        }
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Add the file name to the list
                    imageLinks.Add(fileName);
                }
            }

            return Ok(imageLinks);
        }

    }
}
