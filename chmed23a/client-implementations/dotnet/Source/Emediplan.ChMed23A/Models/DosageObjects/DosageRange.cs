using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.DosageObjects
{
    public class DosageRange : DosageObject
    {
        #region Public Properties

        public override DosageType Type => DosageType.Range;

        /// <summary>
        ///     The minimal amount to be taken.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageRange.MinAmount)]
        public double? MinAmount { get; set; }

        /// <summary>
        ///     The maximum amount to be taken.
        /// </summary>
        [JsonProperty(JsonPropertyNames.DosageObject.DosageRange.MaxAmount)]
        public double? MaxAmount { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (MinAmount == null)
            {
                yield return new ValidationError("The minimum amount must be set",
                                                 nameof(MinAmount),
                                                 JsonPropertyNames.DosageObject.DosageRange.MinAmount,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (MinAmount < 0)
            {
                yield return new ValidationError("The minimum amount must be greater than 0",
                                                 nameof(MinAmount),
                                                 JsonPropertyNames.DosageObject.DosageRange.MinAmount,
                                                 ValidationErrorReason.MustBeGreaterThanOrEqual,
                                                 new ValidationErrorDoubleValueReference(0));
            }

            if (MaxAmount == null)
            {
                yield return new ValidationError("The maximum amount must be set",
                                                 nameof(MaxAmount),
                                                 JsonPropertyNames.DosageObject.DosageRange.MaxAmount,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (MaxAmount <= MinAmount)
            {
                yield return new ValidationError("The maximum amount must be greater than minimum amount",
                                                 nameof(MaxAmount),
                                                 JsonPropertyNames.DosageObject.DosageRange.MaxAmount,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(MinAmount.Value));
            }
        }

        #endregion
    }
}