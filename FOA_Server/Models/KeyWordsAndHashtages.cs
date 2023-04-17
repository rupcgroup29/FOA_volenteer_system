using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class KeyWordsAndHashtages
    {
        public string KH { get; set; }
        public int KH_ID { get; set; }

        private static List<KeyWordsAndHashtages> KeyWordsAndHashtagesList = new List<KeyWordsAndHashtages>();

        public KeyWordsAndHashtages() { }
        public KeyWordsAndHashtages(string KeyWordsAndHashtages, int KeyWordsAndHashtagesID)
        {
            KH = KeyWordsAndHashtages;
            KH_ID = KeyWordsAndHashtagesID;
        }

        // read all KeyWordsAndHashtages
        public List<KeyWordsAndHashtages> ReadKeyWordsAndHashtages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadKeyWordsAndHashtages();
        }


        //Insert new KeyWords or Hashtages
        public KeyWordsAndHashtages InsertKeyWordsAndHashtages()
        {
            KeyWordsAndHashtagesList = ReadKeyWordsAndHashtages();
            try
            {
                if (KeyWordsAndHashtagesList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.KH, KeyWordsAndHashtagesList);
                    if (uniqueName == false)
                    {
                        throw new Exception(" This key word or hashtage is allready exists ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertKeyWordsAndHashtages(this);
                if (good > 0) { return this; }
                else { return null; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }


        // vaild there is not the same KeyWordsAndHashtages already in the list
        public bool UniqueName(string name, List<KeyWordsAndHashtages> KeyWordsAndHashtagesList)
        {
            bool unique = true;

            foreach (KeyWordsAndHashtages item in KeyWordsAndHashtagesList)
            {
                if (item.KH == name)
                { unique = false; break; }
            }
            return unique;
        }




    }
}
