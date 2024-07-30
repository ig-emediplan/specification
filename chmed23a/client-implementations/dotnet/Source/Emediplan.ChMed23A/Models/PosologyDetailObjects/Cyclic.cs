using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Specifies a cyclic posology.
    /// </summary>
    /// <seealso cref="PosologyDetailObject" />
    public class Cyclic : PosologyDetailObject
    {
        #region Public Properties

        public override PosologyObjectType ObjectType => PosologyObjectType.Cyclic;

        /// <summary>
        ///     The unit of the cycle duration.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDurationUnit)]
        public TimeUnit? CycleDurationUnit { get; set; }

        /// <summary>
        ///     The cycle duration (cycle length). The unit of the cycle is being specified by <see cref="CycleDurationUnit" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDuration)]
        public int? CycleDuration { get; set; }

        /// <summary>
        ///     The timed dosage specifying when the intake(s) has to occur.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage)]
        public TimedDosageObject TimedDosage { get; set; }

        /// <summary>
        ///     The repetitions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosagesPerCycle)]
        public int TimedDosagesPerCycle { get; set; } = 1;

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(TimedDosage, validationContext, nameof(TimedDosage), JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (CycleDuration == null)
            {
                yield return new ValidationError("The cycle duration must be set.",
                                                 nameof(CycleDuration),
                                                 JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDuration,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (CycleDuration <= 0)
            {
                yield return new ValidationError("The cycle duration must be greater than 0.",
                                                 nameof(CycleDuration),
                                                 JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDuration,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (CycleDurationUnit == null)
            {
                yield return new ValidationError("The cycle duration unit must be set.",
                                                 nameof(CycleDurationUnit),
                                                 JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDurationUnit,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(TimeUnit), CycleDurationUnit))
            {
                var enumValues = (DaySegment[])Enum.GetValues(typeof(DaySegment));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{CycleDurationUnit}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(CycleDurationUnit),
                                                 JsonPropertyNames.PosologyDetailObject.Cyclic.CycleDurationUnit,
                                                 ValidationErrorReason.NotSupported);
            }

            if (TimedDosagesPerCycle <= 0)
            {
                yield return new ValidationError("The timed dosages per cycle must be greater than 0.",
                                                 nameof(TimedDosagesPerCycle),
                                                 JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosagesPerCycle,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            switch (TimedDosage)
            {
                case DosageOnly _:
                case Times _:
                case DaySegments _:
                case Interval _:
                    // Supported timed dosages
                    yield break;

                case WeekDays _:
                    if (CycleDurationUnit != TimeUnit.Week)
                    {
                        yield return new ValidationError($"When setting a timed dosage of type {nameof(WeekDays)} the cycle duration unit must be {nameof(TimeUnit.Week)}",
                                                         nameof(TimedDosage),
                                                         JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage,
                                                         ValidationErrorReason.NotSupported);
                    }

                    break;

                case DaysOfMonth _:
                    if (CycleDurationUnit != TimeUnit.Month)
                    {
                        yield return new ValidationError($"When setting a timed dosage of type {nameof(DaysOfMonth)} the cycle duration unit must be {nameof(TimeUnit.Month)}",
                                                         nameof(TimedDosage),
                                                         JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage,
                                                         ValidationErrorReason.NotSupported);
                    }

                    break;

                case null:
                    yield return new ValidationError("The timed dosage  must be set",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage,
                                                     ValidationErrorReason.IsRequired);

                    break;

                default:
                    yield return new ValidationError($"Timed dosage of type {TimedDosage.GetType().Name} is not supported",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.PosologyDetailObject.Cyclic.TimedDosage,
                                                     ValidationErrorReason.NotSupported);

                    break;
            }
        }

        #endregion
    }
}