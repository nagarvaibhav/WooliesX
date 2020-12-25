using System;
using WooliesX.DTO;
using WooliesX.Utility;

namespace WooliesX.Services
{
    public class UserService : IUserService
    {
        public UserResponse GetUserAndToken()
        {
            return new UserResponse
            {
                Token = Guid.NewGuid().ToString(),
                Name = Constants.User
            };
        }
    }
}
