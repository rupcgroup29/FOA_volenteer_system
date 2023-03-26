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
        public int InsertVolunteerProgram()
        {
            VpList = ReadAllVolunteerPrograms();
            try
            {
                if (VpList.Count != 0)
                {
                    // vaild there is not the same already in the list
                    bool uniqueName = UniqueName(this.ProgramName);
                    if (!uniqueName)
                    {
                        throw new Exception(" Volunteer Program under that name is allready exists ");
                    }
                }

                DBusers dbs = new DBusers();
                return dbs.InsertVolunteerProgram(this.ProgramName);

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting " + exp.Message);
            }
        }

        // vaild there is not the same already in the list
        public bool UniqueName(string name)
        {
            bool unique = true;
            List<string> tempList = new List<string>();

            foreach (var vp in tempList)
            {
                if (vp == name)
                {
                    unique = false; break;
                }
            }
            return unique;
        }

    }
}
