using FOA_Server.Models.DAL;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;

namespace FOA_Server.Models
{
    public class HourReport
    {
        public int ReportID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Status { get; set; }


        public HourReport() { }
        public HourReport(int reportID, DateTime date, DateTime startTime, DateTime endTime, int status, int userID)
        {
            ReportID = reportID;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            UserID = userID;
        }


        // read all Hour Reports
        public static List<HourReport> ReadAllHourReports()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadHourReports();
        }

        // read all user's hour reports
        public static List<HourReport> ReadUserHourReports(int userId)
        {
            DBusers dbs = new DBusers();
            return dbs.ReadUserHourReports(userId);
        }

        //Insert new Hour Report 
        public bool InsertHourReports()
        {
            try
            {
                TimeSpan timeSpane = this.EndTime - this.StartTime;
                if (timeSpane.TotalMilliseconds < 0)    //אם הזמן שהיוזר הזין הוא שלילי
                {
                    throw new Exception(" שעת הכניסה שהזנת היא אחרי שעת הסיום, אנא נסה שוב ");
                }

                DBusers dbs = new DBusers();
                int good = dbs.InsertHourReport(this);
                if (good > 0) { return true; }
                else { return false; }

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" ההכנסה כשלה, " + exp.Message);
            }
        }


        // update shift status by team leader
        public bool UpdateShiftStatus(int reportID, int status, int userId)
        {
            try
            {
                DBusers dbs = new DBusers();
                if (dbs.UpdateShiftStatus(reportID, status, userId) > 0)
                    return true;
                else { return false; }
            }
            catch (Exception exp) { return false; }
        }


        // delete shift with status 0
        public void DeleteHourReports(int reportID)
        {
            if (this.Status == 0)
            {
                DBusers dbusers = new DBusers();
                dbusers.DeleteHourReports(reportID);
            }
            else throw new Exception(" cannot delete an hour report becouse its status has already changed ");
        }


    }
}
