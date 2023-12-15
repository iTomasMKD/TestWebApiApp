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
    public class CreateUserCommandHandlerTests
    {
        private CreateUserHandler _handler;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IValidator<CreateUserCommand>> _validatorMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validatorMock = new Mock<IValidator<CreateUserCommand>>();
            _mapperMock = new Mock<IMapper>();

            _handler = new CreateUserHandler(_userRepositoryMock.Object, _validatorMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldCreateUser()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand
            {
                Username = "testUser",
                Password = "password123"
            };

            _validatorMock.Setup(validator => validator.ValidateAsync(createUserCommand, CancellationToken.None))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mapperMock.Setup(mapper => mapper.Map<User>(createUserCommand))
                .Returns(new User { /* Populate User properties as needed */ });

            // Act
            await _handler.Handle(createUserCommand, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand
            {
                Username = "testUser",
                Password = "invalid" // Invalid password for testing validation failure
            };

            var validationErrors = new FluentValidation.Results.ValidationResult(new[] 
                { new FluentValidation.Results.ValidationFailure("Password", "Invalid password") });

            _validatorMock.Setup(validator => validator.ValidateAsync(createUserCommand, CancellationToken.None))
                .ReturnsAsync(validationErrors);

            // Act and Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(createUserCommand, CancellationToken.None));
        }
    }
}
