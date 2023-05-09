using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Recommendation
    {
        public string Language { get; set; }
        public string Platform { get; set; }
        public string KeyWordsAndHashtages { get; set; }

        // const parameters for the exposure 3 parameters
        private static string langExp = "";
        private static string platformExp = "";
        private static string kwAndHashtagExp = "";

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
            var result = dbs.ReadExposureKeyWordsAndHashtags();

            if (result != null)  // if zero posts has been uploaded to the system in the last week 
            {
                kwAndHashtagExp = result;
            }

            return kwAndHashtagExp;
        }

        // Exposure Platform
        public static string ReadExposurePlatform()
        {
            DBposts dbs = new DBposts();
            var result = dbs.ReadExposurePlatform();

            if (result != null)  // if zero posts has been uploaded to the system in the last week 
            {
                platformExp = result;
            }

            return platformExp;
        }

        // Exposure Language
        public static string ReadExposureLanguage()
        {
            DBposts dbs = new DBposts();
            var result = dbs.ReadExposureLanguage();

            if (result != null)  // if zero posts has been uploaded to the system in the last week 
            {
                langExp = result;
            }

            return langExp;
        }




    }
}
