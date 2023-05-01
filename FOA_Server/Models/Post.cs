using FOA_Server.Models.DAL;
using System;
using System.Xml.Linq;

namespace FOA_Server.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
        public string[] KeyWordsAndHashtages { get; set; }
        public int Threat { get; set; }
        public string Screenshot { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfShares { get; set; }
        public int AmountOfComments { get; set; }
        public int PostStatus { get; set; }
        public int RemovalStatus { get; set; }
        public DateTime InsertDate { get; set; }

        // FK fields
        public int UserID { get; set; }
        public int PlatformID { get; set; }
        public int[] CategoryID { get; set; }
        public int PostStatusManager { get; set; }
        public int RemovalStatusManager { get; set; }
        public int CountryID { get; set; }
        public int LanguageID { get; set; }

        // הוספת 3 שדות חדשים על מנת לקבל אותם במידה ומישהו מקליד "אחר" בשדה
        public string CountryName { get; set; }
        public string LanguageName { get; set; }
        public string PlatformName { get; set; }


        private static List<Post> postsList = new List<Post>();

        public Post() { }
        public Post(int postID, string urlLink, string description, string[] keyWordsAndHashtages, int threat, string screenshot, int amountOfLikes, int amountOfShares, int amountOfComments, int postStatus, int removalStatus, int userID, int platformID, int[] categoryID, int postStatusManager, int removalStatusManager, int country, int language, string countryName, string languageName, string platformName, DateTime insertDate)
        {
            PostID = postID;
            UrlLink = urlLink;
            Description = description;
            KeyWordsAndHashtages = keyWordsAndHashtages;
            Threat = threat;
            Screenshot = screenshot;
            AmountOfLikes = amountOfLikes;
            AmountOfShares = amountOfShares;
            AmountOfComments = amountOfComments;
            PostStatus = postStatus;
            RemovalStatus = removalStatus;
            UserID = userID;
            PlatformID = platformID;
            CategoryID = categoryID;
            PostStatusManager = postStatusManager;
            RemovalStatusManager = removalStatusManager;
            CountryID = country;
            LanguageID = language;
            CountryName = countryName;
            LanguageName = languageName;
            PlatformName = platformName;
            InsertDate = insertDate;
        }


        // read Posts without status by menager name
        public static List<Post> ReadPostsWitoutStatusByMenagerName()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPostsWitoutStatusByMenagerName();
        }


        //Insert new post
        public Post InsertPost()
        {
            postsList = ReadPostsWitoutStatusByMenagerName();
            try
            {
                if (postsList.Count != 0)
                {
                    // check if there's not the same url link in the data
                    bool uniqueUrl = UniqueUrl(this.UrlLink, postsList);
                    if (!uniqueUrl)
                    {
                        throw new Exception(" post under that URL link is allready exists in the system ");
                    }
                }

                DBposts dbs = new DBposts();
                int postId = dbs.InsertPost(this);

                if (postId > 0)
                {
                    for (int i = 0; i < this.CategoryID.Length; i++) //Loop that runs on all the Category array
                    {
                        //Insert the categoryID and postID to many-to-many table in db
                        int response = dbs.InsertCategoryToPost(postId, this.CategoryID[i]); 
                        if (response <= 0)
                        {
                            return null;
                        }
                    }

                    List<KeyWordsAndHashtages> KeyWordsAndHashtagesList = dbs.ReadKeyWordsAndHashtages();
                    if (KeyWordsAndHashtagesList != null)
                    {
                        for (int i = 0; i < this.KeyWordsAndHashtages.Length; i++)
                        {
                            KeyWordsAndHashtages key = KeyWordsAndHashtagesList.FirstOrDefault(KeyWord => KeyWord.KH == this.KeyWordsAndHashtages[i]);
                            if (key != null)
                            {   //insert existing KW/Hashtag to table with its relevent postID
                                dbs.InsertKeyWordsAndHashtagesToPost(postId, key.KH_ID);
                            }
                            else
                            {
                                KeyWordsAndHashtages keyw = new KeyWordsAndHashtages(this.KeyWordsAndHashtages[i], 0);
                                int id = dbs.InsertKeyWordsAndHashtages(keyw);  //insert KW/Hashtag to its table in DB
                                dbs.InsertKeyWordsAndHashtagesToPost(postId, id);   //insert KW/Hashtag to table with its relevent postID
                            }
                        }
                    }
                    return this;
                }
                else { return null; }
            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // valid unique URL link for the new post insering
        public bool UniqueUrl(string url, List<Post> list)
        {
            bool unique = true;
            foreach (Post p in list)
            {
                if (p.UrlLink == url)
                {
                    unique = false; break;
                }
            }
            return unique;
        }

        //How many posts are without maneger status
        public static int NumberOfPostdWithoutStatus()
        {
            postsList = ReadPostsWitoutStatusByMenagerName();
            int count = 0;

            foreach (Post post in postsList)
            {
                if (post.PostStatus == 0)
                {
                    count++;
                }
            }
            return count;
        }




    }
}
