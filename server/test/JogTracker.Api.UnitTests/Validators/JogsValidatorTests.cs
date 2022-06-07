using JogTracker.Api.Validators;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.Requests.Jogs;
using System;
using System.Linq;
using Xunit;

namespace JogTracker.Api.UnitTests.Validators
{
    public class JogsValidatorTests : ValidatorTestBase
    {
        private readonly IJogsValidator _validator;

        public JogsValidatorTests()
        {
            _validator = new JogsValidator();
        }

        [Fact]
        public void Validate_AllFieldsAreValid_ReturnsNoErrors()
        {
            var payload = GetValidJog();

            var result = _validator.Validate(payload);

            Assert.True(result.IsValid);
            Assert.Empty(result.ValidationErrors);
        }

        [Fact]
        public void Validate_InvalidDistance_ReturnsErrors()
        {
            var payload = GetValidJog();
            payload.DistanceInMeters = null;
            payload.DistanceInKilometers = null;

            var result = _validator.Validate(payload);

            Assert.False(result.IsValid);
            Assert.Collection(result.ValidationErrors,
                err => AssertError(err, "distanceInMeters", "Enter distance"),
                err => AssertError(err, "distanceInKilometers", "Enter distance"));

            payload = GetValidJog();
            payload.DistanceInMeters = 0;
            payload.DistanceInKilometers = 0;

            result = _validator.Validate(payload);

            Assert.False(result.IsValid);
            Assert.Collection(result.ValidationErrors,
                err => AssertError(err, "distanceInMeters", "Distance should be greater than 0"),
                err => AssertError(err, "distanceInKilometers", "Distance should be greater than 0"));
        }

        [Fact]
        public void Validate_InvalidElapsedTime_ReturnsErrors()
        {
            var payload = GetValidJog();
            payload.ElapsedTime = new JogTime(0, 0, 0);

            var result = _validator.Validate(payload);
            var error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "time", "Invalid time");
        }

        [Fact]
        public void Validate_Date_ReturnsErrors()
        {
            var payload = GetValidJog();
            payload.Date = DateTime.Now.AddDays(1);

            var result = _validator.Validate(payload);
            var error = result.ValidationErrors.Single();

            Assert.False(result.IsValid);
            AssertError(error, "date", "Invalid date");
        }

        private JogPayloadCommand GetValidJog() => new JogPayloadCommand
        {
            DistanceInMeters = 10000,
            DistanceInKilometers = 10,
            Date = DateTime.Now,
            ElapsedTime = new JogTime { Hours = 1, Minutes = 30, Seconds = 45 }
        };
    }
}
