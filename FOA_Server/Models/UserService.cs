using FOA_Server.Models.DAL;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FOA_Server.Models
{
    public class UserService
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string RoleDescription { get; set; }
        public int PermissionID { get; set; }
        public string? PermissionName { get; set; }
        public bool IsActive { get; set; }
        public string? Password { get; set; }
        public int TeamID { get; set; }
        public int ProgramID { get; set; }
        public string Email { get; set; }
        public string? ProgramName { get; set; }
        public string? TeamName { get; set; }
        public DateTime? LastReasetPassword { get; set; }
        public double? HoursCount { get; set; }

        private static List<UserService> UsersList = new List<UserService>();

        public UserService() { }

        public UserService(int userID, string firstName, string surname, string userName, string phoneNum, string roleDescription, int permissionID, bool isActive, string? password, int teamID, int programID, string email, string? programName, string? teamName, DateTime? lastReasetPassword, string? permissionName, int? hoursCount)
        {
            UserID = userID;
            FirstName = firstName;
            Surname = surname;
            UserName = userName;
            PhoneNum = phoneNum;
            RoleDescription = roleDescription;
            PermissionID = permissionID;
            IsActive = isActive;
            Password = password;
            TeamID = teamID;
            ProgramID = programID;
            Email = email;
            ProgramName = programName;
            TeamName = teamName;
            LastReasetPassword = lastReasetPassword;
            PermissionName = permissionName;
            HoursCount = hoursCount;
        }


        // read all users
        public static List<UserService> ReadAllUsers()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadUsers();
        }

        // read all users for the users screen
        public static List<UserService> ReadAllUsersWithNames()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadAllUsersWithNames();
        }


        // get user by id with password
        public static UserService ReadUserByIdWithPassword(int id)
        {
            DBusers db = new DBusers();
            return db.ReadUserByIDWithPassword(id);
        }

        // get user by id without password
        public static UserService ReadUserByIdWithoutPassword(int id)
        {
            DBusers db = new DBusers();
            return db.ReadUserByIDWithoutPassword(id);
        }

        // get user by id without password
        public static List<UserService> ReadUsersInTeam(int teamId)
        {
            DBusers db = new DBusers();
            return db.ReadUsersInTeam(teamId);
        }


        // insert new user
        public int InsertUser()
        {
            UsersList = ReadAllUsers();
            try
            {
                if (UsersList.Count != 0)
                {
                    // check new user email uniqueness
                    bool uniqueEmail = UniqueEmail(this.Email, UsersList);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that email address is allready exists ");
                    }

                    // check new user's user name uniqueness
                    bool uniqueUsername = UniqueUsername(this.UserName, UsersList);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that user-name is allready exists ");
                    }

                    // check new user's phone number uniqueness
                    bool uniquePhone = UniquePhone(this.PhoneNum, UsersList);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" there's an exist user with the same phone number ");
                    }
                }

                bool emailValid = EmailValidation(this.Email);
                if (!emailValid)
                {
                    throw new Exception(" wrong email foramt! ");
                }

                DBusers dbs = new DBusers();
                int good = dbs.InsertUser(this);     //get the id for the new user inserted
                if (good > 0) { return good; }
                else { return 0; }
            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // validation for user's email adress
        public bool EmailValidation(string email)
        {
            string patternEmail = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            Regex rgx = new Regex(patternEmail);
            bool valid = rgx.IsMatch(email);
            if (valid)
            {
                return true;
            }
            else return false;
        }

        // next 3 function are for checking unique valeus for new user registration
        public bool UniqueEmail(string email, List<UserService> UsersList)
        {
            bool unique = true;
            foreach (UserService u in UsersList)
            {
                if (u.Email == email)
                {
                    unique = false; break;
                }
            }
            return unique;
        }
        public bool UniqueUsername(string username, List<UserService> UsersList)
        {
            bool unique = true;
            foreach (UserService u in UsersList)
            {
                if (u.UserName == username)
                {
                    unique = false; break;
                }
            }
            return unique;
        }
        public bool UniquePhone(string phone, List<UserService> UsersList)
        {
            bool unique = true;
            foreach (UserService u in UsersList)
            {
                if (u.PhoneNum == phone)
                {
                    unique = false; break;
                }
            }
            return unique;
        }


        // update user's details with password
        public bool UpdateUserWithPassword()
        {
            UsersList = ReadAllUsers();

            try
            {
                foreach (UserService u in UsersList)
                {
                    if (u.UserID == this.UserID)
                    {
                        DBusers dbs = new DBusers();
                        int good = dbs.UpdateUserWithPassword(this);

                        if (good > 0) { return true; }
                        else { return false; }
                    }
                }
                throw new Exception(" no such user ");

            }
            catch (Exception exp)
            {
                throw new Exception(" didn't succeed in updating user's details " + exp.Message);
            }
        }


        // update another user's details, without password
        public bool UpdateUser()
        {
            UsersList = ReadAllUsers();
            try
            {
                foreach (UserService u in UsersList)
                {
                    if (u.UserID == this.UserID)
                    {
                        DBusers dbs = new DBusers();
                        int good = dbs.UpdateUserWithoutPassword(this);

                        if (good > 0) { return true; }
                        else { return false; }
                    }
                }
                throw new Exception(" no such user ");

            }
            catch (Exception exp)
            {
                throw new Exception(" didn't succeed in updating user's details " + exp.Message);
            }
        }


        // update user's teamID
        public bool UpdateUsersTeam(JsonElement[] users)
        {
            bool allUpdated = true;
            DBusers dBusers = new DBusers();

            foreach (JsonElement u in users)
            {
                int userID = Convert.ToInt32(u.GetProperty("userID").GetInt32());
                int teamID = Convert.ToInt32(u.GetProperty("teamID").GetInt32());
                //string userName = u.GetProperty("name").ToString();

                int rowsAffected = dBusers.UpdateTeamLeaderTeam(userID, teamID);
                if (rowsAffected == 0)
                {
                    allUpdated = false;
                    break;    // Exit loop early if any report fails to insert
                }
            }

            if (allUpdated)
                return allUpdated;
            else { throw new Exception(" לא הצליח לעדכן למשתמשים שבחרת את הצוות החדש "); }

        }


        // user log in
        public static int[] Login(string email, string password)
        {
            UsersList = ReadAllUsers();
            int[] userDetails = new int[2];

            foreach (UserService u in UsersList)
            {
                if (email == u.Email && password == u.Password)
                {
                    if (u.IsActive == false) { throw new Exception(" this user is not active "); }
                    userDetails = new int[2] { u.UserID, u.PermissionID };
                    return userDetails;
                }
            }
            return userDetails;
        }


        // get user by email
        public static UserService? GetUserByEmail(string email)
        {
            UsersList = ReadAllUsers();
            foreach (UserService user in UsersList)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }



    }
}
