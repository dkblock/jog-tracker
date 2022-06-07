using JogTracker.Api.Validators;
using JogTracker.Models.Requests.Account;
using JogTracker.Models.Requests.Users;
using JogTracker.Models.Validation;
using JogTracker.Repository;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JogTracker.Api.UnitTests.Validators
{
    public class AccountValidatorTests : ValidatorTestBase
    {
        [Fact]
        public void ValidateName_AllFieldsAreValid_ReturnsNoErrors()
        {
            var validator = GetValidator();
            var payload = new UserNamePayload { FirstName = "FirstName", LastName = "LastName" };

            var result = validator.ValidateName(payload);

            Assert.True(result.IsValid);
            Assert.Empty(result.ValidationErrors);
        }

        [Fact]
        public void ValidateName_PayloadContainsInvalidFields_ReturnsErrors()
        {
            var validator = GetValidator();
            var payload = new UserNamePayload { FirstName = "", LastName = null };

            var result = validator.ValidateName(payload);

            Assert.False(result.IsValid);
            Assert.Collection(result.ValidationErrors,
                err => AssertError(err, "firstName", "Enter name"),
                err => AssertError(err, "lastName", "Enter last name"));
        }

        [Fact]
        public async Task ValidateOnRegister_AllFieldsAreValid_ReturnsNoErrors()
        {
            var validator = GetValidator();
            var payload = GetValidRegisterCommand();

            var result = await validator.ValidateOnRegister(payload);

            Assert.True(result.IsValid);
            Assert.Empty(result.ValidationErrors);
        }

        [Fact]
        public async Task ValidateOnRegister_InvalidUsername_ReturnsError()
        {
            var validator = GetValidator();
            var payload = GetValidRegisterCommand();
            payload.UserName = string.Empty;

            var result = await validator.ValidateOnRegister(payload);
            var error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "userName", "Enter username");

            validator = GetValidator(isUserNameUnique: false);
            payload = GetValidRegisterCommand();

            result = await validator.ValidateOnRegister(payload);
            error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "userName", "This name is already in use");
        }

        [Fact]
        public async Task ValidateOnRegister_InvalidPasswords_ReturnsErrors()
        {
            var validator = GetValidator();
            var payload = GetValidRegisterCommand();
            payload.Password = string.Empty;
            payload.ConfirmPassword = string.Empty;

            var result = await validator.ValidateOnRegister(payload);

            Assert.False(result.IsValid);
            Assert.Collection(result.ValidationErrors,
                err => AssertError(err, "password", "Enter password"),
                err => AssertError(err, "password", "The minimum length is 4 characters"),
                err => AssertError(err, "confirmPassword", "Enter password"));

            payload = GetValidRegisterCommand();
            payload.ConfirmPassword = "another_password";

            result = await validator.ValidateOnRegister(payload);
            var error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "confirmPassword", "Passwords don't match");
        }

        [Fact]
        public async Task ValidateOnLogin_AllFieldsAreValid_ReturnsNoErrors()
        {
            var validator = GetValidator();
            var payload = GetValidLoginCommand();

            var result = await validator.ValidateOnLogin(payload);

            Assert.True(result.IsValid);
            Assert.Empty(result.ValidationErrors);
        }

        [Fact]
        public async Task ValidateOnLogin_InvalidUsernameOrPassword_ReturnsErrors()
        {
            var validator = GetValidator();
            var payload = GetValidLoginCommand();
            payload.UserName = string.Empty;

            var result = await validator.ValidateOnLogin(payload);
            var error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "userName", "Enter username");

            validator = GetValidator(isPasswordValid: false);
            payload = GetValidLoginCommand();

            result = await validator.ValidateOnLogin(payload);
            error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "password", "Invalid username and (or) password");
        }

        private AccountValidator GetValidator(bool isUserNameUnique = true, bool isPasswordValid = true)
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();

            usersRepositoryMock
                .Setup(m => m.IsPasswordValid(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(isPasswordValid);

            usersRepositoryMock
                .Setup(m => m.IsExistByUserName(It.IsAny<string>()))
                .ReturnsAsync(!isUserNameUnique);

            return new AccountValidator(usersRepositoryMock.Object);
        }

        private RegisterCommand GetValidRegisterCommand() => new RegisterCommand
        {
            UserName = "username",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "password",
            ConfirmPassword = "password"
        };

        private LoginCommand GetValidLoginCommand() => new LoginCommand
        {
            UserName = "userName",
            Password = "password"
        };
    }
}
