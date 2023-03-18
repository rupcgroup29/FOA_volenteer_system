using FOA_Server.Models;
using FOA_Server.Models.DAL;
using System.Text.RegularExpressions;

namespace FOA_Server.Services
{
    public class UserServices
    {
        User user = new User();
        Permission permission = new Permission();
        HourReport hourReport = new HourReport();
        VolunteerProgram program = new VolunteerProgram();


        private static List<User> UsersList = new List<User>();
        private static List<Permission> permissionsList = new List<Permission>();
        private static List<HourReport> hourReportlist = new List<HourReport>();
        private static List<VolunteerProgram> volunteerProgramsList = new List<VolunteerProgram>();


        // read all users
        public List<User> ReadAllUsers()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadUsers();
        }

        //Insert new user
        public User InsertUser()
        {
            UsersList = ReadAllUsers();
            try
            {
                if (UsersList.Count != 0)
                {
                    // check new user email uniqueness
                    bool uniqueEmail = UniqueEmail(user.Email);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that email address is allready exists ");
                    }

                    // check new user's user name uniqueness
                    bool uniqueUsername = UniqueUsername(user.UserName);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that user name is allready exists ");
                    }

                    // check new user's phone number uniqueness
                    bool uniquePhone = UniquePhone(user.PhoneNum);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" there's an exist user with the same phone number ");
                    }
                }

                bool emailValid = EmailValidation(user.Email);
                if (!emailValid)
                {
                    throw new Exception(" wrong email foramt! ");
                }

                DBusers dbs = new DBusers();
                int good = dbs.InsertUsr(user);
                if (good > 0) { return user; }
                else { return null; }
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
        public bool UniqueEmail(string email)
        {
            bool unique = true;
            foreach (User u in UsersList)
            {
                if (u.Email == email)
                {
                    unique = false; break;
                }
            }
            return unique;
        }
        public bool UniqueUsername(string username)
        {
            bool unique = true;
            foreach (User u in UsersList)
            {
                if (u.UserName == username)
                {
                    unique = false; break;
                }
            }
            return unique;
        }
        public bool UniquePhone(int phone)
        {
            bool unique = true;
            foreach (User u in UsersList)
            {
                if (u.PhoneNum == phone)
                {
                    unique = false; break;
                }
            }
            return unique;
        }


        // update user's details
        public User UpdateUser()
        {
            UsersList = ReadAllUsers();
            try
            {
                foreach (User u in UsersList)
                {
                    if (u.UserID == user.UserID)
                    {
                        DBusers dbs = new DBusers();
                        int good = dbs.UpdateUser(user);

                        if (good > 0) { return user; }
                        else { return null; }
                    }
                }
                throw new Exception(" no such user ");

            }
            catch (Exception exp)
            {
                throw new Exception(" didn't succeed in updating user's details " + exp.Message);
            }
        }


        // user log in
        public User Login()
        {
            User user = new User();
            UsersList = ReadAllUsers();

            if (user.IsActive == false) { throw new Exception(" this user is not active "); }

            foreach (User u in UsersList)
            {
                if (user.Email == u.Email && user.Password == u.Password)
                {
                    return u;
                }
            }
            return null;
        }


        // read all Permissions
        public List<Permission> ReadAllPermissions()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadPermissions();
        }


        // read all Hour Reports
        public List<HourReport> ReadAllHourReports()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadHourReports();
        }


        // read all Volunteer Programs
        public List<VolunteerProgram> ReadAllVolunteerPrograms()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadVolunteerPrograms();
        }



    }
}
