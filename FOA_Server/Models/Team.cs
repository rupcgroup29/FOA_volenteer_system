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

        // read all teams details
        public static List<Object> ReadTeamsDetails()
        {
            DBteams dbs = new DBteams();
            return dbs.ReadTeamsDetails();
        }

        // read a specific team's details 
        public static Object ReadTeamDetailsByID(int teamID)
        {
            DBteams dbs = new DBteams();
            return dbs.ReadTeamDetailsByID(teamID);
        }


        // read all teams details
        public static List<Object> ReadTeamLeadersWithoutTeamToLead()
        {
            DBteams dbs = new DBteams();
            return dbs.ReadTeamLeadersWithoutTeamToLead();
        }

        // get direct user's maneger (return the team leader's userID)
        public static int GetUserManegerID(int teamID)
        {
            teamsList = ReadAllTeams();
            foreach (Team team in teamsList)
            {
                if (team.TeamID == teamID)
                {
                    return team.TeamLeader;
                }
            }
            return 0;
        }


        // read all team's users hour reports
        public static List<Object> GetUsersHourReportsInTeam(int teamID)
        {
            DBteams dbs = new DBteams();
            return dbs.ReadUsersHourReportsInTeam(teamID);
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
                int newTeamID = dbs.InsertTeam(this);    //returns the new team's ID
                if (newTeamID > 0)
                {
                    DBusers dBusers = new DBusers();
                    int updated = dBusers.UpdateTeamLeaderTeam(this.TeamLeader, newTeamID);
                    if (updated > 0)
                        return this;
                    else { throw new Exception(" לא הצליח לעדכן את מספר הצוות למנהל הצוות שנבחר "); }
                }
                else { throw new Exception(" כישלון בהוספת הצוות החדש "); }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" ההכנסה כשלה, " + exp.Message);
            }
        }


        // update team
        public bool UpdateTeam()
        {
            teamsList = ReadAllTeams();
            try
            {
                foreach (Team t in teamsList)
                {
                    if (t.TeamID == this.TeamID)
                    {
                        DBteams dbs = new DBteams();
                        int good = dbs.UpdateTeam(this);
                        if (good > 0) { return true; }
                        else { return false; }
                    }
                }
                throw new Exception(" no such team ");

            }
            catch (Exception exp)
            {
                throw new Exception(" didn't succeed in updating team's details " + exp.Message);
            }
        }

    }
}
