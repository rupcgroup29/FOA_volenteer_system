using System.Data.SqlClient;
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

        // This method reads all Teams
        public List<Object> ReadTeamsDetails()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadTeamSummary", con);      // create the command

            List<Object> list = new List<Object>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(new
                    {
                        TeamID = Convert.ToInt32(dataReader["TeamID"]),
                        Fullname = dataReader["Fullname"].ToString(),
                        TeamName = dataReader["TeamName"].ToString(),
                        NoOfVolunteerUsers = Convert.ToInt32(dataReader["NoOfVolunteerUsers"])
                    });
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

        // This method reads all Teams
        public Object ReadTeamDetailsByID(int teamID)
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

            cmd = CreateCommandWithStoredProcedureRead("spReadTeamsByTeamID", con, teamID);      // create the command
            Object obj = new Object();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    obj = new
                    {
                        TeamID = Convert.ToInt32(dataReader["TeamID"]),
                        TeamName = dataReader["TeamName"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Fullname = dataReader["fullName"].ToString(),
                        UserID = Convert.ToInt32(dataReader["UserID"])
                    };
                }
                return obj;
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

        // This method reads all Team Leaders with no leading team yet
        public List<Object> ReadTeamLeadersWithoutTeamToLead()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadNoTeamLeader", con);      // create the command

            List<Object> list = new List<Object>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(new
                    {
                        Fullname = dataReader["Fullname"].ToString(),
                        UserName = dataReader["UserName"].ToString(),
                        UserID = Convert.ToInt32(dataReader["UserID"])
                    });
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

        // This method reads all users un-approved hour reports by team ID
        public List<Object> ReadUsersHourReportsInTeam(int teamID)
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

            cmd = CreateCommandWithStoredProcedureRead("spReadUnApprovedHourReportsByTeamID", con, teamID);      // create the command

            List<Object> list = new List<Object>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    DateTime date = Convert.ToDateTime(dataReader["Date"]);

                    TimeSpan timeStart = (TimeSpan)dataReader["StartTime"];
                    DateTime dateTimeStart = date.Add(timeStart);

                    TimeSpan timeEnd = (TimeSpan)dataReader["EndTime"];
                    DateTime dateTimeEnd = date.Add(timeEnd);

                    list.Add(new
                    {
                        ReportID = Convert.ToInt32(dataReader["ReportID"]),
                        Date = date,
                        StartTime = dateTimeStart,
                        EndTime = dateTimeEnd,
                        Status = Convert.ToInt32(dataReader["Status"]),
                        Count = Convert.ToInt32(dataReader["Count"]),
                        userName = dataReader["userName"].ToString(),
                        UserID = Convert.ToInt32(dataReader["UserID"])
                    });
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
                int lastId = Convert.ToInt32(cmd.ExecuteScalar()); //Executescalar requires the command to have a transaction (id)
                return lastId;
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


        // This method update a team to the team table 
        public int UpdateTeam(Team team)
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

            cmd = CreateCommandWithStoredProcedureUpdate("spUpdateTeam", con, team);     // create the command

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

        // Create the SqlCommand using a stored procedure for READ
        private SqlCommand CreateCommandWithStoredProcedureRead(string spName, SqlConnection con, int teamID)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@TeamID", teamID);

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
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for UPDATE User
        private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, Team team)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@TeamID", team.TeamID);
            cmd.Parameters.AddWithValue("@TeamName", team.TeamName);
            cmd.Parameters.AddWithValue("@Description", team.Description);
            cmd.Parameters.AddWithValue("@TeamLeader", team.TeamLeader);

            return cmd;
        }




    }
}
