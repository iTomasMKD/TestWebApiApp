using AutoMapper;
using FluentValidation;
using Moq;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Handlers;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Repository;

namespace UnitTestProject
{
    [TestFixture]
    public class UpdateUserHandlerTests
    {
        private UpdateUserHandler _handler;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IValidator<UpdateUserCommand>> _validatorMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validatorMock = new Mock<IValidator<UpdateUserCommand>>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateUserHandler(_userRepositoryMock.Object, _validatorMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldUpdateUser()
        {
            // Arrange
            var updateUserCommand = new UpdateUserCommand
            {
                UserId = 1,
                // Other properties to update...
            };

            var existingUser = new User
            {
                // Populate existing user properties...
            };

            _validatorMock.Setup(validator => validator.ValidateAsync(updateUserCommand, CancellationToken.None))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(updateUserCommand.UserId, CancellationToken.None))
                .ReturnsAsync(existingUser);

            // Act
            await _handler.Handle(updateUserCommand, CancellationToken.None);

            // Assert
            _mapperMock.Verify(mapper => mapper.Map(updateUserCommand, existingUser), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(existingUser, CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var updateUserCommand = new UpdateUserCommand
            {
                UserId = 1,
                // Other properties to update...
            };

            var validationErrors = new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("Property", "Invalid value") });
            _validatorMock.Setup(validator => validator.ValidateAsync(updateUserCommand, CancellationToken.None))
                .ReturnsAsync(validationErrors);

            // Act and Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(updateUserCommand, CancellationToken.None));
        }

        [Test]
        public async Task Handle_UserNotFound_ShouldNotUpdateUser()
        {
            // Arrange
            var updateUserCommand = new UpdateUserCommand
            {
                UserId = 1,
                // Other properties to update...
            };

            _validatorMock.Setup(validator => validator.ValidateAsync(updateUserCommand, CancellationToken.None))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(updateUserCommand.UserId, CancellationToken.None))
                .ReturnsAsync((User)null); // Simulate user not found

            // Act
            await _handler.Handle(updateUserCommand, CancellationToken.None);

            // Assert
            _mapperMock.Verify(mapper =>
                               mapper.Map(It.IsAny<UpdateUserCommand>(),
                                          It.IsAny<User>()),
                                          Times.Never);

            _userRepositoryMock.Verify(repo =>
                                       repo.UpdateAsync(
                                           It.IsAny<User>(),
                                           CancellationToken.None),
                                           Times.Never);
        }
    }
}

