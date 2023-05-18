using FOA_Server.Models.DAL;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Numerics;

namespace FOA_Server.Models
{
    public class BI_chart
    {
        private static List<Object> listObject = new List<Object>();


        ///// BAR-CHART for REMOVED POSTS vs IHRA CATEGORY

        // read all IHRAs
        public static List<IHRA> ReadAllIHRAs()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadIHRAs();
        }

        // read all Posts with IHRA & key words and hashtages
        public static List<ReadPost> ReadPosts()
        {
            ReadPost post = new ReadPost();
            return post.ReadPostWithHIRAandKeyworks();
        }

        // יצירת רשימה של אובייקטים כמספר הקטגוריות, אשר מכילות את שם הקטגוריה ואיפוס של סטטוס הפוסט ברשת
        public static List<dynamic> ResetListObject()
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
            List<ReadPost> postsList = ReadPosts();
            List<dynamic> listObject = ResetListObject();

            foreach (ReadPost post in postsList)
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
                            } else item.RemovedPosts = item.RemovedPosts + 1;
                        }
                    }
                }
            }
            return listObject;
        }



    }
}
