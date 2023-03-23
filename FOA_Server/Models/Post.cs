using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Post    {

        // its fields
        public int PostID { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
        public string KeyWordsAndHashtages { get; set; }
        public string Threat { get; set; }
        public string Screenshot { get; set; }
        public DateTime Date { get; set; }
        public int AmoutOfLikes { get; set; }
        public int AmoutOfShares { get; set; }
        public int AmoutOfComments { get; set; }
        public int PostStatus { get; set; }
        public int RemovalStatus { get; set; }
       
        // FK fields
        public int UserID { get; set; }
        public int PlatformID { get; set; }
        public int CategoryID { get; set; }
        public int PostStatusManager { get; set; }
        public int RemovalStatusManager { get; set; }
        public int Country { get; set; }
        public int Language { get; set; }

        private static List<Post> postsList = new List<Post>();

        public Post() { }
        public Post(int postID, string urlLink, string description, string keyWordsAndHashtages, string threat, string screenshot, DateTime date, int amoutOfLikes, int amoutOfShares, int amoutOfComments, int postStatus, int removalStatus, int userID, int platformID, int categoryID, int postStatusManager, int removalStatusManager, int country, int language)
        {
            PostID = postID;
            UrlLink = urlLink;
            Description = description;
            KeyWordsAndHashtages = keyWordsAndHashtages;
            Threat = threat;
            Screenshot = screenshot;
            Date = date;
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
            Country = country;
            Language = language;
        }


        // read all Posts
        public List<Post> ReadAllPosts()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPosts();
        }

        //Insert new post
        public Post InsertPost()
        {
            postsList = ReadAllPosts();
            try
            {
                if (postsList.Count != 0)
                {
                    // check new user email uniqueness
                    bool uniqueUrl = UniqueUrl(this.UrlLink);
                    if (!uniqueUrl)
                    {
                        throw new Exception(" post under that URL link is allready exists in the system ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertPost(this);
                if (good > 0) { return this; }
                else { return null; }
            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // valid unique URL link for the new post insering
        public bool UniqueUrl(string url)
        {
            bool unique = true;
            foreach (Post p in postsList)
            {
                if (p.UrlLink == url)
                {
                    unique = false; break;
                }
            }
            return unique;
        }






    }
}
