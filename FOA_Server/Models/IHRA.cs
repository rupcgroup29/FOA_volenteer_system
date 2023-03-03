namespace FOA_Server.Models
{
    public class IHRA
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        private static List<IHRA> IHRAlist = new List<IHRA>();

        public IHRA() { }
        public IHRA(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }

        // read all IHRA
        public List<IHRA> ReadAllIHRAs()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadIHRAs();
        }



    }
}
