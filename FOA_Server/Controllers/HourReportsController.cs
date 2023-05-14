using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HourReportsController : ControllerBase
    {
        // GET: api/<HourReportsController>
        [HttpGet]
        public List<Object> Get()
        {
            return HourReport.ReadUsersHourReports();
        }

        // GET api/<HourReportsController>/5
        [HttpGet("{userId}")]
        public List<HourReport> Get(int userId)
        {
            return HourReport.ReadUserHourReports(userId);
        }

        // POST api/<HourReportsController>
        [HttpPost]
        public IActionResult Post([FromBody] HourReport[] shifts)
        {
            try
            {
                HourReport hourReports = new HourReport();
                bool affected = hourReports.InsertHourReports(shifts);
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // PUT api/<HourReportsController>/5
        [HttpPut]
        public IActionResult Put(string jsonString)
        {
            try
            {
                //int reportID, int status, int userId;
                HourReport shiftsStatus = new HourReport();
                bool affected = shiftsStatus.UpdateShiftStatus(jsonString);
                if (affected)
                {
                    return Ok(affected);
                }
                else throw new Exception(" couldn't succeed in updating this post ");

            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }


        // DELETE api/<HourReportsController>/5
        [HttpDelete]
        public void Delete(int reportID)
        {
            try
            {
                HourReport delete = new HourReport();
                delete.DeleteHourReports(reportID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
