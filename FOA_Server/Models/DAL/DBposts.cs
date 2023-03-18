using System.Data.SqlClient;
using System.Data;

namespace FOA_Server.Models.DAL
{
    public class DBposts : DBservices
    {

        // POSTS
        // This method reads all Posts
        public List<Post> ReadPosts()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadPosts", con);      // create the command

            List<Post> list = new List<Post>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Post p = new Post();
                    p.PostID = Convert.ToInt32(dataReader["PostID"]);
                    p.UrlLink = dataReader["UrlLink"].ToString();
                    p.Description = dataReader["Description"].ToString();
                    p.KeyWordsAndHashtages = dataReader["KeyWordsAndHashtages"].ToString();
                    p.Threat = dataReader["Threat"].ToString();
                    p.Screenshot = dataReader["Screenshot"].ToString();
                    p.Date = Convert.ToDateTime(dataReader["Date"]);
                    p.AmoutOfLikes = Convert.ToInt32(dataReader["AmoutOfLikes"]);
                    p.AmoutOfShares = Convert.ToInt32(dataReader["AmoutOfShares"]);
                    p.AmoutOfComments = Convert.ToInt32(dataReader["AmoutOfComments"]);
                    p.PostStatus = Convert.ToInt32(dataReader["PostStatus"]);
                    p.RemovalStatus = Convert.ToInt32(dataReader["RemovalStatus"]);
                    p.UserID = Convert.ToInt32(dataReader["UserID"]);
                    p.PlatformID = Convert.ToInt32(dataReader["PlatformID"]);
                    p.CategoryID = Convert.ToInt32(dataReader["CategoryID"]);
                    p.PostStatusManager = Convert.ToInt32(dataReader["PostStatusManager"]);
                    p.RemovalStatusManager = Convert.ToInt32(dataReader["RemovalStatusManager"]);
                    p.RemovalStatusManager = Convert.ToInt32(dataReader["RemovalStatusManager"]);
                    p.Country = Convert.ToInt32(dataReader["Country"]);
                    p.Language = Convert.ToInt32(dataReader["Language"]);

                    list.Add(p);
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


        // IHRA
        // This method reads all IHRA
        public List<IHRA> ReadIHRAs()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadIHRAs", con);      // create the command

            List<IHRA> list = new List<IHRA>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    IHRA i = new IHRA();
                    i.CategoryID = Convert.ToInt32(dataReader["CategoryID"]);
                    i.CategoryName = dataReader["CategoryName"].ToString();

                    list.Add(i);
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



        // Platform
        // This method reads all Platforms
        public List<Platform> ReadPlatforms()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadPlatforms", con);      // create the command

            List<Platform> list = new List<Platform>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Platform p = new Platform();
                    p.PlatformID = Convert.ToInt32(dataReader["PlatformID"]);
                    p.PlatformName = dataReader["PlatformName"].ToString();

                    list.Add(p);
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


    }
}
