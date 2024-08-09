using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.SequenceObjects
{
    /// <summary>
    ///     Abstract base class for sequence objects. Sequence objects can be chained; e.g. Take for 2 days, then make a pause
    ///     of 1 day, then start over.
    /// </summary>
    /// <seealso cref="ValidatableBase" />
    public abstract class SequenceObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The sequence object type.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ObjectType)]
        public abstract SequenceObjectType Type { get; }

        /// <summary>
        ///     The unit of the cycle duration.
        /// </summary>
        [JsonProperty(JsonPropertyNames.SequenceObject.DurationUnit)]
        public TimeUnit? DurationUnit { get; set; }

        /// <summary>
        ///     The cycle duration (cycle length). The unit of the cycle is being specified by <see cref="DurationUnit" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.SequenceObject.Duration)]
        public int? Duration { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Validates the <see cref="PosologyDetailObject" />. If the returned list is not empty, validation errors have
        ///     occurred.
        /// </summary>
        /// <param name="validationContext"></param>
        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Duration == null)
            {
                yield return new ValidationError("The duration must be set",
                                                 nameof(Duration),
                                                 JsonPropertyNames.SequenceObject.Duration,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Duration <= 0)
            {
                yield return new ValidationError("The duration must be greater than 0",
                                                 nameof(Duration),
                                                 JsonPropertyNames.SequenceObject.Duration,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (DurationUnit == null)
            {
                yield return new ValidationError("The duration unit must be set",
                                                 nameof(DurationUnit),
                                                 JsonPropertyNames.SequenceObject.DurationUnit,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(TimeUnit), DurationUnit))
            {
                var enumValues = (TimeUnit[])Enum.GetValues(typeof(TimeUnit));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{DurationUnit}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(DurationUnit),
                                                 JsonPropertyNames.SequenceObject.DurationUnit,
                                                 ValidationErrorReason.NotSupported);
            }
        }

        #endregion
    }
}