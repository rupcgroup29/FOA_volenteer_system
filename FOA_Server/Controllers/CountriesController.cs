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
        [HttpGet]   //get all countries by list
        public List<Country> Get()
        {
            return Country.ReadAllCountries();
        }



    }
}
