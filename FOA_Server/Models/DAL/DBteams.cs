﻿using System.Data.SqlClient;
using System.Data;

namespace FOA_Server.Models.DAL
{
    public class DBteams : DBservices
    {

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
                    t.TeamLeader = Convert.ToInt32(dataReader["TeamLeader"]);

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


        // This method inserts a new team to the team table 
        public int InsertTeam(Team team)
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

            cmd = CreateCommandWithStoredProcedureInsert("spInsertTeam", con, team);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();  // execute the command
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





        // Create the SqlCommand using a stored procedure for READ
        private SqlCommand CreateCommandWithStoredProcedureRead(string spName, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for INSERT team
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Team team)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@TeamName", team.TeamName);
            cmd.Parameters.AddWithValue("@Description", team.Description);
            cmd.Parameters.AddWithValue("@TeamLeader", team.TeamLeader);

            return cmd;
        }





    }
}
