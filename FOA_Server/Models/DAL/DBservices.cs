using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ServerTest.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
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

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //--------------------------------------------------------------------------------------------------
    // Read all orders from orders table 
    //--------------------------------------------------------------------------------------------------
    public List<Order> ReadOrders()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRead("spReadAllOrders_A", con);

        List<Order> OrderList = new List<Order>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Order o = new Order();
                o.Id = Convert.ToInt32(dataReader["Id"]);
                o.NumOfPeople = Convert.ToInt32(dataReader["NumOfPeople"]);
                o.RestaurantID = Convert.ToInt32(dataReader["RestaurantID"]);

                OrderList.Add(o);
            }
            return OrderList;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    //--------------------------------------------------------------------------------------------------
    // inserts an order to the orders table 
    //--------------------------------------------------------------------------------------------------
    public int InsertOrder(Order order)
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

        cmd = CreateCommandWithStoredProcedureInsert("spNewOrder_A", con, order);// create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
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
    private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Order order)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@numOfPeople", order.NumOfPeople);
        cmd.Parameters.AddWithValue("@RestaurantID", order.RestaurantID);

        return cmd;
    }




    //--------------------------------------------------------------------------------------------------
    // Read all restaurants from restaurant table 
    //--------------------------------------------------------------------------------------------------
    public List<Restaurant> ReadRestaurants()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRead("spReadAllRestaurants_A", con);

        List<Restaurant> RestaurantList = new List<Restaurant>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Restaurant r = new Restaurant();
                r.Id = Convert.ToInt32(dataReader["Id"]);
                r.Name = dataReader["Name"].ToString();
                r.Rank = Convert.ToDouble(dataReader["Rank"]);
                r.Type = dataReader["Type"].ToString();

                RestaurantList.Add(r);
            }
            return RestaurantList;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    private SqlCommand CreateCommandWithStoredProcedureRead(String spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        return cmd;
    }

}
