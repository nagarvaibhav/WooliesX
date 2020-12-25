using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WooliesX.DTO;
using WooliesX.Services;

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
            Assert.AreEqual("Test", result.User);
        }
    }
}
