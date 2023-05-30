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
        public double? ShiftTime { get; set; }


        public HourReport() { }
        public HourReport(int reportID, DateTime date, DateTime startTime, DateTime endTime, int status, int userID, double? shiftTime)
        {
            ReportID = reportID;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            UserID = userID;
            ShiftTime = shiftTime;
        }


        // read all user's hour reports
        public static List<HourReport> ReadUserHourReports(int userId)
        {
            DBusers dbs = new DBusers();
            return dbs.ReadUserHourReports(userId);
        }

        // read all users hour reports
        public static List<Object> ReadUsersHourReports()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadAllUsersHourReports();
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
                        if (report.StartTime > DateTime.Now)    //אם הזמן שהיוזר הזין הוא עתידי
                        {
                            throw new Exception(" הכנסת זמן עתידי ");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    try
                    {
                        TimeSpan timeSpane = report.EndTime - report.StartTime;
                        if (timeSpane.TotalMilliseconds < 0)    //אם הזמן שהיוזר הזין הוא שלילי
                        {
                            throw new Exception(" שעת הכניסה שהזנת היא אחרי שעת הסיום ");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    try
                    {
                        List<HourReport> usersList = ReadUserHourReports(report.UserID);
                        foreach (HourReport user in usersList)
                        {
                            if (((report.StartTime > user.StartTime) && (report.StartTime < user.EndTime)) ||
                                ((report.EndTime > user.StartTime) && (report.EndTime < user.EndTime)) ||
                                ((report.StartTime < user.StartTime) && (report.EndTime > user.EndTime)) ||
                                !((report.StartTime >= user.EndTime) || (report.EndTime <= user.StartTime)))
                            {
                                throw new Exception("כבר הכנסת דיווח שעות עם אותם הזמנים ");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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
        public bool UpdateShiftStatus(UpdateHourReport[] listOfHours)
        {
            try
            {
                DBusers dbs = new DBusers();
                bool allUpdated = true;
                foreach (UpdateHourReport report in listOfHours)
                {
                    int rowsAffected = dbs.UpdateShiftStatus(report.ReportID, report.Status, report.UserID);
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
        public int DeleteHourReports(int reportID)
        {
            if (this.Status == 0)
            {
                DBusers dbusers = new DBusers();
                return dbusers.DeleteHourReports(reportID);
            }
            else throw new Exception(" לא ניתן למחוק את דיווח השעות כי כבר הוזן לו סטטוס ");
        }


    }
}
