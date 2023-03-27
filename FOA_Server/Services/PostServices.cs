using FOA_Server.Models;
using FOA_Server.Models.DAL;

namespace FOA_Server.Services
{
    public class PostServices
    {
        Post post = new Post();
        Platform platform = new Platform();
        Language language = new Language();
        IHRA ihra = new IHRA();
        Country country = new Country();


        private static List<Post> postsList = new List<Post>();
        private static List<Platform> platformList = new List<Platform>();
        private static List<Language> _languages = new List<Language>();
        private static List<IHRA> IHRAlist = new List<IHRA>();
        private static List<Country> countryList = new List<Country>();


      /*  // read all Posts
        public List<Post> ReadAllPosts()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPosts();
        }

        // read all Platforms
        public List<Platform> ReadAllPlatforms()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPlatforms();
        }


        // read all Languages
        public List<Language> ReadAllLanguages()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadLanguages();
        }


        // read all IHRA
        public List<IHRA> ReadAllIHRAs()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadIHRAs();
        }

        // read all Countries
        public List<Country> ReadAllCountries()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadCountries();
        }

        */

    }
}
