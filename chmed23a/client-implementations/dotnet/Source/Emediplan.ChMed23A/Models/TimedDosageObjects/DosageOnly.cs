using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Specifies a dosage without additional information, like when it has to be taken.
    /// </summary>
    /// <seealso cref="TimedDosageObject" />
    public class DosageOnly : TimedDosageObject
    {
        #region Public Properties

        public override TimedDosageType Type => TimedDosageType.DosageOnly;

        /// <summary>
        ///     The dosage of the medicament.
        /// </summary>
        [JsonProperty(JsonPropertyNames.TimedDosageObject.DosageOnly.Dosage)]
        public DosageObject Dosage { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Dosage, validationContext, nameof(Dosage), JsonPropertyNames.TimedDosageObject.DosageOnly.Dosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Dosage == null)
            {
                yield return new ValidationError("The dosage must be set.",
                                                 nameof(Dosage),
                                                 JsonPropertyNames.TimedDosageObject.DosageOnly.Dosage,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}