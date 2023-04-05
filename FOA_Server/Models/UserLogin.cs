namespace FOA_Server.Models
{
    public class UserServiceLogin
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public UserServiceLogin(string email, string password)
        {
            Password = password;
            Email = email;
        }


    }
}
