using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public int CountryID { get; set; }

        private static List<Country> CountriesList = new List<Country>();  


        public Country() { }   
        public Country(string country, int countryID)
        {
            CountryName = country;
            CountryID = countryID;
        }

        // read all countries
        public List<Country> ReadAllCountries()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadCountries();
        }

        //Insert new Volunteer Program
        public Country InsertCountry()
        {
            CountriesList = ReadAllCountries();
            try
            {
                if (CountriesList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.CountryName, CountriesList);
                    if (uniqueName == false)
                    {
                        throw new Exception(" Country under that name is allready exists ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertCountry(this);
                if (good > 0) { return this; }
                else { return null; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // vaild there is not the same country already in the list
        public bool UniqueName(string name, List<Country> CountriesList)
        {
            bool unique = true;

            foreach (Country item in CountriesList)
            {
                if (item.CountryName == name)
                { unique = false; break; }
            }
            return unique;
        }


        // ענת: הוספתי כדי לקבל את שם המדינה לפי שם 
        // returns CountryID by Country name
        public int getCountryByName(string name)
        {
            CountriesList = ReadAllCountries();

            foreach (Country item in CountriesList)
            {
                if (item.CountryName == name)
                {
                    return item.CountryID;
                }
            }
            return -1;
        }
    }
}
