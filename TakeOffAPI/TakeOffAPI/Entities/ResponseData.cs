using System;
using System.Security.Policy;

namespace TakeOffAPI.Entities
{
    public enum ERROR_CODE
    {
        #region General Group
        SUCCESS = 0,
        FAIL,
        COMMAND_NOT_FOUND,
        TOKEN_INVALID_OR_EXPIRED,
        ACCESS_DENIED,
        INVALID_PARAM,
        API_NOT_SUPPORT,
        INCOMPATIBLE_VERSION,
        PLATFROM_NOT_SUPPORT,
        INTERNAL_ERROR,
        INVALID_DATA,
        SAVE_DATABASE_SUCCESS,
        INVALID_AUTHENTICATION,
        #endregion



        #region appuser database
        APPUSER_WRONG_PASSWORD_USERNAME = 100000,
        APPUSER_CANNOT_SAVE_TOKEN,
        APPUSER_CANNOT_GET_USER_FOR_REPORT,
        APPUSER_CANNOT_GET_OFFLINE_USER,
        APPUSER_CANNOT_GET_TOKEN_REQUEST,
        #endregion



        #region premier database
        PREMIERDB_DATA_IS_NULL = 200000,
        #endregion



        #region qlddata database
        QLD_DATA_IS_NULL = 300000,
        #endregion
    }
    public class ResponseData
    {
        public int Code { get; set; }
        public Object Data { get; set; } = null;



        public ResponseData()
        {
            Code = (int)ERROR_CODE.FAIL;
            Data = null;
        }
        public bool IsFailed() { return Code == int.MinValue; }
        public void MakeFailed() { Code = int.MinValue; Data = ""; }
    }

}

