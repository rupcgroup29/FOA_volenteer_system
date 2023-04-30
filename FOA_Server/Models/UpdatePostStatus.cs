namespace FOA_Server.Models
{
    public class UpdatePostStatus
    {
        public int PostID { get; set; }
        public int PostStatus { get; set; }
        public int PostStatusManager { get; set; }
        public int RemovalStatus { get; set; }
        public int RemovalStatusManager { get; set; }


        public UpdatePostStatus(int postID, int postStatus, int postStatusManager, int removalStatus, int removalStatusManager)
        {
            PostID = postID;
            PostStatus = postStatus;
            PostStatusManager = postStatusManager;
            RemovalStatus = removalStatus;
            RemovalStatusManager = removalStatusManager;
        }

    }
}
