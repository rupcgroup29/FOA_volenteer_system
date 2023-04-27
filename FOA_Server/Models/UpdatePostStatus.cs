namespace FOA_Server.Models
{
    public class UpdatePostStatus
    {
        public int PostID { get; set; }
        public int PostStatus { get; set; }
        public int PostStatusManager { get; set; }

        public UpdatePostStatus(int postID, int postStatus, int postStatusManager)
        {
            PostID = postID;
            PostStatus = postStatus;
            PostStatusManager = postStatusManager;
        }

    }
}
