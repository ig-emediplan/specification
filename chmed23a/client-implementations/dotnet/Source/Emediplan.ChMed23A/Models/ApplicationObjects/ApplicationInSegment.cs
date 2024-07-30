using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.ApplicationObjects
{
    /// <summary>
    ///     Specifies the dosage to take for a specific day segment.
    /// </summary>
    /// <seealso cref="ApplicationObject" />
    public class ApplicationInSegment : ApplicationObject
    {
        #region Public Properties

        /// <summary>
        ///     The day segment.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ApplicationObject.ApplicationInSegment.Segment)]
        public DaySegment? Segment { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            base.GetValidationErrors(validationContext)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Segment == null)
            {
                yield return new ValidationError("The segment must be set",
                                                 nameof(Segment),
                                                 JsonPropertyNames.ApplicationObject.ApplicationInSegment.Segment,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(DaySegment), Segment))
            {
                var enumValues = (DaySegment[])Enum.GetValues(typeof(DaySegment));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{Segment}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(Segment),
                                                 JsonPropertyNames.ApplicationObject.ApplicationInSegment.Segment,
                                                 ValidationErrorReason.NotSupported);
            }
        }

        #endregion
    }
}