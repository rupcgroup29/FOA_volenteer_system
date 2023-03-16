using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Country
    {
        public string _Country { get; set; }


        public Country() { }   
        public Country(string country)
        {
            _Country = country;
        }




    }
}
