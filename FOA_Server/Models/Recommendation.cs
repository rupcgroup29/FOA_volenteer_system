using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Recommendation
    {
        public string Language { get; set; }
        public string Platform { get; set; }
        public string KeyWordsAndHashtages { get; set; }

        public Recommendation(string language, string platform, string keyWordsAndHashtages)
        {
            Language = language;
            Platform = platform;
            KeyWordsAndHashtages = keyWordsAndHashtages;
        }


        // Exposure Parameters

        // Exposure Key Words and Hashtages
        public static string ReadExposureKeyWordsAndHashtages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadExposureKeyWordsAndHashtags();
        }

        // Exposure Platform
        public static string ReadExposurePlatform()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadExposurePlatform();
        }

        // Exposure Language
        public static string ReadExposureLanguage()
        {
            DBposts dbs = new DBposts(); 
            return dbs.ReadExposureLanguage();
        }




    }
}
