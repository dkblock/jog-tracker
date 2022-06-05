using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Jogs;
using JogTracker.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogTracker.Api.Validators
{
    public interface IJogsValidator
    {
        ValidationResult Validate(JogPayloadCommand payload);
    }

    public class JogsValidator : IJogsValidator
    {
        public ValidationResult Validate(JogPayloadCommand payload)
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

        private IEnumerable<ValidationError> ValidateDistance(JogPayloadCommand payload)
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
                    Message = "Distance should be greater than 0"
                });

            if (payload.DistanceInKilometers.HasValue && payload.DistanceInKilometers.Value <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.DistanceInKilometers).ToLowerCamelCase(),
                    Message = "Distance should be greater than 0"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateTime(JogPayloadCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (new TimeSpan(payload.ElapsedTime.Hours, payload.ElapsedTime.Minutes, payload.ElapsedTime.Seconds).TotalSeconds <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = "time",
                    Message = "Invalid time"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateDate(JogPayloadCommand payload)
        {
            var validationErrors = new List<ValidationError>();

            if (payload.Date > DateTime.Now)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(payload.Date).ToLowerCamelCase(),
                    Message = "Invalid date"
                });

            return validationErrors;
        }
    }
}
