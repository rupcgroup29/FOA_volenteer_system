using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServicesController : ControllerBase
    {
        // GET: api/<UserServicesController>
        [HttpGet]
        public List<UserService> Get()
        {
            return UserService.ReadAllUsers();
        }

        // GET: api/<UserServicesController>/6
        [HttpGet("permissionIDlist/{permissionID}")]
        public List<UserService> GetByPermission(int permissionID)
        {
            UserService user = new UserService();
            return user.UsersByPermission(permissionID);
        }

        // GET api/<UserServicesController>/5
        [HttpGet("{userId}")]
        public UserService Get(int userId)
        {
            UserService userID = new UserService();
            return userID.ReadUserById(userId);
        }

        // POST api/<UserServicesController>
        [HttpPost]
        public bool Post([FromBody] UserService user)
        {
            if (user.ProgramID == 999)  //if new volanteer program was choosen
            {
                new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                VolunteerProgram newID = new VolunteerProgram();
                int programID = newID.getVolunteerProgramByName(user.ProgramName);
                user.ProgramID = programID;
            }
            bool insertedUser = user.InsertUser();

            //if (insertedUser)
            //{
            //    try
            //    {
            //        // bulid & send the email 
            //        string messageBody = $"Welcome {user.FirstName} {user.Surname} to our Volenteer System! :)";
            //        string subject = "FOA Volenteer System - welcome";
            //        EmailService emailService = new EmailService();
            //        emailService.SendEmail(emailService.createMailMessage(user.Email, messageBody, subject));
            //    }
            //    catch (Exception ex) { }
            //}
            return insertedUser;

        }


        // POST api/<UserServicesController>/6
        [HttpPost("login")]
        public IActionResult GetLogin([FromBody] UserLogin userLog)
        {
            string email = userLog.Email;
            string password = userLog.Password;

            try
            {
                UserService userService = UserService.Login(email, password);

                if (userService == null)
                {
                    throw new Exception(" wrong email or password ");
                }

                return Ok(userService);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // PUT api/<UserServicesController>/5
        [HttpPut("myUser")]
        public UserService PutMyUser([FromBody] UserService user)
        {
            UserService affected = user.UpdateUserWithPassword();   // update my user
            return affected;
        }

        // PUT api/<UserServicesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserService user)
        {
            try
            {
                bool affected = user.UpdateUser();       // update another user's details
                if (!affected)
                {
                    throw new Exception(" couldn't succeed in update this user ");
                }

                return Ok(affected);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // POST api/<UserServicesController>
        [HttpPost("{resetEmail}")]
        public bool PasswordResetToken(string resetEmail)
        {
            ForgotPass parentforgotPassword = new ForgotPass(resetEmail);

            // chech if we can use resert password, as long we didn't use it for the last 5 minutes
            if (!parentforgotPassword.ShouldWeResetPassword())
            {
                throw new Exception(" can not proceed this resert password with email " + resetEmail);
            }

            Guid newPassword = Guid.NewGuid();      // create random password

            // bulid & send the email 
            string messageBody = "Forgot your password? We recived a request to reset the password for your account, your reset code is " + newPassword;
            string subject = "FOA Volenteer System - reset password";
            EmailService emailService = new EmailService();
            emailService.SendEmail(emailService.createMailMessage(resetEmail, messageBody, subject));

            //update the new password in the data base
            string newPasswordStr = newPassword.ToString();
            int succeed = parentforgotPassword.SaveNewPassword(resetEmail, newPasswordStr);

            if (succeed != 0)
            {
                return true;
            }
            else return false;

        }






    }
}
