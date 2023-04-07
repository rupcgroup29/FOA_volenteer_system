using FOA_Server.Models;
using FOA_Server.Services;
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<UserServicesController>
        [HttpPost]
        public void Post([FromBody] UserService user)
        {
            if (user.ProgramID == 999)  //if new volanteer program was choosen
            {
                new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                VolunteerProgram newID = new VolunteerProgram();
                int programID = newID.getVolunteerProgramByName(user.ProgramName);
                user.ProgramID = programID;
            }
            UserService insertedUser = user.InsertUser();

            try
            {
                // bulid & send the email 
                string messageBody = $"Welcome {insertedUser.FirstName} {insertedUser.Surname} to our Volenteer System! :)";
                string subject = "FOA Volenteer System - welcome";
                EmailService emailService = new EmailService();
                emailService.SendEmail(emailService.createMailMessage(insertedUser.Email, messageBody, subject));
            }
            catch (Exception ex) { }
        }

        // POST api/<UserServicesController>/6
        [HttpPost("login")]
        public UserService? GetLogin([FromBody] UserLogin useLog)
        {
            string email = useLog.Email;
            string password = useLog.Password;
            return UserService.Login(email, password);
        }


        // PUT api/<UserServicesController>/5
        [HttpPut]
        public UserService Put([FromBody] UserService user)
        {
            UserService affected = user.UpdateUser();
            return affected;
        }

        // POST api/<UserServicesController>
        [HttpPost("{resetEmail}")]
        public void PasswordResetToken(string resetEmail)
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
            parentforgotPassword.SaveNewPassword(resetEmail, newPasswordStr);
        }




    }
}
