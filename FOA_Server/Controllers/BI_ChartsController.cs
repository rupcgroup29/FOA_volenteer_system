using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BI_ChartsController : ControllerBase
    {
        // GET: api/<BI_ChartsController>
        [HttpGet("Get_RemovedPost_vs_IHRAcategory")]
        public List<Object> Get_RemovedPost_vs_IHRAcategory()
        {
            try
            {
            return BI_chart.ReadPostStatusVsIHRAcaterory();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<BI_ChartsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BI_ChartsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BI_ChartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BI_ChartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
