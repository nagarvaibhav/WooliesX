using WooliesX.DTO;

namespace WooliesX.Services
{
    public interface IUserService
    {
        UserResponse GetUserAndToken();
    }
}
