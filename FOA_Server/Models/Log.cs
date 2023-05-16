using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Table_name { get; set; }
        public string Description { get; set; }

        private static List<Log> LogsList = new List<Log>();

        public Log() { }
        public Log(int id, DateTime timestamp, string action, string table_name, string description)
        {
            Id = id;
            Timestamp = timestamp;
            Action = action;
            Table_name = table_name;
            Description = description;
        }


        // read all IHRAs
        public static List<Log> ReadAllLogs()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadLogs();
        }
    }
}
