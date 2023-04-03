using FOA_Server.Models.DAL;
using System.Xml.Linq;

namespace FOA_Server.Models
{
    public class Post
    {

        // its fields
        public int PostID { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
        public string[] KeyWordsAndHashtages { get; set; } 
        public int Threat { get; set; }
        // public string Screenshot { get; set; }
        public int AmoutOfLikes { get; set; }
        public int AmoutOfShares { get; set; }
        public int AmoutOfComments { get; set; }
        public int PostStatus { get; set; }
        public int RemovalStatus { get; set; }

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
        public Post(int postID, string urlLink, string description, string[] keyWordsAndHashtages, int threat, int amoutOfLikes, int amoutOfShares, int amoutOfComments, int postStatus, int removalStatus, int userID, int platformID, int[] categoryID, int postStatusManager, int removalStatusManager, int country, int language, string countryName, string languageName, string platformName)
        {
            PostID = postID;
            UrlLink = urlLink;
            Description = description;
            KeyWordsAndHashtages = keyWordsAndHashtages;
            Threat = threat;
            // Screenshot = screenshot;
            AmoutOfLikes = amoutOfLikes;
            AmoutOfShares = amoutOfShares;
            AmoutOfComments = amoutOfComments;
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
        }
        

        // read Posts without menager's status
        public List<Post> ReadPostsWithoutStatus()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPostsWithoutStatus();
        }


        //Insert new post
        public Post InsertPost()
        {
            //postsList = ReadPostsWithoutStatus();
            try
            {
                //if (postsList.Count != 0)
                //{
                //    // unique url ?
                //    bool uniqueUrl = UniqueUrl(this.UrlLink, postsList);
                //    if (!uniqueUrl)
                //    {
                //        throw new Exception(" post under that URL link is allready exists in the system ");
                //    }
                //}

                DBposts dbs = new DBposts();
                int postId = dbs.InsertPost(this);
                if (postId > 0) {
                   
                    for (int i = 0; i < this.CategoryID.Length; i++)//Loop that run on all the Category array
                    {
                        int response = dbs.InsertCategoryToPost(postId, this.CategoryID[i]); //Insert the categoryID and postID to many-to-many table in db
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
                            {
                                dbs.InsertKeyWordsAndHashtagesToPost(postId, key.KH_ID);
                            }
                            else
                            {
                                KeyWordsAndHashtages keyw = new KeyWordsAndHashtages(this.KeyWordsAndHashtages[i],0);
                                int id = dbs.InsertKeyWordsAndHashtages(keyw);
                                dbs.InsertKeyWordsAndHashtagesToPost(postId, id);

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


        //// list of approval posts
        //public List<Post> ApprovalPosts()
        //{
        //    postsList = ReadAllPosts();
        //    List<Post> approvalPosts = new List<Post>();

        //    foreach (Post post in postsList)
        //    {
        //        if (post.PostStatus == 1)
        //        {
        //            approvalPosts.Add(post);
        //        }
        //    }

        //    return approvalPosts;
        //}



    }
}
