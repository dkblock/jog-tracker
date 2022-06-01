using JogTracker.Api.Common;
using JogTracker.Models.Account;
using JogTracker.Models.Validation;
using JogTracker.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Api.Validation
{
    public interface IAccountValidator
    {
        Task<ValidationResult> ValidateOnRegister(RegisterPayload registerPayload);
        Task<ValidationResult> ValidateOnLogin(LoginPayload loginPayload);
    }

    public class AccountValidator : IAccountValidator
    {
        private readonly IUserService _userService;

        public AccountValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ValidationResult> ValidateOnRegister(RegisterPayload registerPayload)
        {
            var validationErrors = new List<ValidationError>();

            validationErrors.AddRange(await ValidateUsername(registerPayload));
            validationErrors.AddRange(ValidateName(registerPayload));
            validationErrors.AddRange(ValidatePasswords(registerPayload));

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        public async Task<ValidationResult> ValidateOnLogin(LoginPayload loginPayload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(loginPayload.Username))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(loginPayload.Username).ToLowerCamelCase(),
                    Message = "Enter username"
                });

            if (string.IsNullOrEmpty(loginPayload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(loginPayload.Password).ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (!await _userService.IsPasswordValid(loginPayload.Username, loginPayload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(loginPayload.Password).ToLowerCamelCase(),
                    Message = "Invalid username and (or) password"
                });

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        private async Task<IEnumerable<ValidationError>> ValidateUsername(RegisterPayload registerPayload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(registerPayload.Username))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(registerPayload.Username).ToLowerCamelCase(),
                    Message = "Enter username"
                });

            if (await _userService.IsUserExistByUsername(registerPayload.Username))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(registerPayload.Username).ToLowerCamelCase(),
                    Message = "This name is already in use"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateName(RegisterPayload registerPayload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(registerPayload.FirstName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(registerPayload.FirstName).ToLowerCamelCase(),
                    Message = "Enter name"
                });

            if (string.IsNullOrEmpty(registerPayload.LastName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(registerPayload.LastName).ToLowerCamelCase(),
                    Message = "Enter last name"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidatePasswords(RegisterPayload registerPayload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(registerPayload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = registerPayload.Password.ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (registerPayload.Password.Length < 4)
                validationErrors.Add(new ValidationError
                {
                    Field = registerPayload.Password.ToLowerCamelCase(),
                    Message = "The minimum length is 4 characters"
                });

            if (string.IsNullOrEmpty(registerPayload.ConfirmPassword))
                validationErrors.Add(new ValidationError
                {
                    Field = registerPayload.ConfirmPassword.ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (registerPayload.Password != registerPayload.ConfirmPassword)
                validationErrors.Add(new ValidationError
                {
                    Field = registerPayload.ConfirmPassword.ToLowerCamelCase(),
                    Message = "Passwords don't match"
                });

            return validationErrors;
        }
    }
}
