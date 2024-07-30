using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.DosageObjects
{
    /// <summary>
    ///     Specifies a start and end amount to be taken during a period of time. This is typically being used for infusions.
    /// </summary>
    /// <seealso cref="DosageObject" />
    public class DosageFromTo : DosageObject
    {
        #region Public Properties

        public override DosageType Type => DosageType.FromTo;

        /// <summary>
        ///     The start amount.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageFromTo.AmountFrom)]
        public double? AmountFrom { get; set; }

        /// <summary>
        ///     The end amount.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageFromTo.AmountTo)]
        public double? AmountTo { get; set; }

        /// <summary>
        ///     The unit of the duration.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageFromTo.DurationUnit)]
        public TimeUnit? DurationUnit { get; set; }

        /// <summary>
        ///     The duration to go from <see cref="AmountFrom" /> to <see cref="AmountTo" />
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageFromTo.Duration)]
        public int? Duration { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (AmountFrom == null)
            {
                yield return new ValidationError("The amount from must be set",
                                                 nameof(AmountFrom),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.AmountFrom,
                                                 ValidationErrorReason.IsRequired);
            }

            if (AmountFrom < 0)
            {
                yield return new ValidationError("The amount from must be greater or equal to 0",
                                                 nameof(AmountFrom),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.AmountFrom,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(0));
            }

            if (AmountTo == null)
            {
                yield return new ValidationError("The amount to must be set",
                                                 nameof(AmountTo),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.AmountFrom,
                                                 ValidationErrorReason.IsRequired);
            }

            if (AmountTo <= AmountFrom)
            {
                yield return new ValidationError("The amount to must be greater than amount from",
                                                 nameof(AmountTo),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.AmountTo,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(AmountFrom.Value));
            }

            if (Duration == null)
            {
                yield return new ValidationError("The duration to must be set",
                                                 nameof(Duration),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.Duration,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Duration <= 0)
            {
                yield return new ValidationError("The duration must be greater than 0",
                                                 nameof(Duration),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.Duration,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (DurationUnit == null)
            {
                yield return new ValidationError("The duration unit must be set",
                                                 nameof(DurationUnit),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.DurationUnit,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(TimeUnit), DurationUnit))
            {
                var enumValues = (TimeUnit[])Enum.GetValues(typeof(TimeUnit));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{DurationUnit}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(DurationUnit),
                                                 JsonPropertyNames.DosageObject.DosageFromTo.DurationUnit,
                                                 ValidationErrorReason.NotSupported);
            }
        }

        #endregion
    }
}