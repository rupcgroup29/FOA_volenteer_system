namespace FOA_Server.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string TeamLeader { get; set; }
        private static List<Team> teamsList = new List<Team>();

        public Team() { }
        public Team(int teamID, string teamName, string description, string teamLeader)
        {
            TeamID = teamID;
            TeamName = teamName;
            Description = description;
            TeamLeader = teamLeader;
        }

        // read all teams
        public List<Team> ReadAllTeams()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadTeams();
        }



    }
}
