using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Platform
    {
        public int PlatformID { get; set; }
        public string PlatformName { get; set; }
        private static List<Platform> platformList = new List<Platform>();

        public Platform() { }
        public Platform(int platformID, string platformName) {
            PlatformID = platformID;
            PlatformName = platformName;
        }


        // read all Platforms
        public List<Platform> ReadAllPlatforms()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPlatforms();
        }

        // insert a platform
        public Platform InsertPlatform()
        {
            platformList = ReadAllPlatforms();
            try
            {
                DBposts dbs = new DBposts();
                int good = dbs.InsertPlatform(this);
                if (good > 0) { return this; }
                else { return null; }
            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }


    }
}
