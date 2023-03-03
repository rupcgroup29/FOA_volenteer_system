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
            DBservices dbs = new DBservices();
            return dbs.ReadPlatforms();
        }

    }
}
