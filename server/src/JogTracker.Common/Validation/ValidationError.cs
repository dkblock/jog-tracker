using JogTracker.Common.Exceptions;

namespace JogTracker.Models.Validation
{
    public class ValidationError : Error
    {
        public string Field { get; set; }
    }
}
