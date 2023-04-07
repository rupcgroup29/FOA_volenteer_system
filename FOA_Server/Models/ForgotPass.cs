﻿using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class ForgotPass
    {
        public string Email { get; set; }

        public ForgotPass() { }

        public ForgotPass(string email)
        {
            Email = email;
        }

        // valid if 5 minutes has not passed from the last time user pressed 'forget password' button
        public bool ShouldWeResetPassword()
        {
            UserService? user = UserService.GetUserByEmail(this.Email);
            if (user != null && user?.LastReasetPassword != null && user?.LastReasetPassword?.Ticks > DateTime.Now.Ticks)
                return false;
            return true;
        }

        // save the new random password that was sent to the user by mail in the DB
        public void SaveNewPassword(string email, string newPassword)
        {
            DBusers db = new DBusers();
            db.UpdateUserPassword(email, newPassword);

        }

    }
}
