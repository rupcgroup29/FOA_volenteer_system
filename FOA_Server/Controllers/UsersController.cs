using FOA_Server.Models;
using FOA_Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public List<User> Get()
        {
            //User u = new User();
            UserServices user = new UserServices();
            return user.ReadAllUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST api/<UsersController>
        [HttpPost]
        public User Post([FromBody] UserServices user)
        {
            //User affected = user.InsertUser();
            User affected = user.InsertUser();
            return affected;
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public User Put([FromBody] UserServices user)
        {
            User affected = user.UpdateUser();
            return affected;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
