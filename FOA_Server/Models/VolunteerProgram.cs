﻿namespace FOA_Server.Models
{
    public class VolunteerProgram
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        private static List<VolunteerProgram> volunteerProgramsList = new List<VolunteerProgram>();
        public VolunteerProgram() { }
        public VolunteerProgram(int programID, string programName)
        {
            ProgramID = programID;
            ProgramName = programName;
        }

        // read all Volunteer Programs
        public List<VolunteerProgram> ReadAllVolunteerPrograms()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVolunteerPrograms();
        }

    }
}
