﻿using System.Text.RegularExpressions;

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
        public bool IsActive { get; set; }
        public int PermissionID { get; set; }
        public int ProgramID { get; set; }
        public int TeamID { get; set; }

        private static List<User> UsersList = new List<User>();

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

        // read all users
        public List<User> ReadAllUsers()
        {
            DBservices dbs = new DBservices();
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
                    bool uniqueEmail = UniqueEmail(this.Email);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that email address is allready exists ");
                    }

                    // check new user's user name uniqueness
                    bool uniqueUsername = UniqueUsername(this.UserName);
                    if (!uniqueEmail)
                    {
                        throw new Exception(" user under that user name is allready exists ");
                    }

                    // check new user's phone number uniqueness
                    bool uniquePhone = UniquePhone(this.PhoneNum);
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

                DBservices dbs = new DBservices();
                int good = dbs.InsertUsr(this);
                if (good > 0) { return this; }
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
                    if (u.UserID == this.UserID)
                    {
                        DBservices dbs = new DBservices();
                        int good = dbs.UpdateUser(this);

                        if (good > 0) { return this; }
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
            List<User> UserList = user.ReadAllUsers();

            foreach (User u in UserList)
            {
                if (this.Email == u.Email && this.Password == u.Password)
                {
                    return u;
                }
            }
            return null;
        }


    }
}
