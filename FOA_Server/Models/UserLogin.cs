namespace FOA_Server.Models
{
    public class UserLogin
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public UserLogin(string email, string password)
        {
            Password = password;
            Email = email;
        }


    }
}
