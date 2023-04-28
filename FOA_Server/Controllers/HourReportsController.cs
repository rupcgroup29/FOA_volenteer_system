using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HourReportsController : ControllerBase
    {
        // GET: api/<HourReportsController>
        [HttpGet]
        public List<HourReport> Get()
        {
            return HourReport.ReadAllHourReports();
        }

        // GET api/<HourReportsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HourReportsController>
        [HttpPost]
        public IActionResult Post([FromBody] HourReport shift)
            {
            try
            {
                bool affected = shift.InsertHourReports();
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // PUT api/<HourReportsController>/5
        [HttpPut("{reportID}")]
        public IActionResult Put(int reportID, int status)
        {
            try
            {
                HourReport shiftStatus = new HourReport();

                bool affected = shiftStatus.UpdateShiftStatus(reportID, status); 
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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
