namespace FOA_Server.Models
{
    public class Language
    {
        public string Lang { get; set; }
        private static List<Language> _languages = new List<Language>();
        public Language() { }
        public Language(string lang)
        {
            Lang = lang;
        }

        // read all Languagea
        public List<Language> ReadAllLanguages()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadLanguages();
        }


    }
}
