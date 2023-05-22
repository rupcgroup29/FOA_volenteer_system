using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace FOA_Server.Models.DAL
{
    public class DBposts : DBservices
    {
        // POSTS
        // This method reads Posts without status by menager name
        public List<Post> ReadPostsWitoutStatusByMenagerName()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadPostsWitoutStatusByMenagerName", con);      // create the command

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
                    p.Threat = Convert.ToInt32(dataReader["Threat"]);
                    p.Screenshot = dataReader["Screenshot"].ToString();
                    p.AmountOfLikes = Convert.ToInt32(dataReader["AmountOfLikes"]);
                    p.AmountOfShares = Convert.ToInt32(dataReader["AmountOfShares"]);
                    p.AmountOfComments = Convert.ToInt32(dataReader["AmountOfComments"]);
                    p.PostStatus = Convert.ToInt32(dataReader["PostStatus"]);
                    p.RemovalStatus = Convert.ToInt32(dataReader["RemovalStatus"]);
                    p.UserID = Convert.ToInt32(dataReader["UserID"]);
                    p.PlatformID = Convert.ToInt32(dataReader["PlatformID"]);
                    p.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                    p.LanguageID = Convert.ToInt32(dataReader["LanguageID"]);
                    p.InsertDate = Convert.ToDateTime(dataReader["CreatedAt"]);

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

        // This method insert a Post
        public int InsertPost(Post post)
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

            cmd = CreateCommandWithStoredProcedureInsert("spInsertPost", con, post);             // create the command

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

        // This method update Post Status in the system & removal status
        public int UpdatePostStatus(UpdatePostStatus postStatusUpdate)
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

            cmd = CreateCommandWithStoredProcedureUpdatePostStatus("spUpdatePost", con, postStatusUpdate);
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




        // READ POST
        // This method reads all Posts without keywords & IHRA category
        public List<ReadPost> ReadPosts()
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

            List<ReadPost> list = new List<ReadPost>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ReadPost p = new ReadPost();
                    p.PostID = Convert.ToInt32(dataReader["PostID"]);
                    p.UrlLink = dataReader["UrlLink"].ToString();
                    p.Description = dataReader["Description"].ToString();
                    p.Threat = Convert.ToInt32(dataReader["Threat"]);
                    p.Screenshot = dataReader["Screenshot"].ToString();
                    p.AmountOfLikes = Convert.ToInt32(dataReader["AmountOfLikes"]);
                    p.AmountOfShares = Convert.ToInt32(dataReader["AmountOfShares"]);
                    p.AmountOfComments = Convert.ToInt32(dataReader["AmountOfComments"]);
                    p.PostStatus = Convert.ToInt32(dataReader["PostStatus"]);
                    p.RemovalStatus = Convert.ToInt32(dataReader["RemovalStatus"]);
                    p.UserName = dataReader["UserName"].ToString();
                    p.PlatformName = dataReader["PlatformName"].ToString();
                    p.StatusManagerName = dataReader["StatusManagerName"].ToString();
                    p.RemovalManagerName = dataReader["RemovalManagerName"].ToString();
                    p.CountryName = dataReader["CountryName"].ToString();
                    p.LanguageName = dataReader["LanguageName"].ToString();
                    p.InsertDate = Convert.ToDateTime(dataReader["CreatedAt"]);

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

        // This method read for a post by its ID without keywords & IHRA category 
        public ReadPost ReadPostByID(int postID)
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

            cmd = CreateCommandWithStoredProcedureRead("spReadPostsByID", con, postID);      // create the command            

            try
            {
                ReadPost p = new ReadPost();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    p.PostID = Convert.ToInt32(dataReader["PostID"]);
                    p.UrlLink = dataReader["UrlLink"].ToString();
                    p.Description = dataReader["Description"].ToString();
                    p.Threat = Convert.ToInt32(dataReader["Threat"]);
                    p.Screenshot = dataReader["Screenshot"].ToString();
                    p.AmountOfLikes = Convert.ToInt32(dataReader["AmountOfLikes"]);
                    p.AmountOfShares = Convert.ToInt32(dataReader["AmountOfShares"]);
                    p.AmountOfComments = Convert.ToInt32(dataReader["AmountOfComments"]);
                    p.PostStatus = Convert.ToInt32(dataReader["PostStatus"]);
                    p.RemovalStatus = Convert.ToInt32(dataReader["RemovalStatus"]);
                    p.UserName = dataReader["UserName"].ToString();
                    p.PlatformName = dataReader["PlatformName"].ToString();
                    p.StatusManagerName = dataReader["StatusManagerName"].ToString();
                    p.RemovalManagerName = dataReader["RemovalManagerName"].ToString();
                    p.CountryName = dataReader["CountryName"].ToString();
                    p.LanguageName = dataReader["LanguageName"].ToString();
                    p.InsertDate = Convert.ToDateTime(dataReader["CreatedAt"]);

                }
                return p;
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

        // This method reads IHRA category names by postID
        public string[] ReadIHRAsPerPostID(int postID)
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

            cmd = CreateCommandWithStoredProcedureRead("spGetCategoriesByPostID", con, postID);      // create the command

            var size = 0;
            string[] array = new string[6];

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    array[size++] = dataReader["CategoryName"].ToString();
                }

                // count the non-null values in the original array
                int nonNullCount = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        break;
                    }
                    nonNullCount++;
                }

                // create a new array with the non-null count
                string[] newArray = new string[nonNullCount];

                // copy non-null values to the new array
                for (int i = 0; i < nonNullCount; i++)
                {
                    newArray[i] = array[i];
                }

                return newArray;
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

        // This method reads key words and hashtages by postID
        public string[] ReadKeyWordsAndHashtagesPerPostID(int postID)
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

            cmd = CreateCommandWithStoredProcedureRead("spGetKeyWordsByPostID", con, postID);      // create the command

            var size = 0;
            string[] array = new string[20];

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    array[size++] = dataReader["KeyWordsAndHashtages"].ToString();

                }

                // count the non-null values in the original array
                int nonNullCount = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        break;
                    }
                    nonNullCount++;
                }

                // create a new array with the non-null count
                string[] newArray = new string[nonNullCount];

                // copy non-null values to the new array
                for (int i = 0; i < nonNullCount; i++)
                {
                    newArray[i] = array[i];
                }

                return newArray;
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

        // This method reads the most exposure Key Words / Hashtag from posts
        public string ReadExposureKeyWordsAndHashtags()
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

            cmd = CreateCommandWithStoredProcedureRead("spRecomadationKeyWord", con);      // create the command

            string result = "";

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    result = dataReader["KeyWords"].ToString();
                }

                return result;
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

        // This method reads the most exposure Platform from posts
        public string ReadExposurePlatform()
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

            cmd = CreateCommandWithStoredProcedureRead("spRecomadationPlatform", con);      // create the command

            string result = "";

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    result = dataReader["Platform"].ToString();
                }

                return result;
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

        // This method reads the most exposure Language from posts
        public string ReadExposureLanguage()
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

            cmd = CreateCommandWithStoredProcedureRead("spRecomadationLng", con);      // create the command

            string result = "";

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    result = dataReader["Lng"].ToString();
                }

                return result;
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



        // KeyWordsAndHashtages
        //This method insert a new KeyWordsAndHashtages and post id to many-to-many table 
        public int InsertKeyWordsAndHashtagesToPost(int postID, int keyWordsAndHashtages)
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
            cmd = CreateCommandWithStoredProcedureInsertK("spInsertPostKeyWordsAndHashtages", con, postID, keyWordsAndHashtages);             // create the command

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

        // This method reads all KeyWordsAndHashtages
        public List<KeyWordsAndHashtages> ReadKeyWordsAndHashtages()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadKeyWordsAndHashtages", con);      // create the command

            List<KeyWordsAndHashtages> list = new List<KeyWordsAndHashtages>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    KeyWordsAndHashtages k = new KeyWordsAndHashtages();
                    k.KH = dataReader["KeyWordsAndHashtages"].ToString();
                    k.KH_ID = Convert.ToInt32(dataReader["ID"]);

                    list.Add(k);
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

        // This method reads TOP 5 of KeyWordsAndHashtages
        public List<string> ReadTop5KeyWordsAndHashtages()
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

            cmd = CreateCommandWithStoredProcedureRead("spReadTop5KW", con);      // create the command

            List<string> list = new List<string>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(dataReader["KeyWordsAndHashtages"].ToString());
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

        // This method insert a new KeyWordsAndHashtages
        public int InsertKeyWordsAndHashtages(KeyWordsAndHashtages keyWordsAndHashtages)
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
            cmd = CreateCommandWithStoredProcedureInsert("spInsertKeyWordsAndHashtages", con, keyWordsAndHashtages);             // create the command

            try
            {
                int lastId = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command //נותן את האופציה לקבל ערך בחזרה מפונקציית ההכנסה (id)
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
                    l.LanguageName = dataReader["LanguageName"].ToString();
                    l.LanguageID = Convert.ToInt32(dataReader["LanguageID"]);

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

        // This method insert a new language
        public int InsertLanguage(Language language)
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
            cmd = CreateCommandWithStoredProcedureInsert("spInsertLanguage", con, language);             // create the command

            try
            {
                int lastId = Convert.ToInt32(cmd.ExecuteScalar());//Executescalar requires the command to have a transaction
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
                    c.CountryName = dataReader["CountryName"].ToString();
                    c.CountryID = Convert.ToInt32(dataReader["CountryID"]);

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

        // This method insert a new Country
        public int InsertCountry(Country country)
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
            cmd = CreateCommandWithStoredProcedureInsert("spInsertCountry", con, country);             // create the command

            try
            {
                int lastId = Convert.ToInt32(cmd.ExecuteScalar()); // Executescalar requires the command to have a transaction
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

        //This method insert a new Category and post id to many-to-many table 
        public int InsertCategoryToPost(int postID, int categoryID)
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
            cmd = CreateCommandWithStoredProcedureInsert("spInsertPostAndCategory", con, postID, categoryID);             // create the command

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

        // This method insert a Platform
        public int InsertPlatform(Platform platform)
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

            cmd = CreateCommandWithStoredProcedureInsert("spInsertPlatform", con, platform);             // create the command

            try
            {
                int lastId = Convert.ToInt32(cmd.ExecuteScalar());//Executescalar requires the command to have a transaction
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




        // Create the SqlCommand using a stored procedure for READ

        // Read all post table data to main screen without keywords & IHRA category
        private SqlCommand CreateCommandWithStoredProcedureRead(string spName, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }


        // Read post's details & keywords & IHRA category data by its ID
        private SqlCommand CreateCommandWithStoredProcedureRead(string spName, SqlConnection con, int postID)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@post_id", postID);

            return cmd;
        }



        // Create the SqlCommand using a stored procedure for INSERT

        // Create the SqlCommand using a stored procedure for Insert a Post
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Post post)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@UserID", post.UserID);
            cmd.Parameters.AddWithValue("@PlatformID", post.PlatformID);
            cmd.Parameters.AddWithValue("@CountryID", post.CountryID);
            cmd.Parameters.AddWithValue("@LanguageID", post.LanguageID);
            cmd.Parameters.AddWithValue("@UrlLink", post.UrlLink);
            cmd.Parameters.AddWithValue("@Description", post.Description);
            cmd.Parameters.AddWithValue("@Threat", post.Threat);
            cmd.Parameters.AddWithValue("@AmountOfLikes", post.AmountOfLikes);
            cmd.Parameters.AddWithValue("@AmountOfShares", post.AmountOfShares);
            cmd.Parameters.AddWithValue("@AmountOfComments", post.AmountOfComments);
            cmd.Parameters.AddWithValue("@Screenshot", post.Screenshot);
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a Catogry to post
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, int postID, int CategoryID)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@post_id", postID);
            cmd.Parameters.AddWithValue("@category_id", CategoryID);

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a KeyWord/Hashtag to post
        private SqlCommand CreateCommandWithStoredProcedureInsertK(String spName, SqlConnection con, int postID, int keyWordsAndHashtages)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@post_id", postID);
            cmd.Parameters.AddWithValue("@KeyWordsAndHashtages_id", keyWordsAndHashtages);

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a Platform
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Platform platform)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@PlatformName", platform.PlatformName);
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a keyWordsAndHashtages
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, KeyWordsAndHashtages keyWordsAndHashtages)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@KeyWordsAndHashtages", keyWordsAndHashtages.KH);
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a Country
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Country country)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }

        // Create the SqlCommand using a stored procedure for Insert a language
        private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Language language)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@LanguageName", language.LanguageName);
            cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;

            return cmd;
        }



        // Create the SqlCommand using a stored procedure for UPDATE

        // Create the SqlCommand using a stored procedure for update a post
        private SqlCommand CreateCommandWithStoredProcedureUpdatePostStatus(String spName, SqlConnection con, UpdatePostStatus postStatusUpdate)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@PostID", postStatusUpdate.PostID);
            cmd.Parameters.AddWithValue("@PostStatus", postStatusUpdate.PostStatus);
            cmd.Parameters.AddWithValue("@PostStatusManager", postStatusUpdate.PostStatusManager);
            cmd.Parameters.AddWithValue("@RemovalStatus", postStatusUpdate.RemovalStatus);
            cmd.Parameters.AddWithValue("@RemovalStatusManager", postStatusUpdate.RemovalStatusManager);

            return cmd;
        }





    }
}
