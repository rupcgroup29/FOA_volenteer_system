using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IHRAsController : ControllerBase
    {
        // GET: api/<IHRAsController>
        [HttpGet]   //get all IHRA categories by list
        public List<IHRA> Get()
        {
            return IHRA.ReadAllIHRAs();
        }

    }
}
