using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Specifies a dosage having to be taken at a fix interval.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class Interval : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.Interval;

        /// <summary>
        ///     The maximum amount to be taken.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.Interval.Dosage)]
        public DosageObject Dosage { get; set; }

        [JsonProperty(JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDurationUnit)]
        public TimeUnit? MinIntervalDurationUnit { get; set; }

        [JsonProperty(JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDuration)]
        public int? MinIntervalDuration { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Dosage, validationContext, nameof(Dosage), JsonPropertyNames.TimedDosageObject.Interval.Dosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Dosage == null)
            {
                yield return new ValidationError("Dosage must be set.",
                                                 nameof(Dosage),
                                                 JsonPropertyNames.TimedDosageObject.Interval.Dosage,
                                                 ValidationErrorReason.IsRequired);
            }

            if (MinIntervalDurationUnit == null)
            {
                yield return new ValidationError("The minimum interval duration unit must be set.",
                                                 nameof(MinIntervalDurationUnit),
                                                 JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDurationUnit,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(TimeUnit), (int)MinIntervalDurationUnit))
            {
                var patientIdEnumValues = (TimeUnit[])Enum.GetValues(typeof(TimeUnit));
                var patientIdEnumValuesString = string.Join(", ", patientIdEnumValues.Select(pId => $"{(int)pId} ({pId})"));

                yield return new ValidationError($"The type '{MinIntervalDurationUnit}' is not supported. Possible values: {patientIdEnumValuesString}",
                                                 nameof(MinIntervalDurationUnit),
                                                 JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDurationUnit,
                                                 ValidationErrorReason.NotSupported);
            }

            if (MinIntervalDuration == null)
            {
                yield return new ValidationError("The minimum interval duration must be set.",
                                                 nameof(MinIntervalDuration),
                                                 JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDuration,
                                                 ValidationErrorReason.IsRequired);
            }

            if (MinIntervalDuration <= 0)
            {
                yield return new ValidationError("The minimum interval duration must be greater than 0.",
                                                 nameof(MinIntervalDuration),
                                                 JsonPropertyNames.TimedDosageObject.Interval.MinIntervalDuration,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }
        }

        #endregion
    }
}