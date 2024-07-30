using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.ApplicationObjects
{
    /// <summary>
    ///     Specifies the dosage to be applied at a specific time.
    /// </summary>
    /// <seealso cref="ApplicationObject" />
    public class ApplicationAtTime : ApplicationObject
    {
        #region Public Properties

        /// <summary>
        ///     The time of day, represented by a time span.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ApplicationObject.ApplicationAtTime.TimeOfDay)]
        public TimeSpan? TimeOfDay { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            base.GetValidationErrors(validationContext)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (TimeOfDay == null)
            {
                yield return new ValidationError("The time of day must be set",
                                                 nameof(TimeOfDay),
                                                 JsonPropertyNames.ApplicationObject.ApplicationAtTime.TimeOfDay,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (TimeOfDay <= TimeSpan.Zero || TimeOfDay > TimeSpan.FromHours(24))
            {
                yield return new ValidationError("The time of day must be greater than 0 and smaller than or equal to 24h",
                                                 nameof(TimeOfDay),
                                                 JsonPropertyNames.ApplicationObject.ApplicationAtTime.TimeOfDay,
                                                 ValidationErrorReason.MustBeInRange,
                                                 new ValidationErrorTimespanReference(TimeSpan.Zero, TimeSpan.FromHours(24)));
            }
        }

        #endregion
    }
}