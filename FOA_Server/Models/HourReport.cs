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

        private static List<HourReport> hourReportlist = new List<HourReport>();

        public HourReport() { }
        public HourReport(int reportID, DateTime date, TimeOnly startTime, TimeOnly endTime, int status)
        {
            ReportID = reportID;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }

        /* // conver date to time
        private TimeOnly TimeFormat(DateTime date)
        {
            TimeOnly time = TimeOnly.FromDateTime(date);
            return time;
        } */

        // read all Hour Reports
        public List<HourReport> ReadAllHourReports()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadHourReports();
        }

    }
}
