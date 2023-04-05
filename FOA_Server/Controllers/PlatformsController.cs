using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        // GET: api/<PlatformsController>
        [HttpGet]
        public List<Platform> Get()
        {
            Platform p = new Platform();
            return p.ReadAllPlatforms();
        }

        // GET api/<PlatformsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PlatformsController>
        [HttpPost]
        public Platform Post([FromBody] Platform plat)
        {
            Platform affected = plat.InsertPlatform();
            return affected;
        }

        


    }
}
