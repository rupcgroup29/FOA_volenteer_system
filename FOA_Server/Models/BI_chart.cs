using FOA_Server.Models.DAL;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Numerics;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace FOA_Server.Models
{
    public class BI_chart
    {
        private static List<Object> listObject = new List<Object>();
        private static List<ReadPost> listPosts = new List<ReadPost>();


        // read all Posts with IHRA & key words and hashtages
        public static List<ReadPost> ReadPosts()
        {
            ReadPost post = new ReadPost();
            return post.ReadPostWithHIRAandKeyworks();
        }

        ///// BAR-CHART for REMOVED POSTS vs IHRA CATEGORY

        // read all IHRAs
        public static List<IHRA> ReadAllIHRAs()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadIHRAs();
        }

        // יצירת רשימה של אובייקטים כמספר הקטגוריות, אשר מכילות את שם הקטגוריה ואיפוס סטטוס הפוסט ברשת
        public static List<dynamic> ResetIhraListObject()
        {
            List<IHRA> ihraList = ReadAllIHRAs();
            List<dynamic> listObject = new List<dynamic>();

            foreach (IHRA ihra in ihraList)
            {
                dynamic obj = new System.Dynamic.ExpandoObject();
                obj.Category = ihra.CategoryName;
                obj.RemovedPosts = 0;
                obj.NotRemovedPosts = 0;
                listObject.Add(obj);
            }

            return listObject;
        }

        // עדכון האובייקט הנ"ל במספר הפוסטרים שהוסרו/לא הוסרו מהרשתות לפי מספר הקטגוריה
        public static List<Object> ReadPostStatusVsIHRAcaterory()
        {
            listPosts = ReadPosts();
            List<dynamic> listObject = ResetIhraListObject();

            foreach (ReadPost post in listPosts)
            {
                for (int i = 0; i < post.CategoryName.Length; i++)
                {
                    foreach (dynamic item in listObject)
                    {
                        if (post.CategoryName[i] == item.Category)
                        {
                            if (post.RemovalStatus == 0)
                            {
                                item.NotRemovedPosts = item.NotRemovedPosts + 1;
                            }
                            else item.RemovedPosts = item.RemovedPosts + 1;
                        }
                    }
                }
            }
            return listObject;
        }



        ///// TOP 5 HASHTAGS & KEYWORDS

        public static List<string> ReadTop5KeyWordsAndHashtages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadTop5KeyWordsAndHashtages();
        }




        ///// LINE-CHART for POSTS UPLOADED BY MONTH

        public static List<Object> ReadPostsUploadedByMonth()
        {
            listPosts = ReadPosts();
            int[] numOfPostsPerMonth = new int[12];

            foreach (ReadPost post in listPosts)
            {
                int month = post.InsertDate.Month;
                numOfPostsPerMonth[month - 1]++;
            }
            numOfPostsPerMonth.ToList();

            List<dynamic> listObject = new List<dynamic>();
            int monthList = 1;
            DateTimeFormatInfo dtfi = CultureInfo.CurrentCulture.DateTimeFormat;

            foreach (int item in numOfPostsPerMonth)
            {
                dynamic obj = new System.Dynamic.ExpandoObject();
                obj.Month = dtfi.GetMonthName(monthList++);
                obj.PostsCounter = item;
                listObject.Add(obj);
            }

            return listObject;
        }



        ///// PIE-CHART for POSTS REMOVED FROM SOCIAL NETWORK 

        public static List<Object> ReadPercentagePostsRemoved()
        {
            listPosts = ReadPosts();
            int removed = 0;
            int stayed = 0;

            foreach (ReadPost post in listPosts)
            {
                if (post.RemovalStatus == 1)
                    removed++;      // post removed from social network
                else stayed++;      // post NOT removed from social network
            }

            List<Object> list = new List<object>();
            Object obj1 = new
            {
                Removed = true,
                Count = removed
            };
            Object obj2 = new
            {
                Removed = false,
                Count = stayed
            };

            list.Add(obj1);
            list.Add(obj2);

            return list;
        }



        ///// NUMBER OF POSTS UPLOADED IN THE LAST 7 DAYS 

        public static int ReadPostsCountLast7Days()
        {
            listPosts = ReadPosts();
            DateTime sevenDaysAgo = DateTime.Today.AddDays(-7);
            int count = listPosts.Count(p => p.InsertDate >= sevenDaysAgo && p.InsertDate <= DateTime.Now);

            return count;
        }



        ///// BAR-CHART for PLATFORMS (most common) 

        // read all Platforms
        public static List<Platform> ReadAllPlatforms()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPlatforms();
        }

        // יצירת רשימה של אובייקטים כמספר הפלטפומות, אשר מכילות את שם הפלטפורמה ואיפוס כמות הפוסטים באותה פלטפורמה
        public static List<dynamic> ResetPostsPerPlatfomListObject()
        {
            List<Platform> platformList = ReadAllPlatforms();
            List<dynamic> listObject = new List<dynamic>();

            foreach (Platform p in platformList)
            {
                dynamic obj = new System.Dynamic.ExpandoObject();
                obj.PlatformName = p.PlatformName;
                obj.Count = 0;
                listObject.Add(obj);
            }

            return listObject;
        }

        // עדכון האובייקט הנ"ל במספר הפוסטרים שהוסרו/לא הוסרו מהרשתות לפי מספר הקטגוריה
        public static List<Object> ReadPostsPerPlatfom()
        {
            listPosts = ReadPosts();
            List<dynamic> listObject = ResetPostsPerPlatfomListObject();

            foreach (ReadPost post in listPosts)
            {
                foreach (dynamic item in listObject)
                {
                    if (post.PlatformName == item.PlatformName)
                    {
                        item.Count = item.Count + 1;
                    }
                }
            }

            return listObject;
        }



    }
}
