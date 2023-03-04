namespace FOA_Server.Models
{
    public class HourReport
    {
        public int ReportID { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StatTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Status { get; set; }

        private static List<HourReport> hourReportlist = new List<HourReport>();

        public HourReport() { }
        public HourReport(int reportID, DateTime date, TimeOnly statTime, TimeOnly endTime, int status) 
        {
            ReportID = reportID;
            Date = date;
            StatTime = statTime;
            EndTime = endTime;
            Status = status;
        }

        // read all Hour Reports
        public List<HourReport> ReadAllHourReports()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadHourReports();
        }

    }
}
