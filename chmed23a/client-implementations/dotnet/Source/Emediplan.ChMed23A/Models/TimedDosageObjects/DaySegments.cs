using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Specifies a dosage for specific day segments.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class DaySegments : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.DaySegments;

        /// <summary>
        ///     The applications specify the dosage to take in a segment.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.DaySegments.Applications)]
        public IList<ApplicationInSegment> Applications { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetMyValidationErrors()
                .Concat(GetValidationErrors(Applications, validationContext, nameof(Applications), JsonPropertyNames.TimedDosageObject.DaySegments.Applications));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Applications == null || !Applications.Any())
            {
                yield return new ValidationError("The applications must be set.",
                                                 nameof(Applications),
                                                 JsonPropertyNames.TimedDosageObject.DaySegments.Applications,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}