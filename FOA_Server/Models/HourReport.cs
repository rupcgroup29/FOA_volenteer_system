using FOA_Server.Models.DAL;
using System.Data;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.Text.Json;


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
        public bool InsertHourReports(HourReport[] reports)
        {
            try
            {
                DBusers dbs = new DBusers();
                bool allInserted = true;
                foreach (HourReport report in reports)
                {
                    try
                    {
                        TimeSpan timeSpane = report.EndTime - report.StartTime;
                        if (timeSpane.TotalMilliseconds < 0)    //אם הזמן שהיוזר הזין הוא שלילי
                        {
                            throw new Exception(" שעת הכניסה שהזנת היא אחרי שעת הסיום, אנא נסה שוב ");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(" ההכנסה כשלה, " + ex.Message);
                    }

                    int good = dbs.InsertHourReport(report);
                    if (good <= 0)
                    {
                        allInserted = false;
                        break;    // Exit loop early if any report fails to insert
                    }
                }
                return allInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(" ההכנסה כשלה, " + ex.Message);
            }
        }


        // update shift status by team leader
        public bool UpdateShiftStatus(string jsonString)
        {
            try
            {
                JsonElement updateStatus = JsonDocument.Parse(jsonString).RootElement;
                DBusers dbs = new DBusers();
                bool allUpdated = true;
                foreach (JsonElement report in updateStatus.EnumerateArray())
                {
                    int reportId = report.GetProperty("reportId").GetInt32();
                    int status = report.GetProperty("status").GetInt32();
                    int userId = report.GetProperty("userId").GetInt32();

                    int rowsAffected = dbs.UpdateShiftStatus(reportId, status, userId);
                    if (rowsAffected == 0)
                    {
                        allUpdated = false;
                        break;    // Exit loop early if any report fails to insert
                    }
                }
                return allUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(" ההכנסה כשלה, " + ex.Message);
            }
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
