﻿using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class VolunteerProgram
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }

        private static List<VolunteerProgram> VpList = new List<VolunteerProgram>();
        public VolunteerProgram() { }
        public VolunteerProgram(int programID, string programName)
        {
            ProgramID = programID;
            ProgramName = programName;
        }


        // read all Volunteer Programs
        public List<VolunteerProgram> ReadAllVolunteerPrograms()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadVolunteerPrograms();
        }

        //Insert new Volunteer Program
        public VolunteerProgram InsertVolunteerProgram()
        {
            VpList = ReadAllVolunteerPrograms();
            try
            {
                if (VpList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.ProgramName, VpList);
                    if (uniqueName==false)
                    {
                        throw new Exception(" Volunteer Program under that name is allready exists ");
                    }
                }

                DBusers dbs = new DBusers();
                int good = dbs.InsertVolunteerProgram(this);
                if (good > 0) { return this; }
                else { return null; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // vaild there is not the same already in the list
        public bool UniqueName(string name , List<VolunteerProgram> VpList)
        {
            bool unique = true;
<<<<<<< Updated upstream
            foreach (VolunteerProgram item in VpList)
            {
                if (item.ProgramName == name)
                    unique = false;
=======
            List<VolunteerProgram> tempList = ReadAllVolunteerPrograms();

            foreach (var item in tempList)
            {
                if (name == item.ProgramName)
                {
                    unique = false; break;
                }
>>>>>>> Stashed changes
            }

            return unique;
        }

    }
}
