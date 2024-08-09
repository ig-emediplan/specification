using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.ApplicationObjects
{
    /// <summary>
    ///     Abstract base class to specify a taking.
    /// </summary>
    /// <seealso cref="Emediplan.ChMed23A.Models.ValidatableBase" />
    public abstract class ApplicationObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The dosage to take.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ApplicationObject.Dosage)]
        public DosageObject Dosage { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Dosage, validationContext, nameof(Dosage), JsonPropertyNames.ApplicationObject.Dosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (Dosage == null)
            {
                yield return new ValidationError("The dosage must be set",
                                                 nameof(Dosage),
                                                 JsonPropertyNames.ApplicationObject.Dosage,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}