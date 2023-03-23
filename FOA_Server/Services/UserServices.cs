using FOA_Server.Models;
using FOA_Server.Models.DAL;
using System.Text.RegularExpressions;

namespace FOA_Server.Services
{
    public class UserServices
    {
        User user = new User();
        Permission permission = new Permission();
        HourReport hourReport = new HourReport();
        VolunteerProgram program = new VolunteerProgram();


        private static List<User> UsersList = new List<User>();
        private static List<Permission> permissionsList = new List<Permission>();
        private static List<HourReport> hourReportlist = new List<HourReport>();
        private static List<VolunteerProgram> volunteerProgramsList = new List<VolunteerProgram>();


        // read all users
        public List<User> ReadAllUsers()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadUsers();
        }


        // read all Permissions
        public List<Permission> ReadAllPermissions()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadPermissions();
        }


        // read all Hour Reports
        public List<HourReport> ReadAllHourReports()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadHourReports();
        }


        // read all Volunteer Programs
        public List<VolunteerProgram> ReadAllVolunteerPrograms()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadVolunteerPrograms();
        }



    }
}
