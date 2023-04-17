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
        public static List<Platform> ReadAllPlatforms()
        {
            DBposts dbs = new DBposts();
            return dbs.ReadPlatforms();
        }

        // insert a platform
        public int InsertPlatform()
        {
            platformList = ReadAllPlatforms();
            try
            {
                if (platformList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.PlatformName, platformList);
                    if (uniqueName == false)
                    {
                        throw new Exception(" Platform under that name is allready exists ");
                    }
                }

                DBposts dbs = new DBposts();
                int good = dbs.InsertPlatform(this);    //gets the id for the new platform inserted
                if (good > 0) { return good; }  
                else { return 0; }
            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // vaild there is not the same Platform already in the list
        public bool UniqueName(string name, List<Platform> platformList)
        {
            bool unique = true;

            foreach (Platform item in platformList)
            {
                if (item.PlatformName == name)
                { unique = false; break; }
            }
            return unique;
        }



    }
}
