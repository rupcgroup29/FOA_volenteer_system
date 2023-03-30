using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Language
    {
        public string LanguageName { get; set; }
        public int LanguageID { get; set; }

        private static List<Language> LanguagesList = new List<Language>();  // ענת: יצרתי רשימת שפות

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

        //ענת: הוספתי את הפונקציה שתוסיף שפה חדשה במידה ומישהו בחר "אחר" בשדה
        //Insert new Volunteer Program
        public Language InsertLanguage()
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
                        throw new Exception(" Volunteer Program under that name is allready exists ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertLanguage(this);
                if (good > 0) { return this; }
                else { return null; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // ענת: הוספתי כדי לוודא שהשפה עוד לא קיימת ברשימה
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

        // ענת: הוספתי כדי לקבל את שם השפה לפי שם 
        // returns LanguageID by Language name
        public int getLanguageByName(string name)
        {
            LanguagesList = ReadAllLanguages();

            foreach (Language item in LanguagesList)
            {
                if (item.LanguageName == name)
                {
                    return item.LanguageID;
                }
            }
            return -1;
        }
    }
}
