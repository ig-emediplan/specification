using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Specifies a dosage for specific days of the month.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class DaysOfMonth : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.DaysOfMonth;

        /// <summary>
        ///     List of days of the month on which to take the medication.<br />
        ///     The supported range is 1-31.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.DaysOfMonth.Days)]
        public ICollection<int> Days { get; set; }

        /// <summary>
        ///     Specifies the timed dosage to take on the specified <see cref="WeekDays" />.<br />
        ///     Supported types are:<br />
        ///     - <see cref="DosageOnly" />: The patient has to take it at any time during the day.<br />
        ///     - <see cref="Times" />: The patient has to take the medicament at specific times.<br />
        ///     - <see cref="DaySegments" />: The patient has to take the the medicament in a specific segment (morning, noon,
        ///     evening or night)
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.DaysOfMonth.TimedDosage)]
        public TimedDosageObject TimedDosage { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(TimedDosage, validationContext, nameof(TimedDosage), JsonPropertyNames.TimedDosageObject.DaysOfMonth.TimedDosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Days == null || !Days.Any())
            {
                yield return new ValidationError("The days must be set.",
                                                 nameof(Days),
                                                 JsonPropertyNames.TimedDosageObject.DaysOfMonth.Days,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (Days.Any(day => day > 31 || day < 1))
            {
                yield return new ValidationError("Specified days must be in range 1 to 31.",
                                                 nameof(Days),
                                                 JsonPropertyNames.TimedDosageObject.DaysOfMonth.Days,
                                                 ValidationErrorReason.MustBeInRange,
                                                 new ValidationErrorIntRangeReference(1, 31));
            }

            switch (TimedDosage)
            {
                case null:
                    yield return new ValidationError("The timed dosage must be set",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.TimedDosageObject.DaysOfMonth.TimedDosage,
                                                     ValidationErrorReason.IsRequired);

                    break;

                case DosageOnly _:
                case DaySegments _:
                case Times _:
                case Interval _:
                    // DosageOnly, DaySegments and Times are the only supported TimedDosageObject for DaysOfMonth
                    yield break;

                default:
                    yield return new ValidationError($"Timed dosage of type {TimedDosage.GetType().Name} is not supported.",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.TimedDosageObject.DaysOfMonth.TimedDosage,
                                                     ValidationErrorReason.NotSupported);

                    break;
            }
        }

        #endregion
    }
}