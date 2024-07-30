using Emediplan.ChMed23A.Validation.ValidationErrorReferences;

namespace Emediplan.ChMed23A.Validation
{
    /// <summary>
    ///     Describes the cause of a validation error.
    /// </summary>
    public class ValidationError
    {
        #region Public Properties

        /// <summary>
        ///     A human readable message in English.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     The path of the property having caused the validation error.
        /// </summary>
        public string PropertyPath { get; }

        /// <summary>
        ///     The path of the property having caused the validation error.
        /// </summary>
        public string PropertyPathJson { get; }

        /// <summary>
        ///     The reason of the validation error.
        /// </summary>
        /// <value>The reason.</value>
        public ValidationErrorReason Reason { get; }

        /// <summary>
        ///     The reference value having caused the validation error.<br />
        ///     If <see cref="Reason" />==<see cref="ValidationErrorReason.MustBeGreaterThan" />, reference will contain a numeric
        ///     value specifying the min value.
        /// </summary>
        public ValidationErrorReference Reference { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationError" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyPath">The property path.</param>
        /// <param name="propertyPathJson">The property path when JSON serialized.</param>
        /// <param name="reason">The reason of the validation error.</param>
        /// <param name="reference">The reference value. Default: <c>null</c>.</param>
        public ValidationError(string message, string propertyPath, string propertyPathJson, ValidationErrorReason reason, ValidationErrorReference reference = null)
        {
            Message = message;
            PropertyPath = propertyPath;
            PropertyPathJson = propertyPathJson;
            Reason = reason;
            Reference = reference;
        }

        #endregion
    }
}