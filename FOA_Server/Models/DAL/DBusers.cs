using System.Data.SqlClient;
using System.Data;
using FOA_Server.Models;

namespace FOA_Server.Models.DAL
{
    public class DBusers : DBservices
    {
        // USERS
        // This method reads all the Users
        public List<User> ReadUsers()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureRead("spReadUsers", con);      // create the command

            List<User> list = new List<User>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User usr = new User();
                    usr.UserID = Convert.ToInt32(dataReader["UserID"]);
                    usr.FirstName = dataReader["FirstName"].ToString();
                    usr.Surname = dataReader["Surname"].ToString();
                    usr.UserName = dataReader["UserName"].ToString();
                    usr.Email = dataReader["Email"].ToString();
                    usr.Password = dataReader["Password"].ToString();
                    usr.PhoneNum = Convert.ToInt32(dataReader["PhoneNum"]);
                    usr.RoleDescription = dataReader["RoleDescription"].ToString();
                    usr.PermissionID = Convert.ToInt32(dataReader["PermissionID"]);
                    usr.VolunteerProgram = dataReader["VolunteerProgram"].ToString();
                    usr.TeamID = Convert.ToInt32(dataReader["TeamID"]);

                    list.Add(usr);
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        // This method inserts a user to the user table 
        public int InsertUsr(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureInsert("spInsertUser", con, user);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        // This method update a user to the user table 
        public int UpdateUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureUpdate("spUpdateUser", con, user);     // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();  // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        // Create the SqlCommand using a stored procedure for Read
        private SqlCommand CreateCommandWithStoredProcedureRead(string spName, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert & Update User
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, User user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@Surname", user.Surname);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            cmd.Parameters.AddWithValue("@RoleDescription", user.RoleDescription);
            cmd.Parameters.AddWithValue("@PermissionID", user.PermissionID);
            cmd.Parameters.AddWithValue("@ProgramID", user.VolunteerProgram);
            cmd.Parameters.AddWithValue("@TeamID", user.TeamID);

            return cmd;
        }

        private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, User user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@Surname", user.Surname);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            cmd.Parameters.AddWithValue("@RoleDescription", user.RoleDescription);
            cmd.Parameters.AddWithValue("@PermissionID", user.PermissionID);
            cmd.Parameters.AddWithValue("@ProgramID", user.VolunteerProgram);
            cmd.Parameters.AddWithValue("@TeamID", user.TeamID);

            return cmd;
        }


        // Volunteer Programs
        // This method reads all Volunteer Programs
        public List<VolunteerProgram> ReadVolunteerPrograms()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureRead("spReadVolunteerProgram", con);      // create the command

            List<VolunteerProgram> list = new List<VolunteerProgram>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    VolunteerProgram vp = new VolunteerProgram();
                    vp.ProgramID = Convert.ToInt32(dataReader["ProgramID"]);
                    vp.ProgramName = dataReader["ProgramName"].ToString();

                    list.Add(vp);
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        // Permission
        // This method reads all Volunteer Programs
        public List<Permission> ReadPermissions()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureRead("spReadPermissions", con);      // create the command

            List<Permission> list = new List<Permission>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Permission per = new Permission();
                    per.PermissionID = Convert.ToInt32(dataReader["PermissionID"]);
                    per.PermissionName = dataReader["PermissionName"].ToString();

                    list.Add(per);
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        // HourReport
        // This method reads all Hour Reports
        public List<HourReport> ReadHourReports()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureRead("spReadHourReports", con);      // create the command

            List<HourReport> list = new List<HourReport>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    HourReport h = new HourReport();
                    h.ReportID = Convert.ToInt32(dataReader["ReportID"]);
                    h.Date = Convert.ToDateTime(dataReader["Date"]);
                    //  h.StartTime = Convert.ToDateTime(dataReader["StartTime"]);
                    //h.EndTime = Convert.ToDateTime(dataReader["EndTime"]);
                    h.Status = Convert.ToInt32(dataReader["Status"]);

                    list.Add(h);
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                Console.WriteLine("Error");
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }






    }

}
