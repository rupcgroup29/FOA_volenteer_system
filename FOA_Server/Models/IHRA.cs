﻿using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class IHRA
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public IHRA() { }
        public IHRA(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }




    }
}
