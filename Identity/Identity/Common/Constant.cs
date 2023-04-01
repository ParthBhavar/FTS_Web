namespace Identity.Common
{
    public static class Constant
    {
        #region SP related to role
        public const string ADDAPPLICATIONROLE = "Usp_IdentityAddRole";
        public const string DELETEAPPLICATIONROLE = "Usp_IdentityDeleteRole";
        public const string UPDATEAPPLICATIONROLE = "Usp_IdentityUpdateRole";
        public const string GETAPPLICATIONROLEBYID = "Usp_IdentityGetRoleById";
        public const string GETAPPLICATIONROLEBYNAME = "Usp_IdentityGetRoleByName";
        #endregion

        #region SP related to user
        public const string ADDNEWUSER = "Usp_IdentityAddUser";

        public const string DELETEUSER = "Usp_IdentityDeleteUser";

        public const string GETUSERBYID = "Usp_IdentityGetUserById";

        public const string GETUSERBYUSERNAME = "Usp_IdentityGetUserByUserName";

        public const string GETUSERBYMAIL = "Usp_IdentityGetUserByEmail";

        public const string UPDATEUSER = "Usp_IdentityUpdateUser";

        public const string ASSIGNROLETOUSER = "Usp_IdentityAssignRoleToUser";

        public const string REMOVEROLEFROMUSER = "Usp_IdentityRemoveRoleFromUser";

        public const string GETUSERROLE = "Usp_IdentityGetUserRoles";       
        #endregion
    }
}
