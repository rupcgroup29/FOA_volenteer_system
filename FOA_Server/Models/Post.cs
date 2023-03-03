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
        public int UserName { get; set; }
        public int UserID { get; set; }
        public int PlatformID { get; set; }
        public int CategoryID { get; set; }
        public int PostStatusManager { get; set; }
        public int RemovalStatusManager { get; set; }
        public int Country { get; set; }
        public int Language { get; set; }

        private static List<Post> postsList = new List<Post>();



    }
}
