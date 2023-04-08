using FOA_Server.Models;
using FOA_Server.Models.DAL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        // GET: api/<CountriesController>
        [HttpGet]
        public List<Country> Get()
        {
            Country c = new Country();
            return c.ReadAllCountries();
        }

        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CountriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}
