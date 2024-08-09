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
    ///     Specifies a dosage for specific times of day.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class Times : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.ApplicationTimes;

        /// <summary>
        ///     The takings specify when, what amount of the medicament has to be taken.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.Times.Applications)]
        public IList<ApplicationAtTime> Applications { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetMyValidationErrors()
                .Concat(GetValidationErrors(Applications, validationContext, nameof(Applications), JsonPropertyNames.TimedDosageObject.Times.Applications));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Applications == null || !Applications.Any())
            {
                yield return new ValidationError("Applications must be set.",
                                                 nameof(Applications),
                                                 JsonPropertyNames.TimedDosageObject.Times.Applications,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}