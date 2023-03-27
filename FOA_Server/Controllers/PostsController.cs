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
