using JogTracker.Common.Extensions;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogTracker.Api.Validators
{
    public interface IJogsValidator
    {
        ValidationResult Validate(CreateJogCommand payload);
    }

    public class JogsValidator : IJogsValidator
    {
        public ValidationResult Validate(CreateJogCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            validationErrors.AddRange(ValidateDistance(payload));
            validationErrors.AddRange(ValidateTime(payload));
            validationErrors.AddRange(ValidateDate(payload));

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        private IEnumerable<ValidationError> ValidateDistance(CreateJogCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (!payload.DistanceInMeters.HasValue && !payload.DistanceInKilometers.HasValue)
                validationErrors.AddRange(new List<ValidationError> {
                    new ValidationError
                    {
                        Field = nameof(payload.DistanceInMeters).ToLowerCamelCase(),
                        Message = "Enter distance"
                    },
                    new ValidationError
                    {
                        Field = nameof(payload.DistanceInKilometers).ToLowerCamelCase(),
                        Message = "Enter distance"
                    }
                });

            if (payload.DistanceInMeters.HasValue && payload.DistanceInMeters.Value <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.DistanceInMeters).ToLowerCamelCase(),
                    Message = "Distance should be a positive number"
                });

            if (payload.DistanceInKilometers.HasValue && payload.DistanceInKilometers.Value <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.DistanceInKilometers).ToLowerCamelCase(),
                    Message = "Distance should be a positive number"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateTime(CreateJogCommand comand)
        {
            var validationErrors = new List<ValidationError>();

            if (comand.ElapsedTime.Hours < 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(comand.ElapsedTime.Hours).ToLowerCamelCase(),
                    Message = "Hours should not be a negative number"
                });

            if (comand.ElapsedTime.Minutes < 0 || comand.ElapsedTime.Minutes > 59)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(comand.ElapsedTime.Minutes).ToLowerCamelCase(),
                    Message = "Minutes should be in the range from 0 to 59"
                });

            if (comand.ElapsedTime.Minutes < 0 || comand.ElapsedTime.Seconds > 59)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(comand.ElapsedTime.Seconds).ToLowerCamelCase(),
                    Message = "Seconds should be in the range from 0 to 59"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateDate(CreateJogCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (payload.Date > DateTime.Now)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Date).ToLowerCamelCase(),
                    Message = "Incorrect date"
                });

            return validationErrors;
        }
    }
}
