using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Validation;

namespace Emediplan.ChMed23A.Exceptions
{
    /// <summary>
    ///     Exception being thrown when validation errors occur.
    /// </summary>
    public class ChMed23AValidationException : Exception
    {
        #region Public Properties

        /// <summary>
        ///     The validation errors.
        /// </summary>
        public ICollection<ValidationError> ValidationErrors { get; }

        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <value>The message.</value>
        public override string Message =>
            $"Validation errors occurred:{Environment.NewLine}"
          + $"{string.Join(Environment.NewLine, ValidationErrors.Select(error => $"{error.PropertyPath}: {error.Message}"))}";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChMed23AValidationException" /> class.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        public ChMed23AValidationException(ICollection<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        #endregion
    }
}