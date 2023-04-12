using FOA_Server.Models.DAL;
using System;

namespace FOA_Server.Models
{
    public class ReadPost
    {
        public int PostID { get; set; }
        public string PlatformName { get; set; }
        public string CountryName { get; set; }
        public string LanguageName { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
        public int Threat { get; set; }
        public string Screenshot { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfShares { get; set; }
        public int PostStatus { get; set; }
        public int RemovalStatus { get; set; }
        public string UserName { get; set; }
        public string StatusManagerName { get; set; }
        public string RemovalManagerName { get; set; }
        public string[] CategoryName { get; set; }
        public string[] KeyWordsAndHashtages { get; set; }
        public DateTime InsertDate { get; set; }


        private static List<ReadPost> PostList = new List<ReadPost>();

        private static string[] PostIHRA;
        private static string[] PostKeywordsAndHashtags;

        
        public ReadPost() { }

        public ReadPost(int postID, string platformName, string countryName, string languageName, string urlLink, string description, int threat, string screenshot, int amountOfLikes, int amountOfComments, int amountOfShares, int postStatus, int removalStatus, string userName, string statusManagerName, string removalManagerName, string[] categoryName, string[] keyWordsAndHashtages, DateTime insertDate)
        {
            PostID = postID;
            PlatformName = platformName;
            CountryName = countryName;
            LanguageName = languageName;
            UrlLink = urlLink;
            Description = description;
            Threat = threat;
            Screenshot = screenshot;
            AmountOfLikes = amountOfLikes;
            AmountOfComments = amountOfComments;
            AmountOfShares = amountOfShares;
            PostStatus = postStatus;
            RemovalStatus = removalStatus;
            UserName = userName;
            StatusManagerName = statusManagerName;
            RemovalManagerName = removalManagerName;
            CategoryName = categoryName;
            KeyWordsAndHashtages = keyWordsAndHashtages;
            InsertDate = insertDate;
        }

        // read all Posts without IHRA & key words and hashtages
        public List<ReadPost> ReadAllPosts()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPosts();
        }

        // read all post's IHRA categories per postID
        public string[] ReadAllIHRAsPerPostID(int postID)
        {
            DBposts dbs = new DBposts();
            return dbs.ReadIHRAsPerPostID(postID);
        }

        // read all post's key words and hashtages by postID
        public string[] ReadKeyWordsAndHashtagesPerPostID(int postID)
        {
            DBposts dbs = new DBposts();
            return dbs.ReadKeyWordsAndHashtagesPerPostID(postID);
        }

        // read all Posts with IHRA & key words and hashtages
        public List<ReadPost> ReadPostWithHIRAandKeyworks()
        {
            List<ReadPost> allPostInfo = new List<ReadPost>();
            PostList = ReadAllPosts();

            foreach (ReadPost item in PostList)
            {
                // add the IHRA categories by postID
                PostIHRA = ReadAllIHRAsPerPostID(item.PostID);  //read for the IHRA categories from this post
                item.CategoryName = PostIHRA;           //insert the array into its filed here in the class

                // add the key words and hashtages by postID
                PostKeywordsAndHashtags = ReadKeyWordsAndHashtagesPerPostID(item.PostID);   //read KeyWordsAndHashtages from this post
                item.KeyWordsAndHashtages = PostKeywordsAndHashtags;     //insert the array into its filed here in the class

                allPostInfo.Add(item);
            }

            return allPostInfo;
        }


        // read Post by ID without its IHRA & key words and hashtages
        public ReadPost ReadPostById(int postID)
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPostByID(postID);
        }

        // read Post by ID with its IHRA & key words and hashtages
        public ReadPost ReadPostByIdWithHIRAandKeyworks(int postID)
        {
            ReadPost postInfo = new ReadPost();
            postInfo = ReadPostById(postID);

            // add the IHRA categories by postID
            PostIHRA = ReadAllIHRAsPerPostID(postID);  //read for the IHRA categories from this post
            postInfo.CategoryName = PostIHRA;           //insert the array into its filed here in the class

            // add the key words and hashtages by postID
            PostKeywordsAndHashtags = ReadKeyWordsAndHashtagesPerPostID(postID);   //read KeyWordsAndHashtages from this post
            postInfo.KeyWordsAndHashtages = PostKeywordsAndHashtags;     //insert the array into its filed here in the class

            return postInfo;
        }


        //Update Post Status details in the system & Removal Status details in social media
        public int UpdatePostStatus(UpdatePostStatus postStatusUpdate)
        {
            PostList = ReadAllPosts();

            try
            {
                foreach (ReadPost p in PostList)
                {
                    if (p.PostID == postStatusUpdate.PostID)
                    {
                        DBposts dbs = new DBposts();
                        return dbs.UpdatePostStatus(postStatusUpdate);
                    }
                }
                throw new Exception(" no such post ");

            }
            catch (Exception exp)
            {
                throw new Exception(" didn't succeed in updating this post, " + exp.Message);
            }
        }



   

    }
}
