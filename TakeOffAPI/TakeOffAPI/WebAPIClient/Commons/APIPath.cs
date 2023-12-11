namespace TakeOffAPI.WebAPIClient.Commons
{
    public enum API_TYPE
    {
        #region [APP_USER]
        LOGIN = 0,
        #endregion

        #region [PREMIER_DUCTS]
        #endregion

        #region [QLD_DATA]
        VALID_USER_DEVICE,
        #endregion
    }

    public class APIPath
    {
        public static Dictionary<API_TYPE, String> HttpCommands = new Dictionary<API_TYPE, string>()
        {
            #region [APP_USER]
            {API_TYPE.LOGIN, "/user/login" },
            #endregion

            #region [PREMIER_DUCTS]
            #endregion

            #region [QLD_DATA]
            {API_TYPE.VALID_USER_DEVICE, "/auth/Authentication/valid-user-device" },
            #endregion
        };
    }
}
