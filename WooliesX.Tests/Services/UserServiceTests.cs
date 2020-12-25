using NUnit.Framework;
using WooliesX.DTO;
using WooliesX.Services;
using WooliesX.Utility;

namespace WooliesX.Tests.Services
{
    public class UserServiceTests
    {

        [Test]
        public void  GetUserAndToken_Should_Return_UserResponse_Object()
        {
            var userService = new UserService();
            var result = userService.GetUserAndToken();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(UserResponse), result);
            Assert.AreEqual(Constants.User, result.Name);
        }
    }
}
