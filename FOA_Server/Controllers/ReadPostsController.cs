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
        [HttpGet]    //get list of all posts with the IHRA category names & key words hashtags names
        public List<ReadPost> Get()
        {
            ReadPost post = new ReadPost();
            return post.ReadPostWithHIRAandKeyworks();
        }

        // GET api/<ReadPostsController>/5
        [HttpGet("{postId}")]   //get specific post's detials with the IHRA category names & key words hashtags names
        public ReadPost Get(int postId)
        {
            ReadPost p = new ReadPost();
            return p.ReadPostByIdWithHIRAandKeyworks(postId);
        }


        // PUT api/<ReadPostsController>/5
        [HttpPut]     //update post statuses
        public int Put([FromBody] UpdatePostStatus postStatusUpdate)
        {
            try
            {
                ReadPost post = new ReadPost();

                int affected = post.UpdatePostStatus(postStatusUpdate);     // update post details
                if (affected > 0)
                {
                    return affected;
                }
                else throw new Exception(" couldn't succeed in updating this post ");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
