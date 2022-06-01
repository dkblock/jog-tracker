using JogTracker.Api.Common;
using JogTracker.Models.Jogs;
using JogTracker.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogTracker.Api.Validation
{
    public interface IJogValidator
    {
        ValidationResult Validate(JogPayload jog);
    }

    public class JogValidator : IJogValidator
    {
        public ValidationResult Validate(JogPayload jog)
        {
            var validationErrors = new List<ValidationError>();

            validationErrors.AddRange(ValidateDistance(jog));
            validationErrors.AddRange(ValidateTime(jog));
            validationErrors.AddRange(ValidateDate(jog));

            return new ValidationResult
            {
                IsValid = !validationErrors.Any(),
                ValidationErrors = validationErrors
            };
        }

        private IEnumerable<ValidationError> ValidateDistance(JogPayload jog)
        {
            var validationErrors = new List<ValidationError>();

            if (!jog.DistanceInMeters.HasValue && !jog.DistanceInKilometers.HasValue)
                validationErrors.AddRange(new List<ValidationError> {
                    new ValidationError
                    {
                        Field = nameof(jog.DistanceInMeters).ToLowerCamelCase(),
                        Message = "Enter distance"
                    },
                    new ValidationError
                    {
                        Field = nameof(jog.DistanceInKilometers).ToLowerCamelCase(),
                        Message = "Enter distance"
                    }
                });

            if (jog.DistanceInMeters.HasValue && jog.DistanceInMeters.Value <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.DistanceInMeters).ToLowerCamelCase(),
                    Message = "Distance should be a positive number"
                });

            if (jog.DistanceInKilometers.HasValue && jog.DistanceInKilometers.Value <= 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.DistanceInKilometers).ToLowerCamelCase(),
                    Message = "Distance should be a positive number"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateTime(JogPayload jog)
        {
            var validationErrors = new List<ValidationError>();            

            if (jog.ElapsedTime.Hours < 0)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.ElapsedTime.Hours).ToLowerCamelCase(),
                    Message = "Hours should not be a negative number"
                });

            if (jog.ElapsedTime.Minutes < 0 || jog.ElapsedTime.Minutes > 59)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.ElapsedTime.Minutes).ToLowerCamelCase(),
                    Message = "Minutes should be in the range from 0 to 59"
                });

            if (jog.ElapsedTime.Minutes < 0 || jog.ElapsedTime.Seconds > 59)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.ElapsedTime.Seconds).ToLowerCamelCase(),
                    Message = "Seconds should be in the range from 0 to 59"
                });

            return validationErrors;
        }

        private IEnumerable<ValidationError> ValidateDate(JogPayload jog)
        {
            var validationErrors = new List<ValidationError>();

            if (jog.Date > DateTime.Now)
                validationErrors.Add(new ValidationError
                {
                    Field = nameof(jog.Date).ToLowerCamelCase(),
                    Message = "Incorrect date"
                });

            return validationErrors;
        }
    }
}
