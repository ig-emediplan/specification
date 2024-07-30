using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.RepetitionObjects
{
    /// <summary>
    ///     Specifies how often a repetition can occur.
    /// </summary>
    /// <seealso cref="RepetitionObject" />
    public class Number : RepetitionObject
    {
        #region Public Properties

        public override RepetitionObjectType Type => RepetitionObjectType.Number;

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.Number.Value)]
        public int? Value { get; set; }

        #endregion

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Value < 0)
            {
                yield return new ValidationError("The value must be greater or equal to 0",
                                                 nameof(Value),
                                                 JsonPropertyNames.RepetitionObject.Number.Value,
                                                 ValidationErrorReason.MustBeGreaterThanOrEqual,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (validationContext.MedicationType == MedicationType.Prescription)
            {
                if (Value == null)
                {
                    yield return new ValidationError("The value must be set for a prescription",
                                                     nameof(Value),
                                                     JsonPropertyNames.RepetitionObject.Number.Value,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }
    }
}