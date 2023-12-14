using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Handlers;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Repository;
using TestWebApiApp.Core.Validators;

namespace UnitTestProject
{
    public class UserUpdateCommandHandlerTest
    {
        [Test]
        public async Task UpdateUserHandler_ValidCommand_ShouldUpdateUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var validator = new UpdateUserValidator();

            var updateUserCommand = new UpdateUserCommand
            {
                UserId = 1,
                NewUsername = "updated_username"
            };

            var existingUser = new User
            {
                Id = 1,
                Username = "john_doe",
                Password = "password123"
            };

            userRepositoryMock.Setup(repo => repo.GetByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(existingUser);

            var handler = new UpdateUserHandler(userRepositoryMock.Object, validator);

            // Act
            await handler.Handle(updateUserCommand, CancellationToken.None);

            // Assert
            userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void UpdateUserValidator_InvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var validator = new UpdateUserValidator();
            var updateUserCommand = new UpdateUserCommand
            {
                // Missing required properties to trigger validation errors
            };

            // Act
            var result = validator.TestValidate(updateUserCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId);
            result.ShouldHaveValidationErrorFor(x => x.NewUsername);
        }
    }
}

