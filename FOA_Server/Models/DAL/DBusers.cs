using System.Data;
using System.Data.SqlClient;

namespace FOA_Server.Models.DAL
{
    public class DBusers : DBservices
    {
        // USERS
        // This method reads all the Users
        public List<UserService> ReadUsers()
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

            List<UserService> list = new List<UserService>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserService usr = new UserService();
                    usr.UserID = Convert.ToInt32(dataReader["UserID"]);
                    usr.FirstName = dataReader["FirstName"].ToString();
                    usr.Surname = dataReader["Surname"].ToString();
                    usr.UserName = dataReader["UserName"].ToString();
                    usr.Email = dataReader["Email"].ToString();
                    usr.Password = dataReader["Password"].ToString();
                    usr.IsActive = Convert.ToBoolean(dataReader["TeamID"]);
                    usr.PhoneNum = dataReader["PhoneNum"].ToString();
                    usr.RoleDescription = dataReader["RoleDescription"].ToString();
                    usr.PermissionID = Convert.ToInt32(dataReader["PermissionID"]);
                    usr.ProgramID = Convert.ToInt32(dataReader["ProgramID"]);
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
       
        // This method reads all the Users
        public List<UserService> ReadAllUsersWithNames()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadUsersScreen", con);      // create the command

            List<UserService> list = new List<UserService>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserService usr = new UserService();
                    usr.UserID = Convert.ToInt32(dataReader["UserID"]);
                    usr.FirstName = dataReader["FirstName"].ToString();
                    usr.Surname = dataReader["Surname"].ToString();
                    usr.UserName = dataReader["UserName"].ToString();
                    usr.Email = dataReader["Email"].ToString();
                    usr.PermissionName = dataReader["PermissionName"].ToString();
                    usr.TeamName = dataReader["TeamName"].ToString();

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


        // This method reads user's details by id with password
        public UserService ReadUserByIDWithPassword(int userId)
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

            cmd = CreateCommandWithStoredProcedureReadByIDWithPassword("spReadUserByIdWithPassword", con, userId);      // create the command

            UserService user = new UserService();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    user.UserID = Convert.ToInt32(dataReader["UserID"]);
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.Surname = dataReader["Surname"].ToString();
                    user.UserName = dataReader["UserName"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.IsActive = Convert.ToBoolean(dataReader["TeamID"]);
                    user.PhoneNum = dataReader["PhoneNum"].ToString();
                    user.RoleDescription = dataReader["RoleDescription"].ToString();
                    user.PermissionID = Convert.ToInt32(dataReader["PermissionID"]);
                    user.PermissionName = dataReader["PermissionName"].ToString();
                    user.ProgramID = Convert.ToInt32(dataReader["ProgramID"]);
                    user.ProgramName = dataReader["ProgramName"].ToString();
                    user.TeamID = Convert.ToInt32(dataReader["TeamID"]);
                    user.TeamName = dataReader["TeamName"].ToString();
                    user.Password = dataReader["Password"].ToString();
                }
                return user;
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

        // This method reads user's details by id without password
        public UserService ReadUserByIDWithoutPassword(int userId)
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

            cmd = CreateCommandWithStoredProcedureReadByIDWitoutPassword("spReadUserByIdWithoutPassword", con, userId);      // create the command

            UserService user = new UserService();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    user.UserID = Convert.ToInt32(dataReader["UserID"]);
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.Surname = dataReader["Surname"].ToString();
                    user.UserName = dataReader["UserName"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.IsActive = Convert.ToBoolean(dataReader["TeamID"]);
                    user.PhoneNum = dataReader["PhoneNum"].ToString();
                    user.RoleDescription = dataReader["RoleDescription"].ToString();
                    user.PermissionID = Convert.ToInt32(dataReader["PermissionID"]);
                    user.PermissionName = dataReader["PermissionName"].ToString();
                    user.ProgramID = Convert.ToInt32(dataReader["ProgramID"]);
                    user.ProgramName = dataReader["ProgramName"].ToString();
                    user.TeamID = Convert.ToInt32(dataReader["TeamID"]);
                    user.TeamName = dataReader["TeamName"].ToString();
                }
                return user;
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
        public int InsertUser(UserService user)
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
        public int UpdateUserWithPassword(UserService user)
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

            cmd = CreateCommandWithStoredProcedureUpdateWithPassword("spUpdateUserWithPassword", con, user);     // create the command

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

        // This method update a user without password to the user table 
        public int UpdateUserWithoutPassword(UserService user)
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

            cmd = CreateCommandWithStoredProcedureUpdateWithoutPassword("spUpdateUserWithoutPassword", con, user);     // create the command

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

        // This method update a user to the user table 
        public int UpdateUserPassword(string email, string password)
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

            cmd = CreateCommandWithStoredProcedureUpdate("spUpdateUserPassword", con, email, password);     // create the command

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

        // Create the SqlCommand using a stored procedure for Read user by ID without password
        private SqlCommand CreateCommandWithStoredProcedureReadByIDWitoutPassword(string spName, SqlConnection con, int userId)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@UserID", userId);

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Read user by ID with password
        private SqlCommand CreateCommandWithStoredProcedureReadByIDWithPassword(string spName, SqlConnection con, int userId)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@UserID", userId);

            return cmd;
        }


        // Create the SqlCommand using a stored procedure for Insert & Update User
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, UserService user)
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
            cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            cmd.Parameters.AddWithValue("@RoleDescription", user.RoleDescription);
            cmd.Parameters.AddWithValue("@PermissionID", user.PermissionID);
            cmd.Parameters.AddWithValue("@TeamID", user.TeamID);
            cmd.Parameters.AddWithValue("@ProgramID", user.ProgramID);

            return cmd;
        }

        private SqlCommand CreateCommandWithStoredProcedureUpdateWithPassword(String spName, SqlConnection con, UserService user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@UserID", user.UserID);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@Surname", user.Surname);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            cmd.Parameters.AddWithValue("@RoleDescription", user.RoleDescription);
            cmd.Parameters.AddWithValue("@PermissionID", user.PermissionID);
            cmd.Parameters.AddWithValue("@ProgramID", user.ProgramID);
            cmd.Parameters.AddWithValue("@TeamID", user.TeamID);

            return cmd;
        }
    
        private SqlCommand CreateCommandWithStoredProcedureUpdateWithoutPassword(String spName, SqlConnection con, UserService user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@UserID", user.UserID);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@Surname", user.Surname);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            cmd.Parameters.AddWithValue("@RoleDescription", user.RoleDescription);
            cmd.Parameters.AddWithValue("@PermissionID", user.PermissionID);
            cmd.Parameters.AddWithValue("@ProgramID", user.ProgramID);
            cmd.Parameters.AddWithValue("@TeamID", user.TeamID);

            return cmd;
        }

        private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, string email, string password)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@LastReasetPassword", DateTime.Now.AddMinutes(5));

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

        // This method insert a new Volunteer Program
        public int InsertVolunteerProgram(VolunteerProgram vp)
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

            cmd = CreateCommandWithStoredProcedureInsert("spInsertProgram", con, vp);         // create the command

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

        // Create the SqlCommand using a stored procedure for Insert & Update User
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, VolunteerProgram vp)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@ProgramName", vp.ProgramName);

            return cmd;
        }



        // Permission
        // This method reads all Permission
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
