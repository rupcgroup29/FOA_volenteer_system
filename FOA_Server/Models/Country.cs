using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public int CountryID { get; set; }


        public Country() { }   
        public Country(string country, int countryID)
        {
            CountryName = country;
            CountryID = countryID;
        }




    }
}
