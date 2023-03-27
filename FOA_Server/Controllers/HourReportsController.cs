using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

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
            HourReport h = new HourReport();
            return h.ReadAllHourReports();
        }

        // GET api/<HourReportsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HourReportsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HourReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HourReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
