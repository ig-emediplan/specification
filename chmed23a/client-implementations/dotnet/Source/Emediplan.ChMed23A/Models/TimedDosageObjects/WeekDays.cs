using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;
using DayOfWeek = Emediplan.ChMed23A.Models.Enums.DayOfWeek;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Specifies a dosage for specific week days.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class WeekDays : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.WeekDays;

        /// <summary>
        ///     List of week days on which to take the medication.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.WeekDays.DaysOfWeek)]
        public IList<DayOfWeek> DaysOfWeek { get; set; }

        /// <summary>
        ///     Specifies the timed dosage to take on the specified <see cref="WeekDays" />.<br />
        ///     Supported types are:<br />
        ///     - <see cref="DosageOnly" />: The patient has to take it at any time during the day.<br />
        ///     - <see cref="Times" />: The patient has to take the medicament at specific times.<br />
        ///     - <see cref="DaySegments" />: The patient has to take the the medicament in a specific segment (morning, noon,
        ///     evening or night)
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.WeekDays.TimedDosage)]
        public TimedDosageObject TimedDosage { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(TimedDosage, validationContext, nameof(TimedDosage), JsonPropertyNames.TimedDosageObject.WeekDays.TimedDosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (DaysOfWeek == null || !DaysOfWeek.Any())
            {
                yield return new ValidationError("Days of week must be set.",
                                                 nameof(DaysOfWeek),
                                                 JsonPropertyNames.TimedDosageObject.WeekDays.DaysOfWeek,
                                                 ValidationErrorReason.IsRequired);
            }

            if (DaysOfWeek != null && DaysOfWeek.Distinct().Count() != DaysOfWeek.Count)
            {
                yield return new ValidationError("Days of week cannot contain the same day multiple times.",
                                                 nameof(DaysOfWeek),
                                                 JsonPropertyNames.TimedDosageObject.WeekDays.DaysOfWeek,
                                                 ValidationErrorReason.MustBeUnique);
            }

            if (DaysOfWeek != null && DaysOfWeek.Any(dayOfWeek => !Enum.IsDefined(typeof(DayOfWeek), dayOfWeek)))
            {
                var enumValues = (DaySegment[])Enum.GetValues(typeof(DaySegment));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"At least one day of week is not supported. Possible values: {enumValuesString}",
                                                 nameof(DaysOfWeek),
                                                 JsonPropertyNames.TimedDosageObject.WeekDays.DaysOfWeek,
                                                 ValidationErrorReason.NotSupported);
            }

            switch (TimedDosage)
            {
                case DosageOnly _:
                case DaySegments _:
                case Times _:
                case Interval _:
                    // Handles the supported timed dosages
                    yield break;

                case null:
                    yield return new ValidationError("Timed dosage must be set",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.TimedDosageObject.WeekDays.TimedDosage,
                                                     ValidationErrorReason.IsRequired);

                    break;

                default:
                    yield return new ValidationError($"Timed dosage of type {TimedDosage.GetType().Name} is not supported.",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.TimedDosageObject.WeekDays.TimedDosage,
                                                     ValidationErrorReason.NotSupported);

                    break;
            }
        }

        #endregion
    }
}