using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HW1.Models;

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

        String cStr = BuildInsertCommand(user);      // helper method to build the insert string

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

    // This method inserts a flat to the flat table 
    public int InsertFlt(Flat flat)
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

        String cStr = BuildInsertCommand(flat);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureInsert("spInsertFlat", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
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

    // This method inserts a vaca to the vaca table 
    public int InsertVaca(Vacation vaca)
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

        String cStr = BuildInsertCommand(vaca);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureInsert("spInsertVacation", con, vaca);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
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

    // This method update a vacation to the vacation table 
    public int UpdateVaca(Vacation vaca)
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

        cmd = CreateCommandWithStoredProcedureUpdate("spUpdateVacation", con, vaca);     // create the command

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

    // This method update a flat to the flat table 
    public int UpdateFlat(Flat flat)
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

        cmd = CreateCommandWithStoredProcedureInsert("spUpdateFlat", con, flat);     // create the command

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
                usr.FirstName = dataReader["FirstName"].ToString();
                usr.FamilyName = dataReader["FamilyName"].ToString();
                usr.Email = dataReader["Email"].ToString();
                usr.Password = dataReader["Password"].ToString();
                usr.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                usr.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);

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

    // This method reads all the Flats
    public List<Flat> ReadFlats()
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

        cmd = CreateCommandWithStoredProcedureRead("spReadFlats", con);      // create the command

        List<Flat> list = new List<Flat>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = Convert.ToInt32(dataReader["Id"]);
                f.City = dataReader["City"].ToString();
                f.Address = dataReader["Address"].ToString();
                f.NumberOfRooms = Convert.ToInt32(dataReader["NumberOfRooms"]);
                f.Price = Convert.ToInt32(dataReader["Price"]);

                list.Add(f);
            }
            return list;
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

    // This method reads all the Vacations
    public List<Vacation> ReadVacations()
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

        cmd = CreateCommandWithStoredProcedureRead("spReadVacations", con);      // create the command

        List<Vacation> list = new List<Vacation>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.Id = Convert.ToInt32(dataReader["Id"]);
                v.UserId = (dataReader["UserId"]).ToString();
                v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
                v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

                list.Add(v);
            }
            return list;
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

    // Build the Insert User command String
    private String BuildInsertCommand(User user)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')", user.FirstName, user.FamilyName, user.Email, user.Password);
        String prefix = "INSERT INTO UsersTable " + "(firstName, familyName, email, password) ";
        command = prefix + sb.ToString();

        return command;
    }

    // Build the Insert Flat command String
    private String BuildInsertCommand(Flat flat)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', {2}, {3})", flat.City, flat.Address, flat.NumberOfRooms, flat.Price);
        String prefix = "INSERT INTO UsersTable " + "(city, address, numberOfRooms, price) ";
        command = prefix + sb.ToString();

        return command;
    }

    // Build the Insert Vacation command String
    private String BuildInsertCommand(Vacation vaca)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', {1}, '{2}', '{3}')", vaca.UserId, vaca.FlatId, vaca.StartDate, vaca.EndDate);
        String prefix = "INSERT INTO UsersTable " + "(userId, flatId, startDate, endDate) ";
        command = prefix + sb.ToString();

        return command;
    }

    // Build the Update command String
    private String BuildUpdateCommand(User user)
    {
        StringBuilder sb = new StringBuilder();

        //	update UsersTable set firstName = @familyName, familyName = @familyName, password = @password where	email = @email
        string command = sb.AppendFormat("update UsersTable set firstName = '{0}', familyName = '{1}', password = '{2}' where email = '{3}'", user.FirstName, user.FamilyName, user.Password, user.Email).ToString();
        return command;
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

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
        cmd.Parameters.AddWithValue("@isActive", user.IsActive);

        return cmd;
    }

    // Create the SqlCommand using a stored procedure for Insert Vacation
    private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Vacation vaca)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        //cmd.Parameters.AddWithValue("@id", vaca.Id);
        cmd.Parameters.AddWithValue("@userId", vaca.UserId);
        cmd.Parameters.AddWithValue("@flatId", vaca.FlatId);
        cmd.Parameters.AddWithValue("@startDate", vaca.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vaca.EndDate);

        return cmd;
    }

    // Create the SqlCommand using a stored procedure for Insert & Update Flat
    private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Flat flat)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", flat.Id);
        cmd.Parameters.AddWithValue("@city", flat.City);
        cmd.Parameters.AddWithValue("@address", flat.Address);
        cmd.Parameters.AddWithValue("@numberOfRooms", flat.NumberOfRooms);
        cmd.Parameters.AddWithValue("@price", flat.Price);

        return cmd;
    }

    // Create the SqlCommand using a stored procedure for Update Vacation
    private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, Vacation vaca)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", vaca.Id);
        cmd.Parameters.AddWithValue("@userId", vaca.UserId);
        cmd.Parameters.AddWithValue("@flatId", vaca.FlatId);
        cmd.Parameters.AddWithValue("@startDate", vaca.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vaca.EndDate);

        return cmd;
    }
    private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, User user)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
        cmd.Parameters.AddWithValue("@isActive", user.IsActive);

        return cmd;
    }


    //get all cities avg price per night - by the user's choosen month
    public Object GetCityAvgPrice(int month)
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

        cmd = CreateCommandWithStoredProcedureReadCityAvg("spAvgPricePerNight", month, con);      // create the command

        List<Object> listObjs = new List<Object>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                listObjs.Add(new
                {
                    city = (dataReader["city"]).ToString(),
                    avg_price = Convert.ToDouble(dataReader["avg_price"])
                });
            }
            return listObjs;
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

    private SqlCommand CreateCommandWithStoredProcedureReadCityAvg(string spName, int month, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@month", month);

        return cmd;
    }
}
