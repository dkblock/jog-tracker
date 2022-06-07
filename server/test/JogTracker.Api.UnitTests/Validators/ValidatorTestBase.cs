using JogTracker.Models.Validation;
using Xunit;

namespace JogTracker.Api.UnitTests.Validators
{
    public class ValidatorTestBase
    {
        public void AssertError(ValidationError error, string expectedField, string expectedMessage)
        {
            Assert.Equal(expectedField, error.Field);
            Assert.Equal(expectedMessage, error.Message);
        }
    }
}
