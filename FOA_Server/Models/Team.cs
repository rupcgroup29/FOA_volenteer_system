using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public int TeamLeader { get; set; }

        private static List<Team> teamsList = new List<Team>();

        public Team() { }
        public Team(int teamID, string teamName, string description, int teamLeader)
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


        // insert new team
        public Team InsertNewTeam()
        {
            try
            {
                teamsList = ReadAllTeams();
                foreach (Team team in teamsList)
                {
                    if (team.TeamName == this.TeamName)
                    {
                        throw new Exception(" כבר קיים צוות בשם הזה, אנא בחר שם אחר ");
                    }
                }

                DBteams dbs = new DBteams();
                int good = dbs.InsertTeam(this);
                if (good > 0) { return this; }
                else { return null; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" ההכנסה כשלה, " + exp.Message);
            }
        }





    }
}
