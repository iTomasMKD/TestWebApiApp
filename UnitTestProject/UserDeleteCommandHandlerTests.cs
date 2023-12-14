using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Validators;

namespace UnitTestProject
{
    public class UserDeleteCommandHandlerTests
    {
        [Test]
        public void DeleteUserValidator_ValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var validator = new DeleteUserValidator();
            var deleteUserCommand = new DeleteUserCommand
            {
                UserId = 1
            };

            // Act
            var result = validator.TestValidate(deleteUserCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        }

        [Test]
        public void DeleteUserValidator_InvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var validator = new DeleteUserValidator();
            var deleteUserCommand = new DeleteUserCommand
            {
                // Invalid UserId (less than or equal to 0)
                UserId = 0
            };

            // Act
            var result = validator.TestValidate(deleteUserCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }
    }
}
