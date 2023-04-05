namespace FOA_Server.Models
{
    public class PostChangeStatus
    {
        public int PostID { get; set; }
        public int PostStatus { get; set; }
        public int RemovalStatus { get; set; }
        public int PostStatusManager { get; set; }
        public int RemovalStatusManager { get; set; }

        public PostChangeStatus() { }
        public PostChangeStatus(int postID, int postStatus, int removalStatus, int postStatusManager, int removalStatusManager)
        {
            PostID = postID;
            PostStatus = postStatus;
            RemovalStatus = removalStatus;
            PostStatusManager = postStatusManager;
            RemovalStatusManager = removalStatusManager;
        }




    }
}
