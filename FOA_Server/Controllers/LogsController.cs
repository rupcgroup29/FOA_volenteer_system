using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        // GET: api/<LogsController>
        [HttpGet]
        public List<Log> Get()
        {
            return Log.ReadAllLogs();
        }

    }
}
