using System;
using WooliesX.DTO;

namespace WooliesX.Services
{
    public class UserService : IUserService
    {
        public UserResponse GetUserAndToken()
        {
            return new UserResponse
            {
                Token = Guid.NewGuid().ToString(),
                User = "Test"
            };
        }
    }
}
