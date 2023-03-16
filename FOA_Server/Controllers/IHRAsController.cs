using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IHRAsController : ControllerBase
    {
        // GET: api/<IHRAsController>
        [HttpGet]
        public List<IHRA> Get()
        {
            PostServices i = new PostServices();
            return i.ReadAllIHRAs();
        }

        // GET api/<IHRAsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IHRAsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IHRAsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IHRAsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
