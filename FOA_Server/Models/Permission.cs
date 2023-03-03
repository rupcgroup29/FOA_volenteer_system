namespace FOA_Server.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }

        public Permission() { }
        public Permission(int permissionID, string permissionName)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
        }

        // read all Permissions
        public List<Permission> ReadAllPermissions()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadPermissions();
        }


    }
}
