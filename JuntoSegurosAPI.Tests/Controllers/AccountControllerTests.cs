using Xunit;
using Moq;
using JuntoSegurosAPI.Controllers;
using JuntoSegurosAPI.Models;
using JuntoSegurosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using NUnit.Framework;

namespace JuntoSegurosAPI.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<Controllers.IAccountService> _accountServiceMock;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _accountServiceMock = new Mock<Controllers.IAccountService>();
            _accountController = new AccountController(_accountServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerModel = new RegisterModel { Email = "test@example.com", Password = "Test@123", ConfirmPassword = "Test@123" };
            _accountServiceMock.Setup(service => service.RegisterAsync(registerModel))
                .ReturnsAsync((true, new string[] { }, "testToken"));

            // Act
            var result = await _accountController.RegisterAsync(registerModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginModel = new LoginModel { Email = "test@example.com", Password = "Test@123" };
            _accountServiceMock.Setup(service => service.LoginAsync(loginModel))
                .ReturnsAsync((true, new string[] { }, "testToken"));

            // Act
            var result = await _accountController.LoginAsync(loginModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsBadRequestResult_WhenLoginFails()
        {
            // Arrange
            var loginModel = new LoginModel { Email = "test@example.com", Password = "WrongPassword" };
            _accountServiceMock.Setup(service => service.LoginAsync(loginModel))
                .ReturnsAsync((false, new string[] { "Invalid login attempt." }, null));

            // Act
            var result = await _accountController.LoginAsync(loginModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsOkResult_WhenChangePasswordIsSuccessful()
        {
            // Arrange
            var changePasswordModel = new ChangePasswordModel { OldPassword = "OldPassword", NewPassword = "NewPassword", ConfirmNewPassword = "NewPassword" };
            _accountServiceMock.Setup(service => service.ChangePasswordAsync(It.IsAny<ClaimsPrincipal>(), changePasswordModel))
                .ReturnsAsync((true, new string[] { }));

            // Act
            var result = await _accountController.ChangePasswordAsync(changePasswordModel);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsBadRequestResult_WhenChangePasswordFails()
        {
            // Arrange
            var changePasswordModel = new ChangePasswordModel { OldPassword = "OldPassword", NewPassword = "NewPassword", ConfirmNewPassword = "WrongConfirmNewPassword" };
            _accountServiceMock.Setup(service => service.ChangePasswordAsync(It.IsAny<ClaimsPrincipal>(), changePasswordModel))
                .ReturnsAsync((false, new string[] { "New password and confirmation password do not match." }));

            // Act
            var result = await _accountController.ChangePasswordAsync(changePasswordModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
