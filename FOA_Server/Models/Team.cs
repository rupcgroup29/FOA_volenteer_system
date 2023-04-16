using FOA_Server.Models.DAL;

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
        public static List<Team> ReadAllTeams()
        {
            DBteams dbs = new DBteams();
            return dbs.ReadTeams();
        }

        // read team members by team name
        public List<Team> ReadTeamByName(string teamName)
        {
            teamsList = ReadAllTeams();
            List<Team> teamByName = new List<Team>();

            foreach (Team team in teamsList)
            {
                if (team.TeamName == teamName)
                {
                    teamByName.Add(team);
                }
            }

            return teamByName;
        }

    }
}
