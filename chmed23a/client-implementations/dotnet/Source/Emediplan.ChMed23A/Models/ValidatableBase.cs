using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Validation;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     Abstract base class for <see cref="IValidatable" /> objects, adding functionality to compute property paths when a
    ///     a validation error occurs.
    /// </summary>
    /// <seealso cref="IValidatable" />
    public abstract class ValidatableBase : IValidatable
    {
        #region Private Fields

        private const string PropertyPathSeparator = ".";

        #endregion

        #region Public Methods

        public abstract IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext);

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Gets the validation errors for all validatable objects in the list passed as parameter.<br />
        ///     The <see cref="propertyName" /> will be included at the end of <see cref="ValidationError.PropertyPath" /> with the
        ///     index of the element that caused the error.
        /// </summary>
        /// <typeparam name="TValidatable">The type of the validatable object.</typeparam>
        /// <param name="validatableList">The list of validatable objects.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="jsonPropertyName">The name of the property as it is being JSON serialized.</param>
        /// <returns>
        ///     All validation errors for the objects contained in the list. Note that an empty error list will be returned if
        ///     <see cref="validatableList" /> is null.
        /// </returns>
        protected IEnumerable<ValidationError> GetValidationErrors<TValidatable>(IList<TValidatable> validatableList,
                                                                                 ValidationContext validationContext,
                                                                                 string propertyName,
                                                                                 string jsonPropertyName)
            where TValidatable : ValidatableBase
        {
            if (validatableList == null)
                return Enumerable.Empty<ValidationError>();

            string BasePropertyName(TValidatable validatable) => $"{propertyName}[{validatableList.IndexOf(validatable)}]";

            string BaseJsonPropertyName(TValidatable validatable) => $"{jsonPropertyName}[{validatableList.IndexOf(validatable)}]";

            return validatableList.SelectMany(validatable => GetErrorWithUpdatedProperty(BasePropertyName(validatable),
                                                                                         BaseJsonPropertyName(validatable),
                                                                                         validatable.GetValidationErrors(validationContext)));
        }

        /// <summary>
        ///     Gets the validation errors for the validatable object passed as parameter.<br />
        ///     The <see cref="propertyName" /> will be included at the end of <see cref="ValidationError.PropertyPath" /> of all
        ///     return <see cref="ValidationError" />.
        /// </summary>
        /// <typeparam name="TValidatable">The type of the validatable object.</typeparam>
        /// <param name="validatable">The validatable object.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="jsonPropertyName">The name of the property as it is being JSON serialized.</param>
        /// <returns>
        ///     All validation errors for the validatable object. Note that an empty error list will be returned if
        ///     <see cref="validatable" /> is null.
        /// </returns>
        protected IEnumerable<ValidationError> GetValidationErrors<TValidatable>(TValidatable validatable,
                                                                                 ValidationContext validationContext,
                                                                                 string propertyName,
                                                                                 string jsonPropertyName)
            where TValidatable : ValidatableBase =>
            validatable == null
                ? Enumerable.Empty<ValidationError>()
                : GetErrorWithUpdatedProperty(propertyName, jsonPropertyName, validatable.GetValidationErrors(validationContext));

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the errors with an updated <see cref="ValidationError.PropertyPath" /> to describe the full path.
        /// </summary>
        /// <param name="basePropertyName">Name of the base property.</param>
        /// <param name="baseJsonPropertyName">Name of the base property when serialized as JSON.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>The errors with updated </returns>
        private static IEnumerable<ValidationError> GetErrorWithUpdatedProperty(string basePropertyName, string baseJsonPropertyName, IEnumerable<ValidationError> errors) =>
            errors.Select(error => new ValidationError(error.Message,
                                                       $"{basePropertyName}{PropertyPathSeparator}{error.PropertyPath}",
                                                       $"{baseJsonPropertyName}{PropertyPathSeparator}{error.PropertyPathJson}",
                                                       error.Reason,
                                                       error.Reference));

        #endregion
    }
}