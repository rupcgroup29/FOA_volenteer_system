using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string TeamLeader { get; set; }

        public Team() { }
        public Team(int teamID, string teamName, string description, string teamLeader)
        {
            TeamID = teamID;
            TeamName = teamName;
            Description = description;
            TeamLeader = teamLeader;
        }




    }
}
