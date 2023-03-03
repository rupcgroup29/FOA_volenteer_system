using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using FOA_Server.Models;

public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // This method creates a connection to the database according to the connectionString name in the web.config 
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString(conString);
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

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
        cmd.Parameters.AddWithValue("@ProgramID", user.ProgramID);
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
        cmd.Parameters.AddWithValue("@ProgramID", user.ProgramID);
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

        cmd = CreateCommandWithStoredProcedureRead("spReadVolunteerProgramss", con);      // create the command

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


    // TEAM
    // This method reads all Teams
    public List<Team> ReadTeams()
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

        cmd = CreateCommandWithStoredProcedureRead("spReadTeams", con);      // create the command

        List<Team> list = new List<Team>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Team t = new Team();
                t.TeamID = Convert.ToInt32(dataReader["TeamID"]);
                t.TeamName = dataReader["TeamName"].ToString();
                t.Description = dataReader["Description"].ToString();
                t.TeamLeader = dataReader["TeamLeader"].ToString();

                list.Add(t);
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



    // Language
    // This method reads all Language
    public List<Language> ReadLanguages()
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

        cmd = CreateCommandWithStoredProcedureRead("spReadLanguages", con);      // create the command

        List<Language> list = new List<Language>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Language l = new Language();
                l.Lang = dataReader["Lang"].ToString();

                list.Add(l);
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



    // Country
    // This method reads all Countries
    public List<Country> ReadCountries()
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

        cmd = CreateCommandWithStoredProcedureRead("spReadCountries", con);      // create the command

        List<Country> list = new List<Country>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Country c = new Country();
                c._Country = dataReader["_Country"].ToString();

                list.Add(c);
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
