using System.Collections.Generic;

namespace Emediplan.ChMed23A.Validation
{
    /// <summary>
    ///     Represents a model object which can be validated.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        ///     Gets the validation errors.
        /// </summary>
        /// <param name="validationContext"></param>
        IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext);
    }
}