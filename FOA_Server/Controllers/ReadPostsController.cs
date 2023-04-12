﻿using FOA_Server.Models;
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
        public void Put([FromBody] UpdatePostStatus postStatusUpdate)
        {
            ReadPost post = new ReadPost();
            post.UpdatePostStatus(postStatusUpdate);
        }


    }
}
