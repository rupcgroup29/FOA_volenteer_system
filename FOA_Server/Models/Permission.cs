using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Permission
    {
        // the permissions in the FOA system
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }

        public Permission() { }
        public Permission(int permissionID, string permissionName)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
        }


        // read all Permissions
        public static List<Permission> ReadAllPermissions()
        {
            DBusers dbs = new DBusers();
            return dbs.ReadPermissions();
        }



    }
}
