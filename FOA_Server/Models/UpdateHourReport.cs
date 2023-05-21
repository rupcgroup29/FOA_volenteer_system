namespace FOA_Server.Models
{
    public class UpdateHourReport
    {
        public int ReportID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }

        public UpdateHourReport()
        {
        }
        public UpdateHourReport(int reportID, int userID, int status)
        {
            ReportID = reportID;
            UserID = userID;
            Status = status;
        }
    }

}
