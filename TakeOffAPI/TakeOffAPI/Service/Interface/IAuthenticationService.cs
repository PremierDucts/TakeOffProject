using TakeOffAPI.Entities;
using TakeOffAPI.Entities.Request;
using TakeOffAPI.WebAPIClient.Commons;

namespace TakeOffAPI.Service.Interface
{
    public interface IAuthenticationService
    {
        Task<ResponseData> Login(AuthRequest authRequest);
    }
}
