using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Constants;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     A risk category.
    /// </summary>
    public class RiskCategory : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     Code of of risk category as specified in the specification.
        ///     <see cref="RiskCategoryCodeConstants" /> to use predefined risk category code constants.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RiskCategory.Id)]
        public int? Id { get; set; }

        /// <summary>
        ///     The list of risks in the group. See specification for possible values.
        /// </summary>
        [JsonProperty(JsonPropertyNames.RiskCategory.RiskIds)]
        public ICollection<int> RiskIds { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Id == null)
            {
                yield return new ValidationError("The id must be set.",
                                                 nameof(Id),
                                                 JsonPropertyNames.RiskCategory.Id,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}