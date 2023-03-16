using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Platform
    {
        public int PlatformID { get; set; }
        public string PlatformName { get; set; }


        public Platform() { }
        public Platform(int platformID, string platformName) {
            PlatformID = platformID;
            PlatformName = platformName;
        }

    

    }
}
