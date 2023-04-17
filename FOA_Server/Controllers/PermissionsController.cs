using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        // GET: api/<PermissionsController>
        [HttpGet]   //get all permitions in the FOA system, by list
        public List<Permission> Get()
        {
            return Permission.ReadAllPermissions();
        }


    }
}
