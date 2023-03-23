using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        // GET: api/<TeamsController>
        [HttpGet]
        public List<Team> Get()
        {
            TeamServices t = new TeamServices();
            return t.ReadAllTeams();
        }

        // GET: api/<TeamsController>/6
        [HttpGet("TeamMembersByTeamName")]
        public List<Team> GetTeamMembersByTeamName(string teamName)
        {
            Team t = new Team();
            return t.ReadTeamByName(teamName);
        }

        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TeamsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
