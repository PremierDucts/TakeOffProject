namespace TakeOffAPI.WebAPIClient.Commons
{
    public class QueryStringGlobal
    {
        #region [QLD Data]
        public static string VALID_USER_DEVICE => @"SELECT ip_address, uid, mac_address, user_name FROM tbl_controllists WHERE user_name = @user_name AND ip_address = @ip_address AND mac_address = @mac_address;";

        #endregion
    }
}
