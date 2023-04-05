using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class ParentForgotPass
    {
        public string Email { get; set; }

        public ParentForgotPass() { }

        public ParentForgotPass(string email)
        {
            Email = email;
        }

        public bool ShouldWeResetPassword()
        {
            UserService? user = UserService.GetUserByEmail(this.Email);
            if (user != null && user?.LastReasetPassword != null && user?.LastReasetPassword?.Ticks > DateTime.Now.Ticks)
                return false;
            return true;
        }

        public void SaveNewPassword(string email, string newPassword)
        {
            DBusers db = new DBusers();
            db.UpdateUserPassword(email, newPassword);

        }

    }
}
