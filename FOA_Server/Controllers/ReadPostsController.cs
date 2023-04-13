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
            ReadPost post = new ReadPost();
            return post.ReadPostWithHIRAandKeyworks();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("{postId}")]
        public ReadPost Get(int postId)
        {
            ReadPost p = new ReadPost();
            return p.ReadPostByIdWithHIRAandKeyworks(postId);
        }


        // PUT api/<ReadPostsController>/5
        [HttpPut]
        public int Put([FromBody] UpdatePostStatus postStatusUpdate)
        {
            ReadPost post = new ReadPost();

            try
            {
                int affected = post.UpdatePostStatus(postStatusUpdate);       // update post details
                if (affected > 0)
                {
                    return affected;
                }
                else throw new Exception(" couldn't succeed in update this user ");

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


    }
}
