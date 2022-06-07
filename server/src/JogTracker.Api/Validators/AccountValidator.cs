using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Account;
using JogTracker.Models.Requests.Users;
using JogTracker.Models.Validation;
using JogTracker.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Api.Validators
{
    public interface IAccountValidator
    {
        Task<ValidationResult> ValidateOnRegister(RegisterCommand payload);
        Task<ValidationResult> ValidateOnLogin(LoginCommand payload);
        ValidationResult ValidateName(UserNamePayload payload);
    }

    public class AccountValidator : IAccountValidator
    {
        private readonly IUsersRepository _usersRepository;

        public AccountValidator(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ValidationResult> ValidateOnRegister(RegisterCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            validationErrors.AddRange(await ValidateUsername(payload));
            validationErrors.AddRange(ValidateName(payload).ValidationErrors);
            validationErrors.AddRange(ValidatePasswords(payload));

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        public async Task<ValidationResult> ValidateOnLogin(LoginCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(payload.UserName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.UserName).ToLowerCamelCase(),
                    Message = "Enter username"
                });

            if (string.IsNullOrEmpty(payload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Password).ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (!await _usersRepository.IsPasswordValid(payload.UserName, payload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Password).ToLowerCamelCase(),
                    Message = "Invalid username and (or) password"
                });

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        private async Task<IEnumerable<ValidationError>> ValidateUsername(RegisterCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(payload.UserName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.UserName).ToLowerCamelCase(),
                    Message = "Enter username"
                });

            if (await _usersRepository.IsExistByUserName(payload.UserName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.UserName).ToLowerCamelCase(),
                    Message = "This name is already in use"
                });

            return validationErrors;
        }

        public ValidationResult ValidateName(UserNamePayload payload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(payload.FirstName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.FirstName).ToLowerCamelCase(),
                    Message = "Enter name"
                });

            if (string.IsNullOrEmpty(payload.LastName))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.LastName).ToLowerCamelCase(),
                    Message = "Enter last name"
                });

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        private IEnumerable<ValidationError> ValidatePasswords(RegisterCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(payload.Password))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Password).ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (payload.Password.Length < 4)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Password).ToLowerCamelCase(),
                    Message = "The minimum length is 4 characters"
                });

            if (string.IsNullOrEmpty(payload.ConfirmPassword))
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.ConfirmPassword).ToLowerCamelCase(),
                    Message = "Enter password"
                });

            if (payload.Password != payload.ConfirmPassword)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.ConfirmPassword).ToLowerCamelCase(),
                    Message = "Passwords don't match"
                });

            return validationErrors;
        }
    }
}
