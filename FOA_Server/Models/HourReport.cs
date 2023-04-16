using FOA_Server.Models.DAL;
using System.Runtime.Serialization;

namespace FOA_Server.Models
{
    public class HourReport
    {
        public int ReportID { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Status { get; set; }


        public HourReport() { }
        public HourReport(int reportID, DateTime date, TimeOnly startTime, TimeOnly endTime, int status)
        {
            ReportID = reportID;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }


        // read all Hour Reports
        public static List<HourReport> ReadAllHourReports()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadHourReports();
        }


        /* // conver date to time
        private TimeOnly TimeFormat(DateTime date)
        {
            TimeOnly time = TimeOnly.FromDateTime(date);
            return time;
        } */



    }
}
