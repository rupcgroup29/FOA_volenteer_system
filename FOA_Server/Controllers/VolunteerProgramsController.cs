using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerProgramsController : ControllerBase
    {
        // GET: api/<VolunteerProgramsController>
        [HttpGet]
        public List<VolunteerProgram> Get()
        {
            VolunteerProgram vp = new VolunteerProgram();
            return vp.ReadAllVolunteerPrograms();
        }

        // GET api/<VolunteerProgramsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VolunteerProgramsController>
        [HttpPost]
        public VolunteerProgram Post([FromBody] VolunteerProgram vp)
        {
            return vp.InsertVolunteerProgram();
        }

        // PUT api/<VolunteerProgramsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VolunteerProgramsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
