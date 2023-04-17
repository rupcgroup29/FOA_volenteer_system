using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Language
    {
        public string LanguageName { get; set; }
        public int LanguageID { get; set; }

        private static List<Language> LanguagesList = new List<Language>(); 

        public Language() { }
        public Language(string lang, int langID)
        {
            LanguageName = lang;
            LanguageID = langID;
        }

        // read all Languages
        public static List<Language> ReadAllLanguages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadLanguages();
        }

        //Insert new Language
        public int InsertLanguage()
        {
            LanguagesList = ReadAllLanguages();
            try
            {
                if (LanguagesList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.LanguageName, LanguagesList);
                    if (uniqueName == false)
                    {
                        throw new Exception(" Language under that name is allready exists ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertLanguage(this);    //gets the id of the new language inserted from the DB
                if (good > 0) { return good; }  
                else { return 0; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // vaild there is not the same country already in the list
        public bool UniqueName(string name, List<Language> LanguagesList)
        {
            bool unique = true;

            foreach (Language item in LanguagesList)
            {
                if (item.LanguageName == name)
                { unique = false; break; }
            }
            return unique;
        }



    }
}
