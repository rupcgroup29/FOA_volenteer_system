namespace FOA_Server.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNum { get; set; }
        public string RoleDescription { get; set; }
        public int PermissionID { get; set; }
        public int ProgramID { get; set; }
        public int TeamID { get; set; }

        public User() { }

        public User(int userID, string firstName, string surname, string userName, string email, string password, int phoneNum, string roleDescription, int permissionID, int programID, int teamID)
        {
            UserID = userID;
            FirstName = firstName;
            Surname = surname;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNum = phoneNum;
            RoleDescription = roleDescription;
            PermissionID = permissionID;
            ProgramID = programID;
            TeamID = teamID;
        }



    }
}
