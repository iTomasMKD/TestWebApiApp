namespace UnitTestProject
{
    [TestFixture]
    public class UserCommandHandlersTests
    {
        [Test]
        public async Task CreateUserHandler_ValidCommand_ShouldCreateUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var validator = new CreateUserValidator();

            var createUserCommand = new CreateUserCommand
            {
                Username = "xyz_test",
                Password = "password123"
            };

            var handler = new CreateUserHandler(userRepositoryMock.Object, validator);

            // Act
            await handler.Handle(createUserCommand, CancellationToken.None);

            // Assert
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void CreateUserValidator_InvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var validator = new CreateUserValidator();
            var createUserCommand = new CreateUserCommand
            {
                // Missing required properties to trigger validation errors
            };

            // Act
            var result = validator.TestValidate(createUserCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }    
}
