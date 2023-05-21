using FOA_Server.Models;
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
            return Team.ReadAllTeams();
        }

        // GET api/<TeamsController>/5
        [HttpGet("teamsDetails")]
        public List<Object> GetTeamsDetails()
        {
            return Team.ReadTeamsDetails();
        }

        // GET api/<TeamsController>/5
        [HttpGet("teamDetails/{teamID}")]
        public Object GetTeamDetailsByID(int teamID)
        {
            return Team.ReadTeamDetailsByID(teamID);
        }

        // GET api/<TeamsController>/5
        [HttpGet("teamLeadersWithoutTeam")]
        public List<Object> GetTeamLeadersWithoutTeamToLead()
        {
            try
            {
                return Team.ReadTeamLeadersWithoutTeamToLead();
            }
            catch (Exception ex)
            {
                throw new Exception("cannot read all team leaders" + ex.Message);
            }
        }

        // GET api/<TeamsController>/5
        [HttpGet("GetUsersHourReportsInTeam/{teamID}")]
        public List<Object> GetUsersHourReportsInTeam(int teamID)
        {
            try
            {
                return Team.GetUsersHourReportsInTeam(teamID);
            }
            catch (Exception ex)
            {
                throw new Exception("cannot read all team leaders" + ex.Message);
            }
        }



        // POST api/<TeamsController>
        [HttpPost]
        public IActionResult Post([FromBody] Team newTeam)
        {
            try
            {
                Team team = newTeam.InsertNewTeam();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }


        // PUT api/<TeamsController>/5
        [HttpPut]
        public bool Put([FromBody] Team team)
        {
            bool affected = team.UpdateTeam();       // update team's details
            return affected;
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
