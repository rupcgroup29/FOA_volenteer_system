using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerProgramsController : ControllerBase
    {
        // GET: api/<VolunteerProgramsController>
        [HttpGet]   //get volunteer programs list
        public List<VolunteerProgram> Get()
        {
            return VolunteerProgram.ReadAllVolunteerPrograms();
        }


        // POST api/<VolunteerProgramsController>
        [HttpPost]    //add new volunteer program to the database
        public VolunteerProgram Post([FromBody] VolunteerProgram vp)
        {
            return vp.InsertVolunteerProgram();
        }


        
    }
}
