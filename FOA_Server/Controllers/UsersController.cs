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
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public List<User> Get()
        {
            User user = new User();
            return user.ReadAllUsers();
        }

        // GET: api/<UsersController>/6
        [HttpGet("permissionIDlist/{permissionID}")]
        public List<User> GetByPermission(int permissionID)
        {
            User user = new User();
            return user.UsersByPermission(permissionID);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public User Post([FromBody] User user)
        {
            if (user.ProgramID == 999)
            {
                new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                //TODO:  להוסיף קריאה לפרוגרם איידי החדש מהדאטה בייס והזנה שלו ליוזר
                // int programId = getVolunteerProgramByName(user.ProgramName); // לשנות את המתודה למתודה שמביאה וולנטיר פרוגרם איידי לפי שם
                VolunteerProgram newID = new VolunteerProgram();
                int programID = newID.getVolunteerProgramByName(user.ProgramName);
                user.ProgramID = programID;
            }
            User affected = user.InsertUser();
            return affected;
        }

        // POST api/<UsersController>/6
        [HttpPost("login")]
        public User GetLogin([FromBody] UserLogin useLog)
        {
            string email = useLog.Email;
            string password = useLog.Password;

            User user = new User();
            return user.Login(email, password);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public User Put([FromBody] User user)
        {
            User affected = user.UpdateUser();
            return affected;
        }

        /*   // POST api/<UsersController>
           [HttpPost("{resetEmail}")]
           public int PasswordResetToken([FromBody] string email)
           {
               ParentForgotPass parentforgotPassword = new ParentForgotPass();
               parentforgotPassword.Email = email;

               // Get the email and send a link mailToResetPassword reset the password
               // Generate token - Guid.NewGuid();
               Guid token = Guid.NewGuid();
               MailMessage resetPasswordMessage = new MailMessage();
               string mailToResetPassword = parentforgotPassword.Email;
               string ourMail = "ruppindads@gmail.com";
               string ourMailPass = "xwjxrnuywiawoirv\r\n";
               string messageBody = "Forgot your password? We recived a request to reset the password for your account, your reset code is " + token;
               SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");


               // Buidlding the message 
               resetPasswordMessage.To.Add(mailToResetPassword);
               resetPasswordMessage.From = new MailAddress(ourMail);
               resetPasswordMessage.Body = messageBody;
               resetPasswordMessage.Subject = "UnityCom - reset password";
               resetPasswordMessage.IsBodyHtml = true;
               // Done

               smtpClient.Credentials = new System.Net.NetworkCredential(ourMail, ourMailPass);
               smtpClient.EnableSsl = true; // Security
               smtpClient.Port = 587; // SMTP client to SMTP Server port. (port=25 means smtp server to smtp server)
               smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // email is sent through the network
               smtpClient.Credentials = new NetworkCredential(ourMail, ourMailPass);

               try
               {
                   smtpClient.Send(resetPasswordMessage);
                   Trace.WriteLine("code send successfully");

               }
               catch (Exception ex)
               {
                   Trace.WriteLine(ex.ToString());
                   Trace.WriteLine(ex.Message);
               }

               DateTime deadlineDateTime = (DateTime.Now).AddMinutes(5);
               string token1 = token.ToString();
               return parentforgotPassword.PasswordResetToken(token1, deadlineDateTime, email);
           } */




    }
}
