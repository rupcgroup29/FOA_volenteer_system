using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class VolunteerProgram
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public VolunteerProgram() { }
        public VolunteerProgram(int programID, string programName)
        {
            ProgramID = programID;
            ProgramName = programName;
        }

       

    }
}
