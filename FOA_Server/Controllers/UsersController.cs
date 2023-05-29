using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServicesController : ControllerBase
    {
        // GET: api/<UserServicesController>
        [HttpGet]  // get all the users with password & only IDs
        public List<UserService> Get()
        {
            return UserService.ReadAllUsers();
        }

        // GET: api/<UserServicesController>/6
        [HttpGet("AllUsers")]   // get all the users with names for users screen
        public List<UserService> GetAllUsers()
        {
            return UserService.ReadAllUsersWithNames();
        }

        // GET api/<UserServicesController>/5
        [HttpGet("user_details/{userId}")]   // get all the users with names & without password
        public UserService GetUserById(int userId)
        {
            return UserService.ReadUserByIdWithoutPassword(userId);
        }

        // GET api/<UserServicesController>/5
        [HttpGet("{myUserId}")]   // get all the users with names & with password
        public UserService GetMyUser(int myUserId)
        {
            return UserService.ReadUserByIdWithPassword(myUserId);
        }

        // GET api/<UserServicesController>/5
        [HttpGet("teamLeader/{userID}")]   // get user's team leader contact details
        public UserService GetUserTeamLeaderDetails(int userID)
        {
            UserService correntUser = UserService.ReadUserByIdWithPassword(userID);
            int teamLeaderID = Team.GetUserManegerID(correntUser.TeamID);
            return UserService.ReadUserByIdWithoutPassword(teamLeaderID);
        }

        // GET api/<UserServicesController>/5
        [HttpGet("usersInTeam/{teamId}")]   // get all users in a specific team
        public List<UserService> GetUsersInTeam(int teamId)
        {
            return UserService.ReadUsersInTeam(teamId);
        }


        // POST api/<UserServicesController>
        [HttpPost]      //insert new user, with different volnteer program, with sent welcome mail with the password
        public bool Post([FromBody] UserService user)
        {
            try
            {
                try
                {
                    if (user.ProgramID == 999)  //if new volanteer program was choosen
                    {
                        new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                        VolunteerProgram newID = new VolunteerProgram();
                        int programID = newID.getVolunteerProgramByName(user.ProgramName);
                        user.ProgramID = programID;
                    }
                }
                catch (Exception e) { throw new Exception(" מסגרת התנדבות זו כבר קיימת במערת " + e.Message); }

                int insertedUser = user.InsertUser();   //gets the new user's ID in the database

                if (insertedUser > 0)
                {
                    try
                    {
                        UserService newUser = UserService.ReadUserByIdWithPassword(insertedUser);
                        // bulid & send the email 
                        string messageBody = $" ברוכים הבאים {newUser.FirstName} {newUser.Surname} למערכת ההתנדבות של FOA! ";
                        messageBody += $" הסיסמא שלך היא: {newUser.Password} ";
                        string subject = "ברוכים הבאים למערכת ההתנדבות של FOA!";
                        EmailService emailService = new EmailService();
                        emailService.SendEmail(emailService.createMailMessage(newUser.Email, messageBody, subject));
                        return true;
                    }
                    catch (Exception ex)
                    { throw new Exception(" didn't succeed in sending the mail " + ex.Message); }
                }
                else return false;
            }
            catch (Exception ex)
            { throw new Exception(" didn't succeed in inserting new user " + ex.Message); }
        }

        // POST api/<UserServicesController>/6
        [HttpPost("login")]     //check if the user's email & password are currect
        public IActionResult Login([FromBody] UserLogin userLog)
        {
            string email = userLog.Email;
            string password = userLog.Password;

            try
            {
                int[] loginUser = UserService.Login(email, password);

                if (loginUser[0] == 0 && loginUser[1] == 0)
                {
                    throw new Exception(" האימייל או הסיסמא שהזנת שגויים ");
                }

                return Ok(loginUser); //returns array with logged-in user's ID and he's permmitionID
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // POST api/<UserServicesController>
        [HttpPost("{resetEmail}")]   //send new password by mail (if user forgot password)
        public bool PasswordResetToken(string resetEmail)
        {
            ForgotPass parentforgotPassword = new ForgotPass(resetEmail);

            // chech if we can use resert password, as long we didn't use it for the last 5 minutes
            if (!parentforgotPassword.ShouldWeResetPassword())
            {
                throw new Exception(" can not proceed this resert password with email " + resetEmail + ". Please try again in 5 minutes");
            }

            try
            {
                Guid newPassword = Guid.NewGuid();      // create random password

                // bulid & send the email 
                string messageBody = $"בעקבות שלחצת על כפתור 'שכחתי סיסמא', להלן הסיסמא החדשה שלך למערכת: {newPassword}";
                string subject = "איפוס סיסמא למערכת ההתנדבות של FOA";
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
            catch (Exception ex) { throw new Exception(" didn't succeed in reset password " + ex.Message); }
        }


        // PUT api/<UserServicesController>/5
        [HttpPut("myUser")]     //update my user's details (the user who is logged in)
        public bool PutMyUser([FromBody] UserService user)
        {
            try
            {
                try
                {
                    if (user.ProgramID == 999)  //if new volanteer program was choosen
                    {
                        new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                        VolunteerProgram newID = new VolunteerProgram();
                        int programID = newID.getVolunteerProgramByName(user.ProgramName);
                        user.ProgramID = programID;
                    }
                }
                catch (Exception e) { throw new Exception(" מסגרת התנדבות זו כבר קיימת במערת " + e.Message); }

                bool affected = user.UpdateUserWithPassword();
                return affected;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        // PUT api/<UserServicesController>/5
        [HttpPut]    // update another user's details (NOT the user who is logged in)
        public bool Put([FromBody] UserService user)
        {
            try
            {
                try
                {
                    if (user.ProgramID == 999)  //if new volanteer program was choosen
                    {
                        new VolunteerProgram(user.ProgramID, user.ProgramName).InsertVolunteerProgram();
                        VolunteerProgram newID = new VolunteerProgram();
                        int programID = newID.getVolunteerProgramByName(user.ProgramName);
                        user.ProgramID = programID;
                    }
                }
                catch (Exception e) { throw new Exception(" מסגרת התנדבות זו כבר קיימת במערת " + e.Message); }

                bool affected = user.UpdateUser();       // update another user's details
                return affected;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        // PUT api/<UserServicesController>/5
        [HttpPut("team")]     //update users teamID
        public bool PutUsersTeam(JsonElement[] users)
        {
            try
            {
                UserService user = new UserService();
                bool affected = user.UpdateUsersTeam(users);
                return affected;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

    }
}
