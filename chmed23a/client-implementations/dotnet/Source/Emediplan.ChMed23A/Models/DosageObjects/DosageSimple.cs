using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.DosageObjects
{
    /// <summary>
    ///     Specifies an amount to be taken.
    /// </summary>
    /// <seealso cref="DosageObject" />
    public class DosageSimple : DosageObject
    {
        #region Public Properties

        public override DosageType Type => DosageType.Simple;

        /// <summary>
        ///     The amount to be applied.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageSimple.Amount)]
        public double? Amount { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Amount == null)
            {
                yield return new ValidationError("The amount must be set",
                                                 nameof(Amount),
                                                 JsonPropertyNames.DosageObject.DosageSimple.Amount,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (Amount <= 0)
            {
                yield return new ValidationError("The amount must be greater than 0",
                                                 nameof(Amount),
                                                 JsonPropertyNames.DosageObject.DosageSimple.Amount,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(0));
            }
        }

        #endregion
    }
}