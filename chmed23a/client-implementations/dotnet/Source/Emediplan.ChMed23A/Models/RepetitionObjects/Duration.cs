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
    public class Duration : RepetitionObject
    {
        #region Public Properties

        public override RepetitionObjectType Type => RepetitionObjectType.Duration;

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.Duration.Unit)]
        public TimeUnit? Unit { get; set; }

        /// <summary>
        ///     The value indicating the number of repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RepetitionObject.Duration.DurationValue)]
        public int? DurationValue { get; set; }

        #endregion

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (DurationValue <= 0)
            {
                yield return new ValidationError("The duration value must be greater or equal to 0",
                                                 nameof(DurationValue),
                                                 JsonPropertyNames.RepetitionObject.Duration.DurationValue,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (Unit != null && !Enum.IsDefined(typeof(TimeUnit), Unit))
            {
                var enumValues = (TimeUnit[])Enum.GetValues(typeof(TimeUnit));
                var enumValuesString = string.Join(", ", enumValues.Select(pId => $"{(int)pId} ({pId})"));

                yield return new ValidationError($"The type '{Unit}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(Unit),
                                                 JsonPropertyNames.RepetitionObject.Duration.Unit,
                                                 ValidationErrorReason.NotSupported);
            }

            if (validationContext.MedicationType == MedicationType.Prescription)
            {
                if (DurationValue == null)
                {
                    yield return new ValidationError("The duration must be set for a prescription",
                                                     nameof(DurationValue),
                                                     JsonPropertyNames.RepetitionObject.Duration.DurationValue,
                                                     ValidationErrorReason.IsRequired);
                }

                if (Unit == null)
                {
                    yield return new ValidationError("The duration unit must be set for a prescription",
                                                     nameof(Unit),
                                                     JsonPropertyNames.RepetitionObject.Duration.Unit,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }
    }
}