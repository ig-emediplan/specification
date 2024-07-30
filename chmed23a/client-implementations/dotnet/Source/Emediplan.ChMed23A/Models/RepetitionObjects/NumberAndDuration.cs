using System;
using System.Collections.Generic;
using System.Linq;
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
    public class NumberAndDuration : RepetitionObject
    {
        #region Public Properties

        public override RepetitionObjectType Type => RepetitionObjectType.NumberAndDuration;

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.NumberAndDuration.Value)]
        public int? Value { get; set; }

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.NumberAndDuration.Unit)]
        public TimeUnit? Unit { get; set; }

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.NumberAndDuration.DurationValue)]
        public int? DurationValue { get; set; }

        #endregion

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Value < 0)
            {
                yield return new ValidationError("The value must be greater or equal to 0",
                                                 nameof(Value),
                                                 JsonPropertyNames.RepetitionObject.NumberAndDuration.Value,
                                                 ValidationErrorReason.MustBeGreaterThanOrEqual,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (DurationValue <= 0)
            {
                yield return new ValidationError("The duration value must be greater than 0",
                                                 nameof(DurationValue),
                                                 JsonPropertyNames.RepetitionObject.NumberAndDuration.DurationValue,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (Unit != null && !Enum.IsDefined(typeof(TimeUnit), Unit))
            {
                var enumValues = (TimeUnit[])Enum.GetValues(typeof(TimeUnit));
                var enumValuesString = string.Join(", ", enumValues.Select(pId => $"{(int)pId} ({pId})"));

                yield return new ValidationError($"The type '{Unit}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(Unit),
                                                 JsonPropertyNames.RepetitionObject.NumberAndDuration.Unit,
                                                 ValidationErrorReason.NotSupported);
            }

            if (validationContext.MedicationType == MedicationType.Prescription)
            {
                if (Value == null)
                {
                    yield return new ValidationError("The value must be set for a prescription",
                                                     nameof(Value),
                                                     JsonPropertyNames.RepetitionObject.NumberAndDuration.Value,
                                                     ValidationErrorReason.IsRequired);
                }

                if (DurationValue == null)
                {
                    yield return new ValidationError("The duration must be set for a prescription",
                                                     nameof(DurationValue),
                                                     JsonPropertyNames.RepetitionObject.NumberAndDuration.DurationValue,
                                                     ValidationErrorReason.IsRequired);
                }

                if (Unit == null)
                {
                    yield return new ValidationError("The duration unit must be set for a prescription",
                                                     nameof(Unit),
                                                     JsonPropertyNames.RepetitionObject.NumberAndDuration.Unit,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }
    }
}