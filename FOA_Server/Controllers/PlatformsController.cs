using FOA_Server.Models;
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
        [HttpGet]   //get all platforms by list
        public List<Platform> Get()
        {
            return Platform.ReadAllPlatforms();
        }


        // POST api/<PlatformsController>
        [HttpPost]      //add new platform to the database
        public int Post([FromBody] Platform plat)
        {
            int affected = plat.InsertPlatform();
            return affected;
        }

        


    }
}
