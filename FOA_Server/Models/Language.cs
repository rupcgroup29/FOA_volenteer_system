using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Language
    {
        public string LanguageName { get; set; }
        public int LanguageID { get; set; }
        public Language() { }
        public Language(string lang, int langID)
        {
            LanguageName = lang;
            LanguageID = langID;
        }

        // read all Languages
        public List<Language> ReadAllLanguages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadLanguages();
        }



    }
}
