namespace FOA_Server.Models
{
    public class Country
    {
        public string _Country { get; set; }

        private static List<Country> countryList = new List<Country>();

        public Country() { }   
        public Country(string country)
        {
            _Country = country;
        }

        // read all Countries
        public List<Country> ReadAllCountries()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadCountries();
        }


    }
}
